using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebFrame.DataType.Common.Enums;



namespace WebFrame.DataType.Common.Attributes

{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceConnectionNameAttribute : Attribute
    {
        public string ServiceConnectionName;
        public ServiceConnectionNameAttribute(string name)
        {
            ServiceConnectionName = name;
        }

        //DataProvider dataProvider = DataProvider.Oracle;
        DataProvider dataProvider = DataProvider.SqlServer;

        public DataProvider DataProvider
        {
            get { return dataProvider; }
            set { dataProvider = value; }
        }

    }
}
