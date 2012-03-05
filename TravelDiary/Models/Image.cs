using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diary.Models
{

    public class Image
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Thumb { get; set; }
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
                              Name = fileInfo.Name,
                                  Url = "../../Upload/" + date + "/" + fileInfo.Name,
                                  Thumb = "../../Upload/" + date + "/thumbnails/" + fileInfo.Name,
                                  Comment = imageComment
                              };
                l.Add(img);
            }
            return l;
        }

          public static void Delete(string name)
          {
              var imageRoot = AppDomain.CurrentDomain.BaseDirectory + "Upload\\";
              TextFile.DeleteFile(imageRoot + name + ".txt");
              File.Delete(imageRoot + name);
              File.Delete(imageRoot + "thumbnails\\" + name);
          }

    }
}