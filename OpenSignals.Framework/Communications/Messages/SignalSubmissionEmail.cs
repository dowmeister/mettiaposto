using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Framework.Communications.Messages
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
