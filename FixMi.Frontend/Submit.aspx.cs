using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Categories;
using log4net;
using log4net.Core;
using FixMi.Framework.Core.Base;
using FixMi.Framework;

namespace FixMi.Frontend
{
    public partial class Submit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                RenderPage();
        }

        private void RenderPage()
        {
            CategoryManager cm = new CategoryManager();
            ddlCategories.DataSource = cm.GetActive();
            ddlCategories.DataBind();

            if (QueryStringContains("address"))
            {
                RegisterDocumentReadyFunction("autogeo", JsUtils.CreateJsFunction("geolocationByAddress", false, GetFromQueryString("address"), "map_canvas"));
            }
        }
    }
}