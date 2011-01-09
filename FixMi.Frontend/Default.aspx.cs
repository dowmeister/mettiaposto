using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Signals;
using FixMi.Framework.Core.Base;
using FixMi.Framework.Places;

namespace FixMi.Frontend
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAjaxSessionKey();

            if (!Page.IsPostBack)
                BuildPage();
        }

        private void BuildPage()
        {
            SignalManager sm = new SignalManager();
            ltTotals.Text = sm.GetCountAll().ToString();
            ltResolved.Text = sm.GetCountByStatus(Signal.SignalStatus.Resolved).ToString();

            PlaceManager pm = new PlaceManager();
            ddlCities.DataSource = pm.GetActivePlaces();
            ddlCities.DataBind();
        }
    }
}