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

using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Framework.Communications
{
    /// <summary>
    /// This class represents base class for communications. Implements common method and properties to configure each communication
    /// </summary>
    public class BaseMessage : BaseManager
    {
        private MailAddressCollection _receivers = new MailAddressCollection();
        private MailAddressCollection _bccReceivers = new MailAddressCollection();
        private MailAddress _sender = new MailAddress(ConfigurationOptions.Current.GetString("email_sender_address"), ConfigurationOptions.Current.GetString("email_sender_name"));
        private string _subject = string.Empty;
        private string body = string.Empty;

        /// <summary>
        /// XSL File name
        /// </summary>
        protected string xslFileName = string.Empty;
        /// <summary>
        /// XML Document
        /// </summary>
        protected XmlDocument xmlDocument = new XmlDocument();

        /// <summary>
        /// Gets or sets the receivers.
        /// </summary>
        /// <value>
        /// The receivers.
        /// </value>
        public MailAddressCollection Receivers
        {
            get { return _receivers; }
            set { _receivers = value; }
        }

        /// <summary>
        /// Gets or sets the BCC receivers.
        /// </summary>
        /// <value>
        /// The BCC receivers.
        /// </value>
        public MailAddressCollection BccReceivers
        {
            get { return _bccReceivers; }
            set { _bccReceivers = value; }
        }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public MailAddress Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// Sends the communication.
        /// </summary>
        public virtual void Send()
        {
            try
            {
                MailMessage m = new MailMessage();
                m.Sender = _sender;
                m.From = _sender;

                foreach (MailAddress ma in _receivers)
                {
                    m.To.Add(ma);
                }

                foreach (MailAddress ma in _bccReceivers)
                {
                    m.Bcc.Add(ma);
                }

                m.Subject = _subject;
                m.Body = body;
                m.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Send(m);
            }
            catch (System.Exception ex)
            {
                LogUtils.Log("Error sending communication", ex);
            }            
        }

        /// <summary>
        /// Transforms XML via XSL to create the communication body
        /// </summary>
        protected void Transform()
        {
            try
            {
                XslCompiledTransform t = new XslCompiledTransform();
                t.Load(HttpContext.Current.Server.MapPath(Path.Combine("/Contents/XSL/", this.xslFileName + ".xsl")));

                StringBuilder sb = new StringBuilder();

                XmlWriterSettings xws = new XmlWriterSettings();
                xws.ConformanceLevel = ConformanceLevel.Auto;
                xws.CheckCharacters = false;

                XmlWriter xw = XmlWriter.Create(sb, xws);
                t.Transform(xmlDocument, xw);
                body = sb.ToString();
                xw.Close();
            }
            catch (Exception ex)
            {
                LogUtils.Log("Error transforming XML", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Creates the XML.
        /// </summary>
        protected virtual void CreateXML()
        {
            this.xmlDocument = new XmlDocument();
            this.xmlDocument.AppendChild(this.xmlDocument.CreateXmlDeclaration("1.0", "iso-8859-1", null));
            this.xmlDocument.PreserveWhitespace = true;
        }
    }
}
