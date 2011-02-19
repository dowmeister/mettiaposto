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
using System.Collections.Generic;
using System.Net.Mail;
using System.Xml;
using OpenSignals.Framework.Comments;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;
using OpenSignals.Framework.Signals;

namespace OpenSignals.Framework.Communications.Messages
{
    /// <summary>
    /// Alert communication: this email is sent to signal owner, admin and subscription users when an action is performed on signal (comment, set as resolved)
    /// </summary>
    public class SignalAlertEmail : BaseMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignalAlertEmail"/> class.
        /// </summary>
        /// <remarks>
        /// This communication is sent to signal owner, admin email and subscription users on signal comment.
        /// </remarks>
        public SignalAlertEmail()
        {
            this.Receivers.Add(ConfigurationOptions.Current.GetString("email_sender_address"));
            xslFileName = "SignalAlertBody";
            this.Subject = "Mettiaposto.it: Segnalazione aggiornata";
        }

        /// <summary>
        /// Sends the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        public void Send(Comment c)
        {
            try
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
            catch (Exception ex)
            {
                log.Error("Error sending alert communication", ex);
            }
        }

        /// <summary>
        /// Creates the XML.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="c">The c.</param>
        protected void CreateXML(Signal s, Comment c)
        {
            base.CreateXML();
            xmlDocument = XmlUtils.Serialize(s);
            XmlNode comment = xmlDocument.ImportNode(XmlUtils.Serialize(c).SelectSingleNode("/Comment"), true);
            xmlDocument.DocumentElement.AppendChild(comment);
        }
    }
}
