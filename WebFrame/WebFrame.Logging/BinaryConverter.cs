using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebFrame.Logging
{
    public class BinaryConverter : ContextBoundObject
    {
        public object CObject { get; set; }
        public byte[] CBytes { get; set; }
        public object BinaryToObject(byte[] bitler)
        {
            using (MemoryStream ms = new MemoryStream(bitler))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                ms.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(ms);
            }
        }
        public byte[] ObjectToBinary(object o)
        {
            if (o == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, o);
                return ms.ToArray();
            }
        }
    }
}
