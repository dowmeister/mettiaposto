using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Signals;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Places;
using OpenSignals.Framework.Core;

namespace OpenSignals.Frontend
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