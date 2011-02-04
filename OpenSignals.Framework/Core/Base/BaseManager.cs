using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using OpenSignals.Framework.Data;

namespace OpenSignals.Framework.Core.Base
{
    public class BaseManager
    {
        private ILog _log = null;

        protected ILog log
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetLogger("System");

                return _log;
            }
        }
    }
}
