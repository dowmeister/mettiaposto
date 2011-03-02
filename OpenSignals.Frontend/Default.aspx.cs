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
            GetCurrentCity();
        }
    }
}