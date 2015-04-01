using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebFrame.Logging
{
    [Serializable]
    public class CustomLog
    {
        public CustomLog()
        {
            Parameters = new List<LogParameter>();
        }
        public string OperationName { get; set; }
        public Type ReturnType { get; set; }
        object _ReturnValue;

        public object ReturnValue
        {
            get { return _ReturnValue; }
            set { _ReturnValue = value; }
        }
        // public object ReturnValue { get; set; }
        public IList<LogParameter> Parameters { get; set; }
        public string Kullanici { get; set; }

    }
}
