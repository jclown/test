using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lib.IO
{
    /// <summary>
    /// A helper class for File operations.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Checks and deletes given file if it does exists.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static void DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static List<string> rsFiles = new List<string>();
        /// <summary>
        /// 历遍指定文件夹下所有指定后缀的文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="suffix"></param>
        public static void TransformFiles(string filePath, string suffix)
        {
            DirectoryInfo dir = new DirectoryInfo(filePath);
            DirectoryInfo[] dirs = dir.GetDirectories();  //获取子目录
            FileInfo[] files = string.IsNullOrEmpty(suffix) ? dir.GetFiles("*.*") : dir.GetFiles("*." + suffix);  //获取文件名            
            foreach (DirectoryInfo d in dirs)
            {
                if (d.ToString().Contains("bin") || d.ToString().Contains("obj") || d.ToString().Contains(".git") || d.ToString().Contains(".vs"))
                    continue;
                TransformFiles(d.ToString() + "\\", suffix); //递归调用
            }
            foreach (FileInfo f in files)
            {
                rsFiles.Add(f.ToString());
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>      
        public static byte[] DownloadFileReadByte(string filePath)
        {
            FileStream pFileStream = null;
            byte[] pReadByte = new byte[0];
            try
            {
                if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
                {
                    pFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    BinaryReader r = new BinaryReader(pFileStream);
                    //将文件指针设置到文件开
                    r.BaseStream.Seek(0, SeekOrigin.Begin);
                    pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                    return pReadByte;
                }
            }
            catch (Exception ex)
            {
                Lib.Log.WriteExceptionLog("DownloadFileReadByte-异常：" + ex.Message);
                return pReadByte;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }
            return null;
        }

        public static void CreateFile(string filePath, byte[] data)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }


        ///<summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImage">源图</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式</param>     
        public static byte[] MakeThumbnail(Image originalImage, int width, int height, string mode)
        {     
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                 
                    break;
                case "W"://指定宽，高按比例                     
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例 
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片 
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
             new Rectangle(x, y, ow, oh),
             GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图 
                //bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                var ms = new MemoryStream();
                var bf = new BinaryFormatter();
                bf.Serialize(ms, bitmap);
                ms.Close();
                return ms.ToArray();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        //public static byte[] a(Image originalImage, int width, int height, string mode)
        //{
        //    height = originalImage.Height * width / originalImage.Width;
        //    using (Image originImage=originalImage)
        //    {
        //        using (var bitmap = new Bitmap(width, height))
        //        {
        //            using (var graphic =  GetGraphic(originImage, bitmap))
        //            {
        //                graphic.DrawImage(originImage, 0, 0, width, height);
        //                using (var encoderParameters = new EncoderParameters(1))
        //                {
        //                    using (var encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L))
        //                    {
        //                        encoderParameters.Param[0] = encoderParameter;
        //                        var ms = new MemoryStream();
        //                        var bf = new BinaryFormatter();
        //                        bf.Serialize(ms, bitmap);
        //                        ms.Close();
        //                        return ms.ToArray();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        public static Image ZoomImage(Image image, int destWidth, int destHeight, string ext = "png")
        {
            try
            {
                var sourceImage = image;
                int width, height;
                var sourceWidth = sourceImage.Width;
                var sourceHeight = sourceImage.Height;
                if (sourceHeight > destHeight || sourceWidth > destWidth)
                {
                    if (sourceWidth * destHeight > sourceHeight * destWidth)
                    {
                        width = destWidth;
                        height = destWidth * sourceHeight / sourceWidth;
                    }
                    else
                    {
                        height = destHeight;
                        width = sourceWidth * destHeight / sourceHeight;
                    }
                }
                else
                {
                    width = sourceWidth;
                    height = sourceHeight;
                }
                var destBitmap = new Bitmap(destWidth, destHeight);
                var g = Graphics.FromImage(destBitmap);
                g.Clear(ext == "png" ? Color.Transparent : Color.White);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourceImage, new Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height),
                    0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                var encoderParams = new EncoderParameters();
                var quality = new long[1];
                quality[0] = 100;
                var encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                sourceImage.Dispose();
                return destBitmap;
            }
            catch
            {
                return image;
            }
        }
    }
}
