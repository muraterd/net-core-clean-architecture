using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Data.Helpers
{
    public static class FileUploadSize
    {
        public const string ORIGINAL = "Uploads/original";
        public const string LARGE      = "Uploads/l";
        public const string MEDIUM   = "Uploads/m";
        public const string SMALL    = "Uploads/sm";
    }

    public class FileUploadHelper
    {
        public static string GetUniqueFileName(string fileName)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(fileName);
        }

        public static void EnsurePathCreated(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void EnsureAllUploadPathsCreated()
        {
            EnsurePathCreated(FileUploadSize.ORIGINAL);
            EnsurePathCreated(FileUploadSize.LARGE);
            EnsurePathCreated(FileUploadSize.MEDIUM);
            EnsurePathCreated(FileUploadSize.SMALL);
        }
    }
}
