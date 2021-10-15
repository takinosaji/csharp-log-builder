using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LogBuilder
{
    public class LogBuilder : IEnumerable<KeyValuePair<string, object>>
    {    
        private readonly List<LogProperty> _logProperties = new();
        
        private string _message { get; }
        
        public LogBuilder(string message)
        {
            _message = message;
        }
        
        public LogBuilder(string message, params LogProperty[] logProperties)
        {
            _message = message;
            AppendProperties(logProperties);
        }
        
        public LogBuilder(string message, IEnumerable<LogProperty> logProperties)
        {
            _message = message;
            AppendProperties(logProperties);
        }
        
        public IEnumerable<LogProperty> GetLogPropertiesIterator()
        {
            foreach (var property in _logProperties)
            {
                yield return property;
            }
        }

        public IReadOnlyCollection<LogProperty> GetLogProperties() => _logProperties;
        
        public static Func<LogBuilder, Exception, string> Formatter { get; } =
            (l, e) => l.ToString();

        public override string ToString() => _message;
        
        public LogBuilder WithProperties(params LogProperty[] logArgs)
        {
            AppendProperties(logArgs);
            return this;
        }
        
        private void AppendProperties(IEnumerable<LogProperty> logArgs) =>
            _logProperties.AddRange(logArgs);

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
            _logProperties.Select(p => (KeyValuePair<string, object>)p).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}