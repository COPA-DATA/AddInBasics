using NLog;
using NLog.Config;
using NLog.Targets;

namespace BadVariableSubscriptionSample
{
    internal class NLogConfigurator
    {
        private readonly string _logFileNamePattern;

        public NLogConfigurator(string logFileNamePattern)
        {
            _logFileNamePattern = logFileNamePattern;
        }

        public NLogConfigurator()
        {
            string addInName = this.GetType().Assembly.GetName().Name;
            _logFileNamePattern = "${specialfolder:folder=CommonApplicationData}/Company/zenon/${processname}_" + addInName  + ".log";
        }

        public void Configure()
        {
            // See: https://github.com/nlog/NLog/wiki/Configuration-API

            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration
            // See http://sentinel.codeplex.com/ for an log viewer 
            var viewerTarget = new NLogViewerTarget();
            config.AddTarget("viewer", viewerTarget);

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            // Step 3. Set target properties 
            viewerTarget.Layout = @"${callsite} ${message} ${onexception:Exception information\:${exception:format=type,message,method,StackTrace:maxInnerExceptionLevel=5:innerFormat=type,message,method,StackTrace}";
            viewerTarget.Address = "udp://127.0.0.1:9999";
            fileTarget.FileName = _logFileNamePattern;
            fileTarget.Layout = @"${callsite} ${message} ${onexception:Exception information\:${exception:format=type,message,method,StackTrace:maxInnerExceptionLevel=5:innerFormat=type,message,method,StackTrace}";

            // Step 4. Define rules
            var rule1 = new LoggingRule("*", LogLevel.Debug, viewerTarget);
            config.LoggingRules.Add(rule1);

            var rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;
        }
    }
}
