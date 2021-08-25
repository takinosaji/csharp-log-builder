using System;
using Microsoft.Extensions.Logging;

namespace LogBuilder
{
    public static partial class LoggerExtensions
    {
        public static void LogInformationWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Information, null, builder);

        public static void LogDebugWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Debug, null, builder);

        public static void LogTraceWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Trace, null, builder);

        public static void LogErrorWithBuilder(
            this ILogger logger,
            Exception exception,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Error, exception, builder);

        public static void LogErrorWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Error, null, builder);

        public static void LogWarningWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Warning, null, builder);
        
        public static void LogWithBuilder(
            this ILogger logger,
            LogLevel level,
            Exception? exception,
            LogBuilder logBuilder) =>
            logger.Log(level, default, logBuilder, exception, LogBuilder.Formatter);
    }
}