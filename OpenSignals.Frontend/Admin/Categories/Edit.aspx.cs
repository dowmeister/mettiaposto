using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Categories;

namespace OpenSignals.Frontend.Admin.Categories
{
    public partial class Edit : BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BuildPage();
            }
        }

        private void BuildPage()
        {
            if (QueryStringContains("ID"))
            {
                CategoryManager cm = new CategoryManager();
                Category c = cm.Load(int.Parse(GetFromQueryString("ID")));
                txtName.Text = c.Name;
                chkActive.Checked = c.Status;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CategoryManager cm = new CategoryManager();
            Category c = new Category();
            c.Name = txtName.Text;
            c.Status = chkActive.Checked;

            if (QueryStringContains("ID"))
            {
                c.CategoryID = int.Parse(GetFromQueryString("ID"));
                cm.Update(c);
            }
            else
                cm.Create(c);

            Response.Redirect("Edit.aspx?ID=" + c.CategoryID);
        }
    }
}