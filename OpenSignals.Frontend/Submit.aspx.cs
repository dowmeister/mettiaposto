﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Categories;
using log4net;
using log4net.Core;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework;
using System.Globalization;

namespace OpenSignals.Frontend
{
    public partial class Submit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAjaxSessionKey();

            metaOgDescription.Attributes["content"] = String.Format(metaOgDescription.Attributes["content"], CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower()));
            ogTitle.Attributes["content"] = String.Format(ogTitle.Attributes["content"], CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower()));
            this.Title = String.Format(this.Title, CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower()));

            if (!Page.IsPostBack)
                RenderPage();
        }

        private void RenderPage()
        {
            CategoryManager cm = new CategoryManager();
            ddlCategories.DataSource = cm.GetActive();
            ddlCategories.DataBind();

            //string[] parts = GetFromQueryString("city").Split('/');
            //string city = parts[0];
            //string address = string.Empty;
            //if (parts.Length > 1)
            //    address = parts[1];

            //ltCity.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(city.ToLower());

            ltCity.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(GetFromQueryString("city").ToLower());

            if (QueryStringContains("address"))
            {
                txtAddress.Text = GetFromQueryString("address");
                RegisterDocumentReadyFunction("autogeo", JsUtils.CreateJsFunction("geolocationByAddress", false, txtAddress.Text, "map_canvas"));
            }
        }
    }
}