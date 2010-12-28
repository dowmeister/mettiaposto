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
using FixMi.Framework.Core;
using FixMi.Framework.Core.Utility;

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
                    string path = Server.MapPath(Settings.UploadPath);
                    string originalPath = Path.Combine(path, UploadPaths.Original);
                    string mobilePath = Path.Combine(path, UploadPaths.Mobile);
                    string bigPath = Path.Combine(path, UploadPaths.Big);
                    string smallPath = Path.Combine(path, UploadPaths.Small);
                    string commentsPath = Path.Combine(path, UploadPaths.Comments);

                    CreateDirectories(path);

                    HttpPostedFile file = GetFileFromRequest("fuFile");

                    string fileName =  Guid.NewGuid() + Path.GetExtension(file.FileName);

                    System.Drawing.Image originalImage = System.Drawing.Image.FromStream(file.InputStream);
                    System.Drawing.Image smallImage = WebUtils.ResizeImage(originalImage, 350, 300, true);
                    System.Drawing.Image mobileImage = WebUtils.ResizeImage(originalImage, 200, 300, true);
                    System.Drawing.Image commentsImage = WebUtils.ResizeImage(originalImage, 250, 300, true);
                    System.Drawing.Image bigImage = WebUtils.ResizeImage(originalImage, 640, 480, true);

                    originalImage.Save(Path.Combine(originalPath, fileName));
                    bigImage.Save(Path.Combine(bigPath, fileName));
                    mobileImage.Save(Path.Combine(mobilePath, fileName));
                    commentsImage.Save(Path.Combine(commentsPath, fileName));
                    smallImage.Save(Path.Combine(smallPath, fileName));

                    originalImage.Dispose();
                    smallImage.Dispose();
                    mobileImage.Dispose();
                    commentsImage.Dispose();
                    bigImage.Dispose();

                    ret["fileName"] = fileName;
                }
                catch (Exception ex)
                {
                    ret["error"] = ex.Message;
                }

                Response.Write(ret.ToString());
            }
        }

        private void CreateDirectories(string path)
        {
            if (!Directory.Exists(Path.Combine(path, UploadPaths.Original)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Original));

            if (!Directory.Exists(Path.Combine(path, UploadPaths.Mobile)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Mobile));

            if (!Directory.Exists(Path.Combine(path, UploadPaths.Small)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Small));
        }
    }
}