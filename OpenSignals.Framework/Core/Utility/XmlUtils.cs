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
using System;

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
        /// Creates the node.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="cData">if set to <c>true</c> [c data].</param>
        /// <param name="doc">The doc.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static XmlNode CreateNode(string name, string value, bool cData, XmlDocument doc, XmlNode node)
        {
            XmlNode n = node.AppendChild(doc.CreateElement(null, name, null));
            if (cData)
                n.AppendChild(doc.CreateCDataSection(value));
            else
                n.InnerXml = value;

            return n;
        }

        /// <summary>
        /// Creates the node.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="doc">The doc.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static XmlNode CreateNode(string name, XmlDocument doc, XmlNode node)
        {
            return CreateNode(name, string.Empty, false, doc, node);
        }

        /// <summary>
        /// Creates the node.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="doc">The doc.</param>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        public static XmlNode CreateNode(string name, string value, XmlDocument doc, XmlNode node)
        {
            return CreateNode(name, value, false, doc, node);
        }

        /// <summary>
        /// Creates the attribute.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="node">The node.</param>
        /// <param name="doc">The doc.</param>
        /// <returns></returns>
        public static XmlAttribute CreateAttribute(string name, string value, XmlNode node, XmlDocument doc)
        {
            XmlAttribute a = doc.CreateAttribute(null, name, null);
            a.InnerXml = value;
            node.Attributes.Append(a);
            return a;
        }

        /// <summary>
        /// Creates the XML document.
        /// </summary>
        /// <returns></returns>
        public static XmlDocument CreateXMLDocument()
        {
            return CreateXMLDocument("iso-8859-1");
        }

        /// <summary>
        /// Creates the XML document.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public static XmlDocument CreateXMLDocument(string encoding)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", encoding, null));
            return doc;
        }

        /// <summary>
        /// Formats the XML.
        /// </summary>
        /// <param name="inputXml">The input XML.</param>
        /// <returns></returns>
        public static string FormatXml(string inputXml)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(new StringReader(inputXml));

                StringBuilder builder = new StringBuilder();

                using (XmlTextWriter writer = new XmlTextWriter(new StringWriter(builder)))
                {
                    writer.Formatting = Formatting.Indented;
                    document.Save(writer);
                }

                return builder.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static object Deserialize(string xml, Type type)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlSerializer xSerializer = new XmlSerializer(type);
            XmlNodeReader nReader = new XmlNodeReader(doc);
            return xSerializer.Deserialize(nReader);
        }
    }
}
