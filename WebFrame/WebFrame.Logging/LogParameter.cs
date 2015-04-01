using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebFrame.Logging
{
    [Serializable]
    public class LogParameter
    {
        public string ParameterTypeName { get; set; }
        public object ParameterValue { get; set; }
        public string ParameterName { get; set; }
        public Type ParameterType { get; set; }
    }
}
