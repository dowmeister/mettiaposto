﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenSignals.Framework.Signals;
using OpenSignals.Framework.Categories;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Frontend.Includes
{
    public partial class SignalDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void BuildSignalDescription(Signal currentSignal)
        {
            title.InnerText = currentSignal.Subject;
            address.Text = currentSignal.Address;
            CategoryManager cm = new CategoryManager();
            category.Text = cm.Load(currentSignal.CategoryID).Name;
            lnkDetail.HRef = currentSignal.Link;
            timeframe.Text = SignalUtils.GetTimeframe(currentSignal.CreationDate);
            if (!currentSignal.Attachment.Equals(string.Empty))
            {
                divImage.Visible = true;
                imgImage.ImageUrl = WebUtils.GetImageUrl(UploadPaths.Comments, currentSignal.Attachment);
            }
        }
    }
}