using System;
using System.Collections.Immutable;
using Microsoft.Extensions.Logging;

namespace LogBuilder.Extensions
{
    public static partial class LoggerExtensions
    {
        public static void LogInformationBatchesWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogBatchesWithBuilder(LogLevel.Information, null, builder);

        public static void LogDebugBatchesWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogBatchesWithBuilder(LogLevel.Debug, null, builder);

        public static void LogTraceBatchesWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogBatchesWithBuilder(LogLevel.Trace, null, builder);

        public static void LogErrorBatchesWithBuilder(
            this ILogger logger,
            Exception exception,
            LogBuilder builder) =>
            logger.LogBatchesWithBuilder(LogLevel.Error, exception, builder);

        public static void LogErrorBatchesWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogBatchesWithBuilder(LogLevel.Error, null, builder);

        public static void LogWarningBatchesWithBuilder(
            this ILogger logger,
            LogBuilder builder) =>
            logger.LogBatchesWithBuilder(LogLevel.Warning, null, builder);

        public static void LogBatchesWithBuilder(
            this ILogger logger,
            LogLevel logLevel,
            Exception? exception,
            LogBuilder logBuilder) =>
            logger.LogWithBatches(
                logLevel, 
                logBuilder.ToString(),
                exception,
                logBuilder.GetLogProperties());
    }
}