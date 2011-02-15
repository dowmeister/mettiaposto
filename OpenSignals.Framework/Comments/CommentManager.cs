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

using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Comments
{
    /// <summary>
    /// 
    /// </summary>
    public class CommentManager : NHibernateSessionManager
    {
        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="signalID">The signal ID.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
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

