using log4net;

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
