using System;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Places;

namespace OpenSignals.Frontend.Includes
{
    public partial class Footer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Place currentCity = ((BasePage)Page).CurrentCity;
            linkSignal.HRef = currentCity.Link + "invia.aspx";
            linkRss.HRef = currentCity.Link + "rss.aspx";
            linkLogo.HRef = currentCity.Link + "index.aspx";
        }
    }
}