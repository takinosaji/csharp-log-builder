using System;
using Microsoft.Extensions.Logging;

namespace LogBuilder.Extensions
{
    public static partial class LoggerExtensions
    {
        public static void LogInformationWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Information, builder);

        public static void LogDebugWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Debug, builder);

        public static void LogTraceWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Trace, builder);

        public static void LogErrorWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Error, builder);

        public static void LogWarningWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogWithBuilder(LogLevel.Warning, builder);
        
        public static void LogWithBuilder(
            this ILogger logger,
            LogLevel level,
            LogBuilder logBuilder) =>
            logger.Log(level, default, logBuilder, logBuilder.Exception, LogBuilder.Formatter);
    }
}