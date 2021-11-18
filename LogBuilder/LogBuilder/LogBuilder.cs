using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LogBuilder
{
    public class LogBuilder : IEnumerable<KeyValuePair<string, object>>
    {    
        private readonly List<LogProperty> _properties = new();
        
        private string _message { get; init; }
        
        public Exception Exception { get; init; }
        
        public LogBuilder(string message)
            : this(message, (Exception) null)
        {
        }
        
        public LogBuilder(string message, Exception exception)
            : this(message, exception, null)
        {
        }
        
        public LogBuilder(string message, IEnumerable<LogProperty> logProperties)
            : this(message, null, logProperties)
        {
        }
        
        public LogBuilder(string message, params LogProperty[] logProperties)
            : this(message, null, logProperties)
        {
        }
        
        public LogBuilder(string message, Exception exception, params LogProperty[] logProperties)
            : this(message, exception, (IEnumerable<LogProperty>)logProperties)
        {
        }
        
        public LogBuilder(string message, Exception exception, IEnumerable<LogProperty> logProperties)
        {
            _message = message;
            Exception = exception;
            
            AppendProperties(logProperties);
        }
      
        public IReadOnlyCollection<LogProperty> GetLogProperties() => _properties.AsReadOnly();
        
        public static Func<LogBuilder, Exception, string> Formatter { get; } =
            (l, e) => l.ToString();

        public override string ToString() => _message;
        
        public LogBuilder WithProperties(params LogProperty[] logArgs)
        {
            AppendProperties(logArgs);
            return this;
        }

        private void AppendProperties(IEnumerable<LogProperty> logArgs)
        {
            if (logArgs == null || !logArgs.Any())
            {
                return;
            }
            
            _properties.AddRange(logArgs);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
            _properties.Select(p => (KeyValuePair<string, object>)p).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}