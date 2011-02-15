using System;
using System.Web.UI;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Places;
using OpenSignals.Framework.Signals;

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