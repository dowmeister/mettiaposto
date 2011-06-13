using System;
using System.IO;
using System.Xml;
using OpenSignals.Framework.Core.Base;

namespace OpenSignals.Frontend
{
    public partial class Content : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckTheBeta();

            if (IsMobileBrowser())
                Server.Transfer("/m/Default.aspx");
            
            RegisterAjaxSessionKey();
            InitClientObjects();

            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath(Path.Combine("/Contents/", GetFromQueryString("page") + ".xml")));
            this.Title = String.Format(this.Title, xml.SelectSingleNode("/page/title").InnerText);
            divContent.InnerHtml = xml.SelectSingleNode("/page/content").InnerText;
            ucHead.MetaDescription = xml.SelectSingleNode("/page/description").InnerText;
            ucHead.MetaKeywords = xml.SelectSingleNode("/page/keywords").InnerText;
        }
    }
}