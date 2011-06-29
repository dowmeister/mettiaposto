using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Places;
using System.Web.Routing;

namespace OpenSignals.Frontend.Includes
{
    public partial class Header : System.Web.UI.UserControl
    {
        private bool _checkCity = true;
        private bool _hideTabs = false;

        public bool HideTabs
        {
            get { return _hideTabs; }
            set { _hideTabs = value; }
        }

        public bool CheckCity
        {
            get { return _checkCity; }
            set { _checkCity = value; }
        }

        private int _selectedTab = 1;

        public int SelectedTab
        {
            get { return _selectedTab; }
            set { _selectedTab = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            tabs.Visible = !_hideTabs;

            if (!_hideTabs)
            {
                switch (_selectedTab)
                {
                    case 1:
                        link1.Attributes["class"] = "homeOn";
                        break;
                    case 2:
                    case 3:
                    case 4:
                        ((HtmlGenericControl)FindControl("link" + _selectedTab.ToString())).Attributes["class"] = "tabOn";
                        break;
                }
            }

            //citiesMenu.Visible = _checkCity;

            if (_checkCity)
            {
                PlaceManager pm = new PlaceManager();
                Place currentCity = ((BasePage)Page).CurrentCity;
                //currentCityAnchor.InnerHtml = currentCity.Name;
                //currentCityAnchor.HRef = currentCity.Link + "index.aspx";

                //linkSearch.HRef = currentCity.Link + "cerca.aspx";
                //linkHome.HRef = currentCity.Link + "index.aspx";
                //linkSignal.HRef = currentCity.Link + "invia.aspx";
                //linkRss.HRef = currentCity.Link + "rss.aspx";
                //geoRSSLink.HRef = currentCity.Link + "georss.aspx";
                //linkLogo.HRef = currentCity.Link + "index.aspx";

                linkSearch.HRef = ((BasePage)Page).GetRouteUrl("search", new { place = ((BasePage)Page).RouteData.Values["place"] }); // currentCity.Link + "cerca.aspx";
                linkHome.HRef = currentCity.Link + "index.aspx";
                linkSignal.HRef = currentCity.Link + "invia.aspx";
                linkRss.HRef = currentCity.Link + "rss.aspx";
                geoRSSLink.HRef = currentCity.Link + "georss.aspx";
                linkLogo.HRef = currentCity.Link + "index.aspx";

                ltCurrentCity.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(currentCity.Name);

                //List<Place> places = pm.GetActivePlaces();
                //places.Remove(currentCity);
                //rptCities.DataSource = places;
                //rptCities.DataBind();
            }
        }
    }
}