using System.Collections.Generic;

namespace LogBuilder
{
    public record LogProperty
    {
        public string Key { get; }

        public object Value { get; }

        public int BatchSize { get; }

        public LogProperty(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public LogProperty(string key, object value, int batchSize)
        {
            Key = key;
            Value = value;
            BatchSize = batchSize;
        }
        
        public static explicit operator KeyValuePair<string, object>(LogProperty logProperty)
        {
            return new KeyValuePair<string, object>(logProperty.Key, logProperty.Value);
        }
    }
}