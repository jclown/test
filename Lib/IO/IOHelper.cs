using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace Lib
{
    public static class IOHelper
    {
        public static string CombinePath(params string[] path)
        {
            var newPath = Path.Combine(path);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                newPath = newPath.Replace("\\", "/");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                newPath = newPath.Replace("/", "\\");
            }

            return newPath;
        }

        public static string AppPath { get; set; }

        /// <summary>
        /// 根据文件的相对路径，返回绝对路径。
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetFileFullPath(string file)
        {
            var basePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            if (!string.IsNullOrEmpty(AppPath))
            {
                basePath = AppPath + @"\";
            }
            return CombinePath(basePath, file);
        }

        public static string StreamToString(Stream stream)
        {
            return new System.IO.StreamReader(stream).ReadToEnd();
        }

        /// <summary>
        /// 网络图片转字节流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static byte[] UrlStreamToBytes(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream s = response.GetResponseStream();
            byte[] data = new byte[1024];
            int length = 0;
            MemoryStream ms = new MemoryStream();
            while ((length = s.Read(data, 0, data.Length)) > 0)
            {
                ms.Write(data, 0, length);
            }
            ms.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Seek(0, SeekOrigin.Begin);

            if (ms != null)
                ms.Close();
            if (s != null)
                s.Close();

            return bytes;
        }
    }
}
