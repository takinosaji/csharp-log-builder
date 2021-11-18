using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace LogBuilder.Extensions
{
    public static partial class LoggerExtensions
    {
        public static void LogInformationWith(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            LogWithProperties(logger, LogLevel.Information, message, null, properties);

        public static void LogDebugWith(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            LogWithProperties(logger, LogLevel.Debug, message, null, properties);

        public static void LogTraceWith(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            LogWithProperties(logger, LogLevel.Trace, message, null, properties);

        public static void LogErrorWith(
            this ILogger logger,
            Exception exception,
            string message,
            params LogProperty[] properties) =>
            LogWithProperties(logger, LogLevel.Error, message, exception, properties);

        public static void LogErrorWith(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            LogWithProperties(logger, LogLevel.Error, message, null, properties);

        public static void LogWarningWith(
            this ILogger logger,
            string message,
            params LogProperty[] properties) =>
            LogWithProperties(logger, LogLevel.Warning, message, null, properties);

        public static void LogWithProperties(
            this ILogger logger,
            LogLevel level,
            string message,
            IEnumerable<LogProperty> properties) =>
            LogWithProperties(logger, level, message, null, properties);

        public static void LogWithProperties(
            this ILogger logger,
            LogLevel level,
            string message,
            Exception? exception,
            IEnumerable<LogProperty> properties) =>
            logger.LogWithBuilder(level, new LogBuilder(message, exception, properties));
        
        public static void LogWithProperties(
            this ILogger logger,
            LogLevel level,
            string message,
            Exception? exception,
            params LogProperty[] properties) =>
            logger.LogWithBuilder(level, new LogBuilder(message, exception, properties));
    }
}