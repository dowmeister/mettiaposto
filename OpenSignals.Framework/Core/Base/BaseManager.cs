﻿// Copyright (C) 2010-2011 Francesco 'ShArDiCk' Bramato

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

using log4net;
using System.Web;

namespace OpenSignals.Framework.Core.Base
{
    /// <summary>
    /// This class repesents the base class to inheritate from when using businnes logic classes. 
    /// </summary>
    public class BaseManager
    {
        private ILog _log = null;

        /// <summary>
        /// Gets the log.
        /// </summary>
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

    /// <summary>
    /// This class is used by Global class
    /// </summary>
    public class BaseApplication : HttpApplication
    {
        private ILog _log = null;

        /// <summary>
        /// Gets the log.
        /// </summary>
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
