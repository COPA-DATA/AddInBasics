using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Scada.AddIn.Contracts;

namespace BadVariableSubscriptionSample
{
    /// <summary>
    /// Description of Project Service Extension.
    /// </summary>
    [AddInExtension("Variable Subscription Bad Sample", "Demonstrates a bad sample how you shouldn't work with events")]
    public class ProjectServiceExtension : IProjectServiceExtension
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly BadVariableSubscription _variableSubscription;

        public ProjectServiceExtension()
        {
            _variableSubscription = new BadVariableSubscription();
        }

        #region IProjectServiceExtension implementation

        public void Start(IProject context, IBehavior behavior)
        {
            var configurator = new NLogConfigurator();
            configurator.Configure();

            try
            {
                List<string> newOnlineVariables = new List<string>();

                // iterate through all variables in the current project and select all variables which are marked as "External Visible"
                foreach (var item in context.VariableCollection)
                {
                    if ((bool)item.GetDynamicProperty("ExternVisible"))
                    {
                        newOnlineVariables.Add(item.Name);
                    }                    
                }

                if (newOnlineVariables.Any())
                {
                    _variableSubscription.Start(context, newOnlineVariables);
                }
                else
                {
                    Logger.Info("No Variables in the project " + context.Name + " were marked as \"External Visible\"");
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }


        public void Stop()
        {
            try
            {
                _variableSubscription.Stop();

            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        #endregion
    }
}