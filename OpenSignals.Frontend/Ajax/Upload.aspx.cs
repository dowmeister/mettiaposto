﻿using System;
using System.IO;
using System.Web;
using Jayrock.Json;
using OpenSignals.Framework.Core;
using OpenSignals.Framework.Core.Base;
using OpenSignals.Framework.Core.Utility;

namespace OpenSignals.Frontend.Ajax
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
                    string path = Server.MapPath(ConfigurationOptions.Current.GetString("system_upload_path"));
                    string originalPath = Path.Combine(path, UploadPaths.Original);
                    string mobilePath = Path.Combine(path, UploadPaths.Mobile);
                    string bigPath = Path.Combine(path, UploadPaths.Big);
                    string smallPath = Path.Combine(path, UploadPaths.Small);
                    string commentsPath = Path.Combine(path, UploadPaths.Comments);

                    CreateDirectories(path);

                    HttpPostedFile file = GetFileFromRequest("fuFile");

                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    System.Drawing.Image originalImage = System.Drawing.Image.FromStream(file.InputStream);
                    System.Drawing.Image smallImage = WebUtils.ResizeImage(originalImage, 350, 300, true);
                    System.Drawing.Image mobileImage = WebUtils.ResizeImage(originalImage, 200, 300, true);
                    System.Drawing.Image commentsImage = WebUtils.ResizeImage(originalImage, 150, 200, true);
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

            if (!Directory.Exists(Path.Combine(path, UploadPaths.Big)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Big));

            if (!Directory.Exists(Path.Combine(path, UploadPaths.Comments)))
                Directory.CreateDirectory(Path.Combine(path, UploadPaths.Comments));
        }
    }
}