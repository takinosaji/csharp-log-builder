using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LogBuilder
{
    public class LogBuilder : IEnumerable<KeyValuePair<string, object>>
    {    
        private readonly List<KeyValuePair<string, object>> _logProperties = new();
        
        private string _message { get; }
        
        public LogBuilder(string message)
        {
            _message = message;
        }
        
        public LogBuilder(string message, params (string name, object value)[] logProperties)
        {
            _message = message;
            AppendProperties(logProperties);
        }
        
        public static Func<LogBuilder, Exception?, string> Formatter { get; } =
            (l, e) => l._message;
        
        public LogBuilder WithProperties(params (string name, object value)[] logArgs)
        {
            AppendProperties(logArgs);
            return this;
        }
        
        private void AppendProperties(params (string name, object value)[] logArgs) =>
            _logProperties.AddRange(logArgs
                .Select(item => new KeyValuePair<string, object>(
                    item.name,
                    item.value)));

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
            _logProperties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}