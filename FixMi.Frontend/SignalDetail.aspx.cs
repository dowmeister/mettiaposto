using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Core.Base;
using FixMi.Framework.Signals;
using FixMi.Framework.Categories;

namespace FixMi.Frontend
{
    public partial class SignalDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RenderPage();
            }
        }

        private void RenderPage()
        {
            if (QueryStringContains("id"))
            {
                SignalManager sm = new SignalManager();
                Signal s = sm.LoadSingnal(int.Parse(GetFromQueryString("id")));
                divTitle.InnerHtml = s.Subject;
                divDescription.InnerHtml = s.Description;

                if (s.ShowName)
                    ltAuthor.Text = "Anonimo";
                else
                    ltAuthor.Text = s.Name;

                TimeSpan ts = DateTime.Now.Subtract(s.CreationDate);
                ltTimeFrame.Text = ts.Minutes.ToString() + " minuti";
                CategoryManager cm = new CategoryManager();
                ltCategory.Text = cm.Load(s.CategoryID).Name;
            }
        }
    }
}