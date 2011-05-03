using System;
using System.Globalization;
using System.Web.UI;
using OpenSignals.Framework.Categories;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Frontend
{
    public partial class Submit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsMobileBrowser())
                Server.Transfer("/m/Default.aspx");

            RegisterAjaxSessionKey();
            InitClientObjects();

            metaOgDescription.Attributes["content"] = String.Format(metaOgDescription.Attributes["content"], CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower()));
            ogTitle.Attributes["content"] = String.Format(ogTitle.Attributes["content"], CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower()));
            this.Title = String.Format(this.Title, CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower()));

            if (!Page.IsPostBack)
                RenderPage();
        }

        private void RenderPage()
        {
            CategoryManager cm = new CategoryManager();
            ddlCategories.DataSource = cm.GetActive();
            ddlCategories.DataBind();

            ltCity.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower());

            if (QueryStringContains("address"))
                txtAddress.Text = GetFromQueryString("address");
        }
    }
}