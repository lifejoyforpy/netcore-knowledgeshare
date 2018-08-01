using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BoeSj.KnowledgeBase.Infrastructure.FileHelper
{
    public static class FileHelper
    {
        public static string GetContentType(this string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                { ".txt", "text/plain"},
                { ".pdf", "application/pdf"},
                { ".doc", "application/vnd.ms-word"},
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                { ".xls", "application/vnd.ms-excel"},
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                { ".png", "image/png"},
                { ".jpg", "image/jpeg"},
                { ".jpeg", "image/jpeg"},
                { ".gif", "image/gif"},
                { ".csv", "text/csv"},
                { ".mpeg", "video/mpeg"},
                { ".mp4", "video/mp4" },
                { ".avi", "video/x-msvideo"},
                {".html", "text/html"}
            };

        }

        public static Dictionary<string, int> FileCollections = new Dictionary<string, int>
        {
            { ".pdf", 0},{ ".png", 0},{ ".jpg", 0},{ ".jpeg", 0},{ ".gif", 0}
            ,{ ".doc", 1},{ ".docx", 1},{ ".xls", 1},{ ".xlsx", 1},{ ".csv", 1}
            ,{ ".avi", 0},{ ".mpeg", 0},{".mp4",0}
        };
    }
}
