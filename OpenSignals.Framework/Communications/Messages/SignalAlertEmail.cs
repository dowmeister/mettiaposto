using System.Collections.Generic;
using System.Net.Mail;
using System.Xml;
using OpenSignals.Framework.Comments;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Framework.Communications.Messages
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
