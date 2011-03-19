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

namespace OpenSignals.Framework.Comments
{
    /// <summary>
    /// This class represents the signal comment.
    /// </summary>
    public class Comment
    {
        private bool _setSignalResolved = false;

        /// <summary>
        /// Gets or sets a value indicating whether [set signal resolved].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [set signal resolved]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool SetSignalResolved
        {
            get { return _setSignalResolved; }
            set { _setSignalResolved = value; }
        }

        /// <summary>
        /// Represents comment status
        /// </summary>
        public struct CommentStatus
        {
            /// <summary>
            /// Not Approved, still to approve.
            /// </summary>
            public const int NotApproved = 0;
            /// <summary>
            /// Approved and published
            /// </summary>
            public const int Approved = 1;
            /// <summary>
            /// Rejected
            /// </summary>
            public const int Reject = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public struct AuthorType
        {
            /// <summary>
            /// 
            /// </summary>
            public const int Anonymous = -1;
            /// <summary>
            /// 
            /// </summary>
            public const int Internal = 0;
            /// <summary>
            /// 
            /// </summary>
            public const int Facebook = 1;
            /// <summary>
            /// 
            /// </summary>
            public const int Twitter = 2;
            /// <summary>
            /// 
            /// </summary>
            public const int OpenID = 3;
            /// <summary>
            /// 
            /// </summary>
            public const int Gravatar = 4;
            /// <summary>
            /// 
            /// </summary>
            public const int Yahoo = 5;
        }

        /// <summary>
        /// Gets or sets the comment ID.
        /// </summary>
        /// <value>
        /// The comment ID.
        /// </value>
        public virtual int CommentID { get; set; }
        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        /// <value>
        /// The name of the author.
        /// </value>
        public virtual string AuthorName { get; set; }
        /// <summary>
        /// Gets or sets the author email.
        /// </summary>
        /// <value>
        /// The author email.
        /// </value>
        public virtual string AuthorEmail { get; set; }
        /// <summary>
        /// Gets or sets the signal ID.
        /// </summary>
        /// <value>
        /// The signal ID.
        /// </value>
        public virtual int SignalID { get; set; }
        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        /// <value>
        /// The attachment.
        /// </value>
        public virtual string Attachment { get; set; }
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public virtual int Status { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show author name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show author name]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowAuthorName { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public virtual string Text { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>
        /// The user ID.
        /// </value>
        public virtual string AuthorReferenceKey { get; set; }
        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public virtual int AuthorReferenceType { get; set; }
       
    }
}
