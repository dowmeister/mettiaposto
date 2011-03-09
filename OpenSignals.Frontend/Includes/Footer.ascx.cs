using System;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Places;

namespace OpenSignals.Frontend.Includes
{
    public partial class Footer : System.Web.UI.UserControl
    {
        private bool _checkCity = true;

        public bool CheckCity
        {
            get { return _checkCity; }
            set { _checkCity = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (_checkCity)
            {
                Place currentCity = ((BasePage)Page).CurrentCity;
                linkSignal.HRef = currentCity.Link + "invia.aspx";
                linkRss.HRef = currentCity.Link + "rss.aspx";
                linkLogo.HRef = currentCity.Link + "index.aspx";
            }
        }
    }
}