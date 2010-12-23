using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixMi.Framework.Comments
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public int SignalID { get; set; }
        public string Attachment { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public bool ShowAuthorName { get; set; }
    }
}
