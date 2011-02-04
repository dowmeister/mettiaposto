using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSignals.Framework.Data;
using NHibernate.Criterion;

namespace OpenSignals.Framework.Comments
{
    public class CommentManager : NHibernateSessionManager
    {
        public List<Comment> GetComments(int signalID, int offset, out int totalCount)
        {
            OpenSession();

            totalCount = session.CreateCriteria(typeof(Comment))
                .Add(Restrictions.Eq("SignalID", signalID))
                .AddOrder(Order.Desc("CreationDate"))
                .Add(Restrictions.Eq("Status", 1))
                .SetProjection(Projections.RowCount())
                .FutureValue<int>().Value;

            List<Comment> comments = session.CreateCriteria(typeof(Comment))
                .Add(Restrictions.Eq("SignalID", signalID))
                .Add(Restrictions.Eq("Status", 1))
                .AddOrder(Order.Desc("CreationDate"))
                .SetMaxResults(offset+5)
                .SetFirstResult(offset)
                .Future<Comment>().ToList();

            CloseSession();

            return comments;
        }

        public int AddComment(Comment c)
        {
            OpenSession();
            OpenTransaction();
            session.Save(c);
            CommitTransaction();
            CloseSession();
            return c.CommentID;
        }
    }
}

