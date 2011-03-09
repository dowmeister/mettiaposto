using System;
using OpenSignals.Framework.Categories;
using OpenSignals.Framework.Core.Base;

namespace OpenSignals.Frontend.Mobile
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