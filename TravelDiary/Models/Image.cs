using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Diary.Models
{
    public class Image
    {
        public string Name { get; set; }
        public string Comment { get; set; }

        public static List<Image> GetAllImages(string sPath, string date)
        {
            var l = new List<Image>();
            var dir = new DirectoryInfo(sPath);
            foreach (var fileInfo in dir.GetFiles())
            {
                if (fileInfo.Extension.ToLower() != ".jpg") continue;
                var fullPath = fileInfo.FullName.Replace(".jpg", "") + ".txt";
                var imageComment = TextFile.ReadFileWithoutEncoding(fullPath);

                var img = new Image
                              {
                                  Name = "../../Upload/" + date + "/" + fileInfo.Name,
                                  Comment = imageComment
                              };
                l.Add(img);
            }
            return l;
        }
    }
}