using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WebFrame.DataType.Common.CustomTypes
{

    [Serializable()]
    [XmlRoot(ElementName = "")]
   public class ContextMessageHeader
    {       
        [XmlElement()]
        public string UserIP;
        [XmlElement()]
        public string IssName;       
        [XmlElement()]
        public string CurrentPageURL;      
        [XmlElement()]
        public string UserName;
        [XmlElement()]
        public string DomainUserName;      
        [XmlElement()]
        public string UserTCKN;

    }
}
