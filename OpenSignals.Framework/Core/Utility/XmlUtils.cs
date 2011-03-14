// Copyright (C) 2010-2011 Francesco 'ShArDiCk' Bramato

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OpenSignals.Framework.Core.Utility
{
    /// <summary>
    /// Utility class to mange XML, nodes and documents
    /// </summary>
    public class XmlUtils
    {
        /// <summary>
        /// Serializes the specified object
        /// </summary>
        /// <param name="o">The object to serialize</param>
        /// <returns>Object serialization</returns>
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

        /// <summary>
        /// Creates the XML node.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="doc">The doc.</param>
        /// <returns></returns>
        public static XmlNode CreateXmlNode(string name, string value, XmlDocument doc)
        {
            XmlNode n = doc.AppendChild(doc.CreateNode(XmlNodeType.Element, name, string.Empty));
            n.Value = value;
            return n;

        }
    }
}
