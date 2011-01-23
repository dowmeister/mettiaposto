using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Core.Base;
using System.IO;
using System.Xml;

namespace FixMi.Frontend
{
    public partial class Content : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Server.MapPath(Path.Combine("/Contents/", GetFromQueryString("page") + ".xml")));
            this.Title = String.Format(this.Title, xml.SelectSingleNode("/page/title").InnerText);
            divContent.InnerHtml = xml.SelectSingleNode("/page/content").InnerText;
            ucHead.MetaDescription = xml.SelectSingleNode("/page/description").InnerText;
            ucHead.MetaKeywords = xml.SelectSingleNode("/page/keywords").InnerText;
        }

        //protected override void OnPreInit(EventArgs e)
        //{
        //    this.Theme = "Default";//" + GetFromQueryString("page") + ".skin";

        //    base.OnPreInit(e);
        //}
    }
}