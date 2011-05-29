using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;
using System.Xml;

namespace OpenSignals.Framework.Communications.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class AbuseReportEmail : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackEmail"/> class.
        /// </summary>
        public AbuseReportEmail()
        {
            this.Sender = new System.Net.Mail.MailAddress(ConfigurationOptions.Current.GetString("email_sender_address"));
            this.Receivers.Add(ConfigurationOptions.Current.GetString("email_sender_address"));
            xslFileName = "AbuseReportBody";
            this.Subject = "Mettiaposto.it: Segnalazione contenuto non corretto";
        }

        /// <summary>
        /// Sends the specified name.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="message">The message.</param>
        public void Send(string url, string message)
        {
            try
            {
                this.CreateXML(url, message);
                base.Transform();
                base.Send();
            }
            catch (Exception ex)
            {
                log.Error("Error sending alert communication", ex);
                throw ex;
            }
        }

        private void CreateXML(string url, string message)
        {
            base.CreateXML();
            XmlNode root = XmlUtils.CreateNode("Feedback", xmlDocument, xmlDocument);
            XmlUtils.CreateNode("URL", url, xmlDocument, root);
            XmlUtils.CreateNode("Message", message, xmlDocument, root);
        }
    }
}
