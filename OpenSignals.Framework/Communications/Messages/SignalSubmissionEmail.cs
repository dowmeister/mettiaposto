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
