using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Categories;

namespace OpenSignals.Frontend.m
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAjaxSessionKey();

            CategoryManager cm = new CategoryManager();
            ddlCategories.DataSource = cm.GetActive();
            ddlCategories.DataBind();
        }
    }
}