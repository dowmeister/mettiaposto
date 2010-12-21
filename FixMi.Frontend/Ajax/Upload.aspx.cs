using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FixMi.Framework.Core.Base;
using System.IO;
using Jayrock.Json;
using System.Configuration;

namespace FixMi.Frontend.Ajax
{
    public partial class Upload : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RequestContainsFile("fuFile"))
            {
                JsonObject ret = new JsonObject();

                try
                {
                    HttpPostedFile file = GetFileFromRequest("fuFile");
                    string fileName =  Guid.NewGuid() + Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["UploadPath"]), fileName));
                    ret["fileName"] = fileName;
                }
                catch (Exception ex)
                {
                    ret["error"] = ex.Message;
                }

                Response.Write(ret.ToString());
            }
        }
    }
}