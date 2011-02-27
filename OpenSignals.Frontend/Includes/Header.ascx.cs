using System;
using System.Web.UI.HtmlControls;

namespace OpenSignals.Frontend.Includes
{
    public partial class Header : System.Web.UI.UserControl
    {
        private int _selectedTab = 1;

        public int SelectedTab
        {
            get { return _selectedTab; }
            set { _selectedTab = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (_selectedTab)
            {
                case 1:
                    link1.Attributes["class"] = "homeOn";
                    break;
                case 2:
                    ((HtmlGenericControl)FindControl("link" + _selectedTab.ToString())).Attributes["class"] = "tabOn";
                    break;
            }
        }
    }
}