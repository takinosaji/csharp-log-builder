using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LogBuilder.Extensions;

namespace LogBuilder
{
    internal static class BatchedLogPropertiesFactory
    {
        public static IReadOnlyCollection<IReadOnlyCollection<LogProperty>> CreateBatches(
            IEnumerable<LogProperty> properties)
        {
            var propertyArray = properties.ToArray();
            var commonProperties = new List<LogProperty>();
            var batchableProperties = new List<(string key, IEnumerable<object> batches)>();
            int maxBatchesCount = default;
            
            foreach (var property in propertyArray)
            {
                var batchableProperty = property.Value as IEnumerable<object>;
                if (property.BatchSize == 0 || batchableProperty is null)
                {
                    commonProperties.Add(property);
                    continue;
                }

                var batchablePropertyArray = batchableProperty.Batch(property.BatchSize).ToImmutableArray();
                batchableProperties.Add((property.Key, batches: batchablePropertyArray));
                if (maxBatchesCount < batchablePropertyArray.Length)
                {
                    maxBatchesCount = batchablePropertyArray.Length;
                }
            }

            return !batchableProperties.Any() 
                ? new []{ propertyArray } 
                : ComposeAllProperties(maxBatchesCount, commonProperties, batchableProperties);
        }

        private static IReadOnlyCollection<IReadOnlyCollection<LogProperty>> ComposeAllProperties(
            int maxBatchesCount,
            IReadOnlyCollection<LogProperty> commonProperties,
            IReadOnlyCollection<(string key, IEnumerable<object> batches)> batchableProperties)
        {
            var batchedLogProperties = new List<List<LogProperty>>();

            for (var batchToTake = 0; batchToTake < maxBatchesCount; batchToTake++)
            {
                var propertiesPerBatch = batchableProperties
                    .Where(p => p.batches.Count() >= batchToTake + 1)
                    .Select(p => new LogProperty(p.key, p.batches.ElementAt(batchToTake)));

                var batchedLogProperty = commonProperties
                    .Concat(propertiesPerBatch)
                    .ToList();

                batchedLogProperties.Add(batchedLogProperty);
            }

            return batchedLogProperties;
        }
    }
}