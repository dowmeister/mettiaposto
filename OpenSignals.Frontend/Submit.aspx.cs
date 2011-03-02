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
            RegisterAjaxSessionKey();
            GetCurrentCity();

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

            //string[] parts = GetFromQueryString("city").Split('/');
            //string city = parts[0];
            //string address = string.Empty;
            //if (parts.Length > 1)
            //    address = parts[1];

            //ltCity.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city.ToLower());

            ltCity.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower());

            if (QueryStringContains("address"))
            {
                txtAddress.Text = GetFromQueryString("address");
                RegisterDocumentReadyFunction("autogeo", JsUtils.CreateJsFunction("geolocateByAddress", false, txtAddress.Text, "map"));
            }
        }
    }
}