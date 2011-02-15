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
using System.Xml.Serialization;
using OpenSignals.Framework.Comments;

namespace OpenSignals.Framework.Signals
{
    /// <summary>
    /// 
    /// </summary>
    public class Signal
    {
        public struct SignalStatus
        {
            public const int NotApproved = 0;
            public const int Approved = 1;
            public const int Resolved = 2;            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Signal"/> class.
        /// </summary>
        public Signal() { }

        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets or sets the signal ID.
        /// </summary>
        /// <value>
        /// The signal ID.
        /// </value>
        public virtual int SignalID { get; set; }
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public virtual string Subject { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show name]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowName { get; set; }
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        public virtual DateTime UpdateDate { get; set; }
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>
        /// The category ID.
        /// </value>
        public virtual int CategoryID { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public virtual string Email { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public virtual int Status { get; set; }
        /// <summary>
        /// Gets or sets the resolution date.
        /// </summary>
        /// <value>
        /// The resolution date.
        /// </value>
        public virtual DateTime ResolutionDate { get; set; }
        /// <summary>
        /// Gets or sets the resolution description.
        /// </summary>
        /// <value>
        /// The resolution description.
        /// </value>
        public virtual string ResolutionDescription { get; set; }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public virtual decimal Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public virtual decimal Longitude { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public virtual string Zip { get; set; }
        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        /// <value>
        /// The zoom.
        /// </value>
        public virtual int Zoom { get; set; }
        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        /// <value>
        /// The attachment.
        /// </value>
        public virtual string Attachment { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        [XmlIgnore]
        public virtual IList<Comment> Comments { get; set; }

        /// <summary>
        /// Gets the link.
        /// </summary>
        public virtual string Link
        {
            get
            {
                return "/" + this.City.ToLower() + "/" + this.SignalID.ToString() + "/segnalazione.aspx";
            }
        }

        /// <summary>
        /// Gets the excerpt.
        /// </summary>
        public virtual string Excerpt
        {
            get
            {
                if (this.Description.Length > 100)
                    return this.Description.Substring(0, 99) + "...";
                else
                    return this.Description;
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class SignalSubscription
    {
        /// <summary>
        /// Gets or sets the signal ID.
        /// </summary>
        /// <value>
        /// The signal ID.
        /// </value>
        public virtual int SignalID { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        ///   </exception>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
