using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixMi.Framework.Signals
{
    public class SignalUtils
    {
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
