using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Frontend
{
    public partial class AddPlace : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RegisterAjaxSessionKey();

                RegisterDocumentReadyFunction("locatePlace", JsUtils.CreateJsFunction("initAddPlacePage", false, GetFromQueryString("city")));

                InitClientObjects();

                txtCity.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city"));
            }
            catch (Exception ex)
            {
                ManageException("Error loading create place page", "Errore", ex);
            }
        }
    }
}