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
    /// 
    /// </summary>
    public class Comment
    {
        private bool _setSignalResolved = false;

        /// <summary>
        /// 
        /// </summary>
        public virtual bool SetSignalResolved
        {
            get { return _setSignalResolved; }
            set { _setSignalResolved = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public struct CommentStatus
        {
            public const int NotApproved = 0;
            public const int Approved = 1;
            public const int Reject = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int CommentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string AuthorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string AuthorEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int SignalID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Attachment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool ShowAuthorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Text { get; set; }
       
    }
}
