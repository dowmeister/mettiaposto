using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FixMi.Framework.Core
{
    public struct UploadPaths
    {
        public const string Original = "o/";
        public const string Mobile = "m/";
        public const string Small = "s/";
        public const string Big = "b/";
        public const string Comments = "c/";
    }

    public struct Settings
    {
        public static string UploadPath = ConfigurationManager.AppSettings["UploadPath"];
    }
}
