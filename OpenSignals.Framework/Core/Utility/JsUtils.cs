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
using System.Text;
using Jayrock.Json;

namespace OpenSignals.Framework.Core.Utility
{
    /// <summary>
    /// Utility class to manage javascript functions and objects
    /// </summary>
    public class JsUtils
    {
        /// <summary>
        /// Utility class to create a JsFunction to be passed to CreateJsFunction method
        /// </summary>
        public class JsFunction
        {
            private string _code = string.Empty;

            /// <summary>
            /// Initializes a new instance of the <see cref="JsFunction"/> class.
            /// </summary>
            /// <param name="code">The code.</param>
            public JsFunction(string code)
            {
                _code = code;
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return _code;
            }
        }

        /// <summary>
        /// Utility class to manage a javascript constant to be passed to CreateJsFunction method
        /// </summary>
        public class JsConstant
        {
            private string _name = string.Empty;

            /// <summary>
            /// Initializes a new instance of the <see cref="JsConstant"/> class.
            /// </summary>
            /// <param name="name">The name.</param>
            public JsConstant(string name)
            {
                _name = name;
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents this instance.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return _name;
            }
        }

        /// <summary>
        /// Creates the js function.
        /// </summary>
        /// <param name="funcName">Name of the func.</param>
        /// <param name="appendReturnFalse">if set to <c>true</c> [append return false].</param>
        /// <returns></returns>
        public static string CreateJsFunction(string funcName, bool appendReturnFalse)
        {
            return CreateJsFunction(funcName, appendReturnFalse, null);
        }

        /// <summary>
        /// Creates the js function.
        /// </summary>
        /// <param name="objName">Name of the obj.</param>
        /// <param name="funcName">Name of the func.</param>
        /// <param name="appendReturnFalse">if set to <c>true</c> [append return false].</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string CreateJsFunction(string objName, string funcName, bool appendReturnFalse, params object[] parameters)
        {
            return CreateJsFunction(objName + "." + funcName, appendReturnFalse, parameters);
        }

        /// <summary>
        /// Creates the js function.
        /// </summary>
        /// <param name="objName">Name of the obj.</param>
        /// <param name="funcName">Name of the func.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string CreateJsFunction(string objName, string funcName, params object[] parameters)
        {
            return CreateJsFunction(objName + "." + funcName, false, parameters);
        }

        /// <summary>
        /// Creates the js function.
        /// </summary>
        /// <param name="funcName">Name of the func.</param>
        /// <param name="appendReturnFalse">if set to <c>true</c> [append return false].</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string CreateJsFunction(string funcName, bool appendReturnFalse, params object[] parameters)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(funcName);

            sb.Append("(");
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != null)
                    {
                        if (parameters[i] is JsFunction)
                        {
                            sb.Append(parameters[i].ToString());
                        }
                        else if (parameters[i] is JsConstant)
                        {
                            sb.Append(parameters[i].ToString());
                        }
                        else if (parameters[i] is JsonObject)
                        {
                            sb.Append(parameters[i].ToString());
                        }
                        else
                        {
                            if (parameters[i] is Boolean)
                                sb.Append(parameters[i].ToString().ToLower());
                            else
                            {
                                sb.Append("'");
                                sb.Append(parameters[i].ToString().Replace(@"\", @"\\"));
                                sb.Append("'");
                            }
                        }
                    }
                    else
                        sb.Append("undefined");

                    if (i != (parameters.Length - 1))
                        sb.Append(",");
                }
            }
            sb.Append(");");

            if (appendReturnFalse)
                sb.Append(" return false;");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the array.
        /// </summary>
        /// <param name="pars">The pars.</param>
        /// <returns></returns>
        public static string CreateArray(List<string[]> pars)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{");

            if (pars != null)
            {
                for (int i = 0; i < pars.Count; i++)
                {
                    sb.Append(pars[i][0]);
                    sb.Append(":\"");
                    sb.Append(pars[i][1]);
                    sb.Append("\"");

                    if (i != (pars.Count - 1))
                        sb.Append(",");
                }
            }
            sb.Append("}");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the script tag.
        /// </summary>
        /// <param name="jsFunction">The js function.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string CreateScriptTag(string jsFunction, params object[] parameters)
        {
            return CreateScriptTag(CreateJsFunction(jsFunction, false, parameters));
        }

        /// <summary>
        /// Creates the script tag.
        /// </summary>
        /// <param name="jsFunctions">The js functions.</param>
        /// <returns></returns>
        public static string CreateScriptTag(string jsFunctions)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"javascript\" type=\"text/javascript\">//<![CDATA[");
            sb.Append(jsFunctions);
            sb.Append("//]]></script>");
            return sb.ToString();
        }

        /// <summary>
        /// Encodes a string to be represented as a string literal. The format
        /// is essentially a JSON string.
        /// 
        /// The string returned includes outer quotes 
        /// Example Output: "Hello \"Rick\"!\r\nRock on"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\'':
                        sb.Append("\'");
                        break;
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append("\"");

            return sb.ToString();
        }
    }
}

