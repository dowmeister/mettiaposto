using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace OpenSignals.Framework.Core.Utility
{
    public class XmlUtils
    {
        public static XmlDocument Serialize(object o)
        {
            XmlDocument xDocument = null;
            XmlSerializer xSerializer = new XmlSerializer(o.GetType());
            
            xDocument = new XmlDocument();
            
            using (MemoryStream ms = new MemoryStream())
            {
                xSerializer.Serialize(ms, o);
                ms.Position = 0;
                byte[] b = new byte[ms.Length];
                ms.Read(b, 0, (int)ms.Length);
                xDocument.LoadXml(Encoding.UTF8.GetString(b));
            }
            
            return xDocument;
        }
    }
}
