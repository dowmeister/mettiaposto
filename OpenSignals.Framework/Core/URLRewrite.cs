using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using OpenSignals.Framework.Core.Configuration;

namespace OpenSignals.Framework.Core.Configuration
{
    public class RewriteConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("rules", IsRequired = true),
        ConfigurationCollection(typeof(RewriteRuleElement), AddItemName="rule")]
        public RewriteRuleCollection Rules
        {
            get { return (RewriteRuleCollection)this["rules"]; }
        }
    }

    public class RewriteRuleCollection : ConfigurationElementCollection
    {
        public RewriteRuleElement this[int index]
        {
            get { return (RewriteRuleElement)BaseGet(index); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RewriteRuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RewriteRuleElement)element).Url;
        }
    }

    public class RewriteRuleElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return this["url"].ToString(); }
        }

        [ConfigurationProperty("to", IsRequired = true)]
        public string Destination
        {
            get { return this["to"].ToString(); }
        }
    }
}

namespace OpenSignals.Framework.Core
{
    public class RewriteManager
    {
        public static void RewriteUrl()
        {
            RewriteConfigurationSection config = (RewriteConfigurationSection)ConfigurationManager.GetSection("rewrite");

            foreach (RewriteRuleElement rule in config.Rules)
            {
                Regex r = new Regex(rule.Url, RegexOptions.Singleline|RegexOptions.IgnoreCase|RegexOptions.CultureInvariant);
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
    }
}
