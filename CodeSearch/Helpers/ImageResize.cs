using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using CodeSearch.Models;

namespace CodeSearch.Helpers
{
    public class ImageResize
    {
        public static HttpPostedFileBase ResizeImage(HttpPostedFileBase ImageUpload)
        {

                //Save File
                string imageFile = ImageUpload.FileName.ToString();
                string path = HttpContext.Current.Server.MapPath("~/Content/Images/" + imageFile);
                ImageUpload.SaveAs(path);

                System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                int newHeight = 200;
                int newWidth = Convert.ToInt32(aspectRatio * newHeight);

                System.Drawing.Bitmap thumbBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
                System.Drawing.Graphics thumbGraph = System.Drawing.Graphics.FromImage(thumbBitmap);

                thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);

                    using (var newBitmap = new Bitmap(thumbBitmap))
                    {
                        newBitmap.Save(HttpContext.Current.Server.MapPath("~/Content/Images/" + "resize-" + ImageUpload.FileName), System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                thumbGraph.Dispose();
                thumbBitmap.Dispose();
                image.Dispose();

                return ImageUpload;          
        }
    }
}