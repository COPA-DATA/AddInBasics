using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NLog;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.Variable;

namespace BadVariableSubscriptionSample
{
    public class BadVariableSubscription
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly string _containerName;
        private IOnlineVariableContainer _container;
        private IProject _project;

        public BadVariableSubscription()
        {
            _containerName = "MyOnlineContainerCollection-" + Guid.NewGuid();
        }

        public void Start(IProject context, IEnumerable<string> variables)
        {
            _project = context;

            // Ensure that the container is deleted
            context.OnlineVariableContainerCollection.Delete(_containerName);

            // Create a new container
            _container = context.OnlineVariableContainerCollection.Create(_containerName);

            // Add variables and register Event
            ErrorHandler.ThrowOnError(_container.AddVariable(variables.ToArray()));
            _container.BulkChanged += Container_BulkChanged;

            // Activate OnlineContainer
            ErrorHandler.ThrowOnError(_container.ActivateBulkMode());
            ErrorHandler.ThrowOnError(_container.Activate());
        }

        public void Stop()
        {
            // All events are removed here - the container gets disabled and deleted.
            _container.BulkChanged -= Container_BulkChanged;

            _container.Deactivate();
            ErrorHandler.ThrowOnError(_project.OnlineVariableContainerCollection.Delete(_containerName));

        }

        private void Container_BulkChanged(object sender, BulkChangedEventArgs e)
        {
            try
            {

                // Do not execute long-running processes here. They are blocking the
                // main thread of zenon. Therefore we use the TPL (Task Parallel Library) to run the 
                // Action as separate thread

                // Here, as bad sample we block this method for 5 seconds. That means zenon is also blocked for
                // 5 seconds..
                _logger.Info("Bulk update received.");
                Thread.Sleep(5000);
             }
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
        }
    }
}
