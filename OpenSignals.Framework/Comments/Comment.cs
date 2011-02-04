using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSignals.Framework.Comments
{
    public class Comment
    {
        private bool _setSignalResolved = false;

        public virtual bool SetSignalResolved
        {
            get { return _setSignalResolved; }
            set { _setSignalResolved = value; }
        }

        public struct CommentStatus
        {
            public const int NotApproved = 0;
            public const int Approved = 1;
            public const int Reject = -1;
        }

        public virtual int CommentID { get; set; }
        public virtual string AuthorName { get; set; }
        public virtual string AuthorEmail { get; set; }
        public virtual int SignalID { get; set; }
        public virtual string Attachment { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual int Status { get; set; }
        public virtual bool ShowAuthorName { get; set; }
        public virtual string Text { get; set; }
       
    }
}
