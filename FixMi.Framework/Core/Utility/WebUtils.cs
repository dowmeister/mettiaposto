using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace FixMi.Framework.Core.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class WebUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string RenderControlToString(Control c)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(sw);
            c.RenderControl(htmlWriter);
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalRecords"></param>
        /// <param name="recordsPerPage"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static HtmlGenericControl CreatePagination(int totalRecords, int recordsPerPage, string func)
        {
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)recordsPerPage));

            if (totalRecords <= 1)
                return new HtmlGenericControl("ul");
            else
            {
                HtmlGenericControl ul = new HtmlGenericControl("ul");

                for (int i = 0; i < totalPages; i++)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    HtmlAnchor a = new HtmlAnchor();
                    a.InnerHtml = (i + 1).ToString();
                    a.HRef = "#";
                    a.Attributes.Add("onclick", JsUtils.CreateJsFunction(func, true, (i * recordsPerPage)));
                    li.Controls.Add(a);
                    ul.Controls.Add(li);
                }

                return ul;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OriginalFile"></param>
        /// <param name="NewFile"></param>
        /// <param name="NewWidth"></param>
        /// <param name="MaxHeight"></param>
        /// <param name="OnlyResizeIfWider"></param>
        /// <seealso cref="http://snippets.dzone.com/posts/show/4336" />
        public static Image ResizeImage(Image FullsizeImage, int NewWidth, int MaxHeight, bool OnlyResizeIfWider)
        {
            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (OnlyResizeIfWider)
            {
                if (FullsizeImage.Width <= NewWidth)
                {
                    NewWidth = FullsizeImage.Width;
                }
            }

            int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;

            if (NewHeight > MaxHeight)
            {
                // Resize with height instead
                NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
                NewHeight = MaxHeight;
            }

            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            //FullsizeImage.Dispose();

            // Save resized picture
            //NewImage.Save(NewFile);
            return NewImage;
        }
    }
}
