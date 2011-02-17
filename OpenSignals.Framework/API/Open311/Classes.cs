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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSignals.Framework.API.Open311
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceType
    {
        private string _serviceCode = string.Empty;

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public string ServiceCode
        {
            get { return _serviceCode; }
            set { _serviceCode = value; }
        }
        private string _serviceName = string.Empty;

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }
        private string _serviceDescription = string.Empty;

        /// <summary>
        /// Gets or sets the service description.
        /// </summary>
        /// <value>
        /// The service description.
        /// </value>
        public string ServiceDescription
        {
            get { return _serviceDescription; }
            set { _serviceDescription = value; }
        }
    }
}
