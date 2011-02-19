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
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Configuration;

namespace OpenSignals.Framework.Core.Configuration
{
    /// <summary>
    /// This class manage the rewrite configuration section from web.config
    /// </summary>
    public class RewriteConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the rules.
        /// </summary>
        [ConfigurationProperty("rules", IsRequired = true),
        ConfigurationCollection(typeof(RewriteRuleElement), AddItemName = "rule")]
        public RewriteRuleCollection Rules
        {
            get { return (RewriteRuleCollection)this["rules"]; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RewriteRuleCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets or sets a property, attribute, or child element of this configuration element.
        /// </summary>
        /// <returns>
        /// The specified property, attribute, or child element
        ///   </returns>
        ///   
        /// <exception cref="T:System.Configuration.ConfigurationErrorsException">
        ///   <paramref name="index"/> is read-only or locked.
        ///   </exception>
        public RewriteRuleElement this[int index]
        {
            get { return (RewriteRuleElement)BaseGet(index); }
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new RewriteRuleElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RewriteRuleElement)element).Url;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RewriteRuleElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the URL.
        /// </summary>
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return this["url"].ToString(); }
        }

        /// <summary>
        /// Gets the destination page of rewrited URL
        /// </summary>
        [ConfigurationProperty("to", IsRequired = true)]
        public string Destination
        {
            get { return this["to"].ToString(); }
        }
    }
}

namespace OpenSignals.Framework.Core
{
    /// <summary>
    /// This class manage the rewrite engine
    /// </summary>
    public class RewriteManager : BaseManager
    {
        /// <summary>
        /// Rewrites the URL and redirect the request to destination page from rules in web.config
        /// </summary>
        public static void RewriteUrl()
        {
            RewriteConfigurationSection config = (RewriteConfigurationSection)ConfigurationManager.GetSection("rewrite");

            if (config != null)
            {
                if (config.Rules != null)
                {
                    foreach (RewriteRuleElement rule in config.Rules)
                    {
                        Regex r = new Regex(rule.Url, RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                        Match m = r.Match(HttpContext.Current.Request.Url.AbsolutePath);
                        if (m.Success)
                        {
                            string destinationUrl = rule.Destination;

                            for (int i = 0; i < m.Groups.Count; i++)
                            {
                                if (m.Groups[i].Index > 0)
                                    destinationUrl = destinationUrl.Replace("$" + i.ToString(), m.Groups[i].Value);
                            }

                            HttpContext.Current.RewritePath(destinationUrl + HttpContext.Current.Request.Url.Query);
                        }
                    }
                }
                else
                    throw new Exception("Cannot find <rules> node in web.config");
            }
            else
                throw new Exception("Cannot find <rewrite> node in web.config");
        }
    }
}
