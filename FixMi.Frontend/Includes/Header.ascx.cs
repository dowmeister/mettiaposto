using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FixMi.Frontend.Includes
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
                    link1.Attributes["class"] = "green";
                    break;
                case 2:
                    link2.Attributes["class"] = "orange";
                    break;
                case 3:
                    link3.Attributes["class"] = "pink";
                    break;
                case 4:
                    link4.Attributes["class"] = "pinkdark";
                    break;
            }
        }
    }
}