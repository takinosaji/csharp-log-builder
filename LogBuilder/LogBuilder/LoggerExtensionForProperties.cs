using System;
using Microsoft.Extensions.Logging;

namespace LogBuilder
{
    public static partial class LoggerExtensions
    {
        public static void LogInformationWith(
            this ILogger logger,
            string message,
            params (string key, object value)[] properties) =>
            LogWithProperties(logger, LogLevel.Information, message, null, properties);

        public static void LogDebugWith(
            this ILogger logger,
            string message,
            params (string key, object value)[] properties) =>
            LogWithProperties(logger, LogLevel.Debug, message, null, properties);

        public static void LogTraceWith(
            this ILogger logger,
            string message,
            params (string key, object value)[] properties) =>
            LogWithProperties(logger, LogLevel.Trace, message, null, properties);

        public static void LogErrorWith(
            this ILogger logger,
            Exception exception,
            string message,
            params (string key, object value)[] properties) =>
            LogWithProperties(logger, LogLevel.Error, message, exception, properties);

        public static void LogErrorWith(
            this ILogger logger,
            string message,
            params (string key, object value)[] properties) =>
            LogWithProperties(logger, LogLevel.Error, message, null, properties);

        public static void LogWarningWith(
            this ILogger logger,
            string message,
            params (string key, object value)[] properties) =>
            LogWithProperties(logger, LogLevel.Warning, message, null, properties);

        public static void LogWithProperties(
            this ILogger logger,
            LogLevel level,
            string message,
            params (string key, object value)[] properties) =>
            LogWithProperties(logger, level, message, null, properties);

        public static void LogWithProperties(
            this ILogger logger,
            LogLevel level,
            string message,
            Exception? exception,
            params (string key, object value)[] properties) =>
            logger.LogWithBuilder(level, exception, new LogBuilder(message, properties));
    }
}