using System;
using System.Collections.Generic;
using System.Text;

namespace FixMi.Framework
{
    public class JsUtils
    {
        public class JsFunction
        {
            private string _code = string.Empty;

            public JsFunction(string code)
            {
                _code = code;
            }

            public override string ToString()
            {
                return _code;
            }

        }
        public static string CreateJsFunction(string funcName, bool appendReturnFalse)
        {
            return CreateJsFunction(funcName, appendReturnFalse, null);
        }

        public static string CreateJsFunction(string objName, string funcName, bool appendReturnFalse, params object[] parameters)
        {
            return CreateJsFunction(objName + "." + funcName, appendReturnFalse, parameters);
        }

        public static string CreateJsFunction(string objName, string funcName, params object[] parameters)
        {
            return CreateJsFunction(objName + "." + funcName, false, parameters);
        }

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

        public static string CreateScriptTag(string jsFunction, params object[] parameters)
        {
            return CreateScriptTag(CreateJsFunction(jsFunction, false, parameters));
        }

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

