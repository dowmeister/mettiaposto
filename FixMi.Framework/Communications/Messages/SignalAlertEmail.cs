using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixMi.Framework.Signals;
using FixMi.Framework.Comments;
using System.Net.Mail;
using FixMi.Framework.Core.Utility;
using System.Xml;
using FixMi.Framework.Core;

namespace FixMi.Framework.Communications.Messages
{
    public class SignalAlertEmail : BaseMessage
    {
        public SignalAlertEmail()
        {
            this.Receivers.Add(ConfigurationOptions.Current.GetString("email_sender_address"));
            xslFileName = "SignalAlertBody";
            this.Subject = "Mettiaposto.it: Segnalazione aggiornata";
        }

        public void Send(Comment c)
        {
            SignalManager sm = new SignalManager();
            Signal s = sm.LoadSingnal(c.SignalID);

            this.BccReceivers.Add(new MailAddress(s.Email));

            List<SignalSubscription> subscriptions = sm.GetSubscriptions(c.SignalID);
            
            foreach (SignalSubscription sc in subscriptions)
            {
                MailAddress ma = new MailAddress(sc.Email);
                if (!this.BccReceivers.Contains(ma))
                    this.BccReceivers.Add(ma);
            }

            this.CreateXML(s, c);
            base.Transform();
            base.Send();
        }

        protected void CreateXML(Signal s, Comment c)
        {
            base.CreateXML();
            xmlDocument = XmlUtils.Serialize(s);
            XmlNode comment = xmlDocument.ImportNode(XmlUtils.Serialize(c).SelectSingleNode("/Comment"), true);
            xmlDocument.DocumentElement.AppendChild(comment);
        }
    }
}
