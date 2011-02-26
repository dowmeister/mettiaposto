using System;

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
                    link1.Attributes["class"] = "small homeOn";
                    break;
                case 2:
                case 3:
                case 4:
                    link2.Attributes["class"] = "tabOn";
                    break;
            }
        }
    }
}