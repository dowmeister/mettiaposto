using System;
using OpenSignals.Framework.Core.Base;

namespace OpenSignals.Frontend
{
    public partial class Index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!QueryStringContains("nomobile"))
            {
                if (IsMobileBrowser())
                    Server.Transfer("/m/Default.aspx");
            }

            RegisterAjaxSessionKey();
            GetCurrentCity();
        }
    }
}