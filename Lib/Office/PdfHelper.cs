using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace Lib.Office
{
    public class PdfHelper  
    {
        /// <summary>
        /// 通过模板生成PDF文件流
        /// </summary>
        /// <param name="tempPath"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string ConvertToPDF(string tempPath, Dictionary<string, string> para)
        {
            //获取中文字体，第三个参数表示为是否潜入字体，但只要是编码字体就都会嵌入。
            //BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //字体设置
            var dllPath1 = Lib.IOHelper.GetFileFullPath(@"DLL\iTextAsian4Core.dll");
            BaseFont.AddToResourceSearch(dllPath1);
            var dllPath2 = Lib.IOHelper.GetFileFullPath(@"DLL\iTextAsianCmaps4Core.dll");
            BaseFont.AddToResourceSearch(dllPath2);
            BaseFont baseFont = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.NOT_EMBEDDED);
             
            //读取模板文件
            PdfReader reader = new PdfReader(tempPath);

            //创建文件流用来保存填充模板后的文件
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            PdfStamper stamp = new PdfStamper(reader, stream);
            //设置表单字体，在高版本有用，高版本加入这句话就不会插入字体，低版本无用
            //stamp.AcroFields.AddSubstitutionFont(baseFont);

            AcroFields form = stamp.AcroFields;

            //表单文本框是否锁定
            stamp.FormFlattening = true;

            //填充表单,para为表单的一个（属性-值）字典
            foreach (KeyValuePair<string, string> parameter in para)
            {
                //要输入中文就要设置域的字体;
                form.SetFieldProperty(parameter.Key, "textfont", baseFont, null);
                //为需要赋值的域设置值;
                form.SetField(parameter.Key, parameter.Value);
            }

            //按顺序关闭io流
            stamp.Close();
            reader.Close();

            //保存到磁盘
            var fileName = RandomHelper.GetRandomString(13,3);
            var path = $@"Resources\Templates\PDFTemplates\{fileName}.pdf";
            var saveFullPath = Lib.IOHelper.GetFileFullPath(path);
            var fullPath = Path.GetDirectoryName(saveFullPath);
            //如果没有此文件夹，则新建
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            var bytes = stream.ToArray();
            //创建文件，返回一个 FileStream，它提供对 path 中指定的文件的读/写访问。
            using (FileStream s = File.Create(saveFullPath))
            {
                //将字节数组写入流
                s.Write(bytes, 0, bytes.Length);
                s.Close();
            }
            return fileName+ ".pdf";
        }     
    }
}
