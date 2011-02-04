using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Xml;
using System.Xml.Xsl;
using System.Web;
using System.IO;
using OpenSignals.Framework.Core;

namespace OpenSignals.Framework.Communications
{
    public class BaseMessage
    {
        private MailAddressCollection _receivers = new MailAddressCollection();
        private MailAddressCollection _bccReceivers = new MailAddressCollection();
        private MailAddress _sender = new MailAddress(ConfigurationOptions.Current.GetString("email_sender_address"), ConfigurationOptions.Current.GetString("email_sender_name"));
        private string _subject = string.Empty;
        private string body = string.Empty;
        protected string xslFileName = string.Empty;
        protected XmlDocument xmlDocument = new XmlDocument();

        public MailAddressCollection Receivers
        {
            get { return _receivers; }
            set { _receivers = value; }
        }

        public MailAddressCollection BccReceivers
        {
            get { return _bccReceivers; }
            set { _bccReceivers = value; }
        }

        public MailAddress Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public virtual void Send()
        {
            _Send();
        }

        private void _Send()
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

        protected void Transform()
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

        protected virtual void CreateXML()
        {
            this.xmlDocument = new XmlDocument();
            this.xmlDocument.AppendChild(this.xmlDocument.CreateXmlDeclaration("1.0", "iso-8859-1", null));
            this.xmlDocument.PreserveWhitespace = true;
        }
    }
}
