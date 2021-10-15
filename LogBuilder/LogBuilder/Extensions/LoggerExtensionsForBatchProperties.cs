using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace LogBuilder.Extensions
{
    public static partial class LoggerExtensions
    {
        public static void LogInformationWithBatches(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            logger.LogWithBatches(LogLevel.Information, message, null, properties);

        public static void LogDebugWithBatches(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            logger.LogWithBatches(LogLevel.Debug, message, null, properties);

        public static void LogTraceWithBatches(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            logger.LogWithBatches(LogLevel.Trace, message, null, properties);

        public static void LogErrorWithBatches(
            this ILogger logger,
            Exception exception,
            string message,
            params LogProperty[] properties) =>
            logger.LogWithBatches(LogLevel.Error, message, exception, properties);

        public static void LogErrorWithBatches(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            logger.LogWithBatches(LogLevel.Error, message, null, properties);

        public static void LogWarningWithBatches(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            logger.LogWithBatches(LogLevel.Warning, message, null, properties);
        
        public static void LogWithBatches(
            this ILogger logger,
            LogLevel logLevel,
            string message,
            Exception? exception,
            params LogProperty[] properties) => logger.LogWithBatches(logLevel, message, exception, (IEnumerable<LogProperty>)properties);
        
        public static void LogWithBatches(
            this ILogger logger,
            LogLevel logLevel,
            string message,
            Exception? exception,
            IEnumerable<LogProperty> properties)
        {
            var logPropertyBatches = BatchedLogPropertiesFactory.CreateBatches(properties);
            foreach (var logPropertyBatch in logPropertyBatches)
            {
                logger.LogWithProperties(logLevel, message, exception, logPropertyBatch);
            }
        }
    }
}