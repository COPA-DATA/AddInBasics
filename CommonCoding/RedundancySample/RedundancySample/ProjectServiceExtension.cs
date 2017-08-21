using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace RedundancySample
{
    /// <summary>
    /// Description of Project Service Extension.
    /// </summary>
    [AddInExtension("Redundancy Sample", "Demonstrates the usage of IProject.ProjectNetworkType and corresponding events StandbyToServerChanging and ServerToStandbyChanging")]
    public class ProjectServiceExtension : IProjectServiceExtension
    {
        private readonly BackgroundWorker _backgroundWorker;
        private IProject _context;
        private IVariable _variable;
        private bool _runBackgroundWorker;
        private int _counter;

        public ProjectServiceExtension()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
        }

        #region IProjectServiceExtension implementation

        public void Start(IProject context, IBehavior behavior)
        {
            _context = context;

            try
            {
                _variable = _context.VariableCollection["AddInCounter"];

                context.StandbyToServerChanging += Context_StandbyToServerChanging;
                context.ServerToStandbyChanging += ContextOnServerToStandbyChanging;


                switch (context.ProjectNetworkType)
                {
                    case ProjectNetworkType.Server:
                    case ProjectNetworkType.Mainstation:
                        _runBackgroundWorker = true;
                        _backgroundWorker.RunWorkerAsync();
                        break;
                }

                MessageBox.Show("The service ist started in mode: " + context.ProjectNetworkType);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_runBackgroundWorker)
            {
                if (_counter == Int32.MaxValue)
                {
                    _counter = 0;
                }

                _variable.SetValue(0, _counter++);
                Thread.Sleep(1000);
            }
        }

        private void ContextOnServerToStandbyChanging(object sender, ServerToStandbyChangingEventArgs e)
        {
            _runBackgroundWorker = false;
        }

        private void Context_StandbyToServerChanging(object sender, StandbyToServerChangingEventArgs e)
        {
            _runBackgroundWorker = true;
            _backgroundWorker.RunWorkerAsync();
        }

        public void Stop()
        {
            _context.StandbyToServerChanging -= Context_StandbyToServerChanging;
            _context.ServerToStandbyChanging -= ContextOnServerToStandbyChanging;
            _context = null;
        }

        #endregion
    }
}