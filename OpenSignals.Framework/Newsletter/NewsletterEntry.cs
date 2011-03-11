using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSignals.Framework.Newsletter
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsletterEntry
    {
        /// <summary>
        /// Gets or sets the subscription ID.
        /// </summary>
        /// <value>
        /// The subscription ID.
        /// </value>
        public virtual int SubscriptionID { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public virtual string Email { get; set; }
    }
}
