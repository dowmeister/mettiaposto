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

namespace OpenSignals.Framework.Signals
{
    /// <summary>
    /// 
    /// </summary>
    public class SignalUtils
    {
        /// <summary>
        /// Gets the timeframe.
        /// </summary>
        /// <param name="creationDate">The creation date.</param>
        /// <returns></returns>
        public static string GetTimeframe(DateTime creationDate)
        {
            TimeSpan ts = DateTime.Now.Subtract(creationDate);

            if (ts.Days > 60)
            {
                return "circa 1 mese fa (il " + creationDate.ToShortDateString() + ")";
            }

            if (ts.Days > 30)
            {
                return "circa " + (ts.Days / 30).ToString() + " mesi fa (il " + creationDate.ToShortDateString() + ")";
            }

            if (ts.Days > 1)
            {
                return ts.Days.ToString() + " giorni fa alle " + creationDate.ToShortTimeString();
            }

            if (ts.Days == 1)
            {
                return "ieri alle " + creationDate.ToShortTimeString();
            }

            if (ts.Days == 0 && ts.Hours > 1)
            {
                return ts.Hours.ToString() + " ore fa e " + ts.Minutes.ToString() + " minuti fa";
            }

            if (ts.Days == 0 && ts.Hours == 1)
            {
                return "1 ora e " + ts.Minutes.ToString() + " minuti fa";
            }

            if (ts.Minutes <= 1)
                return "circa 1 minuto fa";

            if (ts.Minutes > 1)
                return ts.Minutes.ToString() + " minuti fa";

            return "pochi secondi fa";
        }

    }
}
