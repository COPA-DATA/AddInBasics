using System;
using NLog;
using Scada.AddIn.Contracts;

namespace NLogSample
{
    /// <summary>
    /// Description of Project Wizard Extension.
    /// </summary>
    [AddInExtension("NLogSample", "Your Project Wizard Extension Description")]
    public class ProjectWizardExtension : IProjectWizardExtension
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        #region IProjectWizardExtension implementation

        public void Run(IProject context, IBehavior behavior)
        {
            var logging = new NLogConfigurator();
            logging.Configure();

            try
            {
                _logger.Info("Hello, Logging!");
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        #endregion
    }

}