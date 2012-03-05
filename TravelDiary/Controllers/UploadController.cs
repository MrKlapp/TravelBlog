using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Diary.Models;
using Image = System.Drawing.Image;

namespace Models.Controllers
{
    public class UploadController : Controller
    {
        readonly string _imageRoot = AppDomain.CurrentDomain.BaseDirectory + "Upload\\";

        public ActionResult SaveComments(string id, FormCollection collection)
        {
            var path = Path.Combine(_imageRoot, id);
            path = string.Format("{0}\\", path);
            foreach (var item in collection.AllKeys) {
                var text = collection[item];
                var fileUrl = path + item + ".txt";
                UpdateCommentFile(fileUrl, text);

            }
            return Redirect("/Home/Upload/"+ id);
        }

        public ActionResult UploadFile(string id)
        {
            Image thumbnailImage = null;
            Image originalImage = null;
            Bitmap finalImage = null;
            Graphics graphic = null;
            MemoryStream ms = null;

            if (id.Length == 0) {
                return Redirect("/Home/Day/"+ DateTime.Now.ToShortDateString());
            }

            try
            {
                // Get the data
                var jpegImageUpload = Request.Files["Filedata"];
                var newFolder = id;

                var newPath = Path.Combine(_imageRoot, newFolder);
                Directory.CreateDirectory(newPath);

                var newPathThumbs = Path.Combine(newPath, "thumbnails");
                Directory.CreateDirectory(newPathThumbs);

                newPath = string.Format("{0}\\", newPath);
                newPathThumbs = string.Format("{0}thumbnails\\", newPath);

                var imgName = jpegImageUpload.FileName;
                var imgPath = newPath + imgName;
                var imgPathThumb = newPathThumbs + imgName;

                // Retrieve the uploaded image
                originalImage = Image.FromStream(jpegImageUpload.InputStream);

                // Calculate the new width and height
                var width = originalImage.Width;
                var height = originalImage.Height;
                const int targetWidth = 100;
                const int targetHeight = 100;
                int newWidth, newHeight;

                const float targetRatio = targetWidth/(float) targetHeight;
                var imageRatio = width/(float) height;

                if (targetRatio > imageRatio)
                {
                    newHeight = targetHeight;
                    newWidth = (int) Math.Floor(imageRatio*targetHeight);
                }
                else
                {
                    newHeight = (int) Math.Floor(targetWidth/imageRatio);
                    newWidth = targetWidth;
                }

                newWidth = newWidth > targetWidth ? targetWidth : newWidth;
                newHeight = newHeight > targetHeight ? targetHeight : newHeight;


                // Create the thumbnail

                thumbnailImage = new Bitmap(targetWidth, targetHeight);
                graphic = Graphics.FromImage(thumbnailImage);
                graphic.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, targetWidth, targetHeight));
                var pasteX = (targetWidth - newWidth)/2;
                var pasteY = (targetHeight - newHeight)/2;
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic; /* new way */
                graphic.DrawImage(originalImage, pasteX, pasteY, newWidth, newHeight);

                thumbnailImage.Save(imgPathThumb, ImageFormat.Jpeg);


                if (width > height)
                {
                    newWidth = 600;
                    newHeight = 400;
                }
                else
                {
                    newWidth = 400;
                    newHeight = 600;
                }

                finalImage = new Bitmap(newWidth, newHeight);
                graphic = Graphics.FromImage(finalImage);
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic; /* new way */
                graphic.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                finalImage.Save(imgPath, ImageFormat.Jpeg);
                TextFile.CreateFile(imgPath + ".txt");
                
                Response.StatusCode = 200;
                Response.Write("ok");

            }
            catch(Exception ex)
            {
                // If any kind of error occurs return a 500 Internal Server error
                Response.StatusCode = 500;
                Response.Write("An error occured");
                Response.End();
                //throw ex;
            }
            finally
            {
                 //Clean up
                if (finalImage != null) finalImage.Dispose();
                if (graphic != null) graphic.Dispose();
                if (originalImage != null) originalImage.Dispose();
                if (thumbnailImage != null) thumbnailImage.Dispose();
                if (ms != null) ms.Close();
                Response.End();
            }

            return View();
        }

        private void UpdateCommentFile(string file, string text)
        {
            TextFile.DeleteFile(file);
            TextFile.CreateFile(file);
            TextFile.AppendToFile(file, text);
        }
    }
}
