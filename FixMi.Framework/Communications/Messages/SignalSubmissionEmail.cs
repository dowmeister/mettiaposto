using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixMi.Framework.Signals;
using FixMi.Framework.Core.Utility;
using FixMi.Framework.Core;

namespace FixMi.Framework.Communications.Messages
{
    public class SignalSubmissionEmail : BaseMessage
    {
        public SignalSubmissionEmail()
        {
            this.Receivers.Add(ConfigurationOptions.Current.GetString("signal_submission_receiver_address"));
            xslFileName = "SignalSubmissionBody";
            this.Subject = "Mettiaposto.it: Nuova segnalazione inserita";
        }

        public void Send(Signal s)
        {
            this.CreateXML(s);
            base.Transform();
            base.Send();
        }

        protected void CreateXML(Signal s)
        {
            base.CreateXML();
            xmlDocument = XmlUtils.Serialize(s);
        }
    }
}
