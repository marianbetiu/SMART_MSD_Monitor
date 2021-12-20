using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MultiPack
{
    public static class DrawingLib
    {
        #region Extensions
        public static Rectangle GetRectangle(this Image image)
        {
            if (image != null)
            {
                return new Rectangle(0, 0, image.Width, image.Height);
            }
            else
            {
                return Rectangle.Empty;
            }
        }

        public static RectangleF ToRectangleF(this Rectangle rectangle)
        {
            return new RectangleF((float)rectangle.X, (float)rectangle.Y, (float)rectangle.Width, (float)rectangle.Height);
        }
        #endregion

        public static Rectangle RectangleFromString(string coordinates)
        {
            //string format [x, y, width, height]

            Rectangle result = new Rectangle();

            var chunks = coordinates.Split(new string[] { ",", " ", ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (chunks.Length == 4)
            {
                int x = 0;
                int y = 0;
                int width = 0;
                int height = 0;

                int.TryParse(chunks[0], out x);
                int.TryParse(chunks[1], out y);
                int.TryParse(chunks[2], out width);
                int.TryParse(chunks[3], out height);

                result = new Rectangle(x, y, width, height);
            }

            return result;
        }

        public static Image RotateImageRight(Image image)
        {
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            return image;
        }

        public static Image RotateImageLeft(Image image)
        {
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);

            return image;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Image ResizeImage(Image image, double factor)
        {
            var result = image;

            try
            {
                var width = (int)((double)image.Width * factor);
                var height = (int)((double)image.Height * factor);

                var destRect = new Rectangle(0, 0, width, height);
                var destImage = new Bitmap(width, height);

                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (var graphics = Graphics.FromImage(destImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                result = destImage;
            }
            catch { }

            return result;
        }

        //public static Image ResizeImage(Image image, double factor)
        //{
        //    Image result = image;

        //    //Check if image not null and factor positive
        //    if ((image != null) && (factor > 0))
        //    {
        //        try
        //        {
        //            //Calculate new dimensions
        //            var widthNew = (int)((double)image.Width * factor);
        //            var heightNew = (int)((double)image.Height * factor);

        //            //Create new image
        //            result = new Bitmap(widthNew, heightNew);

        //            //Draw into new image canvas
        //            using (var graphics = Graphics.FromImage(result))
        //            {
        //                graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //                graphics.DrawImage(image, new RectangleF(0, 0, widthNew, heightNew));
        //            }
        //        }
        //        catch { }
        //    }

        //    return result;
        //}

        public static Image ResizeImageByWidth(Image image, double width)
        {
            double factor = (((image != null) && (image.Width > 0)) ? width / (double)image.Width : 1.0);

            return ResizeImage(image, factor);
        }

        public static Image ResizeImageByHeight(Image image, double height)
        {
            double factor = (((image != null) && (image.Height > 0)) ? height / (double)image.Height : 1.0);

            return ResizeImage(image, factor);
        }

        public static Image ResizeImageFitRectangle(Image image, double width, double height)
        {
            double factor = 1.0;

            if ((image != null) && (width > 0) && (height > 0))
            {
                if ((width / height) > ((double)image.Width / (double)image.Height))
                {
                    factor = height / (double)image.Height;
                }
                else
                {
                    factor = width / (double)image.Width;
                }
            }

            return ResizeImage(image, factor);
        }

        public static Image ResizeImageFitRectangle(Image image, Rectangle rectangle)
        {
            return ResizeImageFitRectangle(image, rectangle.Width, rectangle.Height);
        }

        public static Image CropImage(Image image, Rectangle rectangle)
        {
            Image result = image;

            if ((image != null) && (rectangle != null))
            {
                try
                {
                    Bitmap croppedImage = new Bitmap(image);
                    var cropRectangle = Rectangle.Intersect(image.GetRectangle(), rectangle);
                    result = croppedImage.Clone(cropRectangle.ToRectangleF(), image.PixelFormat);
                }
                catch { }
            }

            return result;
        }
        
        public static Image CropImage(Image image, int x, int y, int width, int height)
        {
            return CropImage(image, new Rectangle(x, y, width, height));
        }

        public static Color GetContrastColor(Color color)
        {
            int level = 0;

            // Counting the perceptive luminance - human eye favors green color... 
            double luminance = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            if (luminance < 0.5)
                level = 0; // bright colors - black font
            else
                level = 255; // dark colors - white font

            return Color.FromArgb(level, level, level);
        }

        public static Image DrawRectangleOnImage(Image image, Rectangle rectangle, Color color, string title, float fontSize)
        {
            try
            {
                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.DrawRectangle(new Pen(color, 2), rectangle);
                    var font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                    //graphics.DrawString(title, font, new SolidBrush(color), rectangle.X + 2, rectangle.Y + 2);

                    DrawStringOutline(graphics, font, rectangle, title, fontSize, Color.Black, color);

                    font.Dispose();
                }
            }
            catch { }

            return image;
        }

        //public static GraphicsPath GetStringPath(Font font, Rectangle rectangle, string text, float fontSize, StringFormat stringFormat)
        //{
        //    GraphicsPath path = new GraphicsPath();
        //    // Convert font size into appropriate coordinates
        //    //float emSize = dpi * font.SizeInPoints / 72;

        //    path.AddString(text, font.FontFamily, (int)font.Style, fontSize, rectangle.ToRectangleF(), stringFormat);

        //    return path;
        //}

        public static void DrawStringOutline(Graphics graphics, Font font, Rectangle rectangle, string text, float fontSize, Color colorFill, Color colorBorder)
        {
            StringFormat format = StringFormat.GenericTypographic;

            using (var path = new GraphicsPath())
            {
                path.AddString(text, font.FontFamily, (int)font.Style, fontSize, rectangle.ToRectangleF(), format);

                graphics.FillPath(new SolidBrush(colorFill), path);
                graphics.DrawPath(new Pen(colorBorder), path);
            }
        }

        public static Image DrawCrosshairOnImage(Image image, List<Point> positions, Color color)
        {
            try
            {
                int size = 20;

                using (var graphics = Graphics.FromImage(image))
                {
                    var brush = new SolidBrush(color);
                    var pen = new Pen(color, 2);

                    foreach(var position in positions)
                    {
                        graphics.FillEllipse(brush, position.X - (size / 2), position.Y - (size / 2), size, size);
                        graphics.DrawLine(pen, position.X, position.Y - size, position.X, position.Y + size);
                        graphics.DrawLine(pen, position.X - size, position.Y, position.X + size, position.Y);
                    }
                }
            }
            catch { }

            return image;
        }

        public static Image ImageFromFile(string filePath)
        {
            Image result = null;

            try
            {
                result = Image.FromStream(new MemoryStream(File.ReadAllBytes(filePath)));
            }
            catch { }

            return result;
        }

        public static Image ImageFromByteArray(byte[] array)
        {
            Image result = null;

            try
            {
                result = Image.FromStream(new MemoryStream(array));
            }
            catch { }

            return result;
        }

        public static byte[] ImageToByteArrayPNG(Image image)
        {
            byte[] result = null;

            try
            {
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    result = stream.ToArray();
                }
            }
            catch { }

            return result;
        }

        public static byte[] ImageToByteArray(Image image)
        {
            byte[] result = null;

            try
            {
                ImageConverter imageConverter = new ImageConverter();
                result = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
            }
            catch { }

            return result;
        }

        //public static Rectangle AdjustCropRectangleToImage(Image image, Rectangle rectangle)
        //{
        //    Rectangle result = rectangle;

        //    if (image != null)
        //    {
        //        var x = Math.Max(0, rectangle.X);
        //        var y = Math.Max(0, rectangle.Y);
        //        var width = Math.Min((image.Width - x), rectangle.Width);
        //        var height = Math.Min((image.Height - y), rectangle.Height);

        //        result = new Rectangle(x, y, width, height);
        //    }

        //    return result;
        //}

        public static GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            return path;
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if ((graphics != null) && (pen != null))
            {
                using (GraphicsPath path = GetRoundedRectPath(bounds, cornerRadius))
                {
                    graphics.DrawPath(pen, path);
                }
            }
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if ((graphics != null) && (brush != null))
            {
                using (GraphicsPath path = GetRoundedRectPath(bounds, cornerRadius))
                {
                    graphics.FillPath(brush, path);
                }
            }
        }

        // Draw a rotated string at a particular position.
        public static void DrawStringRotated(Graphics g, string text, Font font, Brush brush, float x, float y, float width, float height, StringFormat drawFormat, float angle)
        //public static void DrawStringRotated(Graphics g, string text, Font font, Brush brush, float x, float y, float angle)
        {
            // Save the graphics state.
            GraphicsState state = g.Save();
            g.ResetTransform();

            // Rotate.
            g.RotateTransform(angle);

            RectangleF drawRect = new RectangleF(y, x, height, width);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            g.TranslateTransform(x, y, MatrixOrder.Append);
            
            // Draw the text at the origin.
            //g.DrawString(text, font, brush, 0, 0);
            g.DrawString(text, font, brush, drawRect, drawFormat);

            // Restore the graphics state.
            g.Restore(state);
        }

        public static void FillCircle(Graphics g, Brush brush, float x, float y, float radius)
        {
            var ellipseX = x - radius;
            var ellipseY = y - radius;
            g.FillEllipse(brush, ellipseX, ellipseY, 2 * radius, 2 * radius);
        }

        public static Brush GetColor(string color)
        {
            Brush result = Brushes.Gray;
            if(!string.IsNullOrEmpty(color))
            {
                switch(color)
                {
                    case "Green": result = Brushes.Green;
                        break;
                    case "Yellow": result = Brushes.Yellow;
                        break;
                    case "Red": result = Brushes.Red;
                        break;
                }
            }
            return result;
        }
    }
}
