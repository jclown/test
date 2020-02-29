using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lib.Office
{
    /// <summary>
    /// Pdf表格操作类
    /// </summary>
    public class PdfTable : PdfBase
    {
        /// <summary>
        /// 向PDF中动态插入表格
        /// </summary>
        /// <param name="pdfTemplate">pdf模板路径</param>
        /// <param name="tempFilePath">pdf导出路径</param>
        /// <param name="distance">插入列表位置高度</param>
        /// <param name="title">列表标题</param>
        /// <param name="tableFoot">列表尾部统计</param>
        /// <param name="list">列表部分</param>
        /// <param name="remarks">尾部备注</param>
        public static void PutTable<T>(string pdfTemplate, string tempFilePath, int distance, string[] title,string[] tableFoot, List<T> list, List<string> remarks)
        {
            Document doc = new Document();
            try
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }

                doc = new Document(PageSize.Letter);
                FileStream temFs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
                PdfWriter PWriter = PdfWriter.GetInstance(doc, temFs);
                PdfTable pagebase = new PdfTable();
                PWriter.PageEvent = pagebase;//添加页眉页脚
                doc.Open();

                PdfContentByte cb = PWriter.DirectContent;
                PdfReader reader = new PdfReader(pdfTemplate);
                for (int pageNumber = 1; pageNumber < reader.NumberOfPages + 1; pageNumber++)
                {
                    doc.SetPageSize(reader.GetPageSizeWithRotation(1));

                    PdfImportedPage page = PWriter.GetImportedPage(reader, pageNumber);
                    int rotation = reader.GetPageRotation(pageNumber);
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    //doc.NewPage();
                }
                var distanceStr = "\n";
                for (int i= 0; i < distance;i++)
                {
                    distanceStr= distanceStr+ "\n";
                }
                doc.Add(new Phrase(distanceStr));         

                AddPdfTable<T>(doc, title,tableFoot, list, remarks);              

                doc.Close();
                temFs.Close();
                PWriter.Close();
            }
            catch (Exception ex)
            {
                Lib.Log.WriteExceptionLog($"PdfTable.PutTable={ex.ToString()}");
                throw ex;
            }
            finally
            {
                doc.Close();
            }
        }

        public static void AddPdfTable<T>(Document doc, string[] title, string[] tableFoot, List<T> entitys, List<string> remarks)
        {
            //字体设置
            var dllPath1 = Lib.IOHelper.GetFileFullPath(@"DLL\iTextAsian4Core.dll");
            BaseFont.AddToResourceSearch(dllPath1);
            var dllPath2 = Lib.IOHelper.GetFileFullPath(@"DLL\iTextAsianCmaps4Core.dll");
            BaseFont.AddToResourceSearch(dllPath2);
            BaseFont baseFont = BaseFont.CreateFont("STSong-Light", "UniGB-UCS2-H", BaseFont.NOT_EMBEDDED);
            Font font_title = new Font(baseFont, 8, iTextSharp.text.Font.COURIER);

            //填充标题
            PdfPTable newTitle = new PdfPTable(title.Length);            
            for (int index = 0; index < title.Length; index++)
            {              
                PdfPCell newCell = new PdfPCell(new Paragraph(1, title[index], font_title));
                newCell.BackgroundColor =BaseColor.White;                
                newTitle.AddCell(newCell);   
            }
            doc.Add(newTitle);

            //填充表格内容
            for (int index = 0; index < entitys.Count; index++)
            {
                Type entityType = entitys[0].GetType();
                var entityProperties = entityType.GetProperties();

                PdfPTable newTable = new PdfPTable(entityProperties.Length);
                for (int columnIndex = 0; columnIndex < entityProperties.Length; columnIndex++)
                {
                    var entityValue = entityProperties[columnIndex].GetValue(entitys[index]);
                    PdfPCell newCell = new PdfPCell(new Paragraph(1, entityValue.ToString(), font_title));                                  
                    newTable.AddCell(newCell); 
                }
                doc.Add(newTable);
            }

            //填充表格统计内容
            if (tableFoot != null && tableFoot.Length > 0)
            {
                PdfPTable newFoot = new PdfPTable(tableFoot.Length);
                for (int index = 0; index < tableFoot.Length; index++)
                {
                    PdfPCell newCell = new PdfPCell(new Paragraph(1, tableFoot[index], font_title));
                    newFoot.AddCell(newCell);
                }
                doc.Add(newFoot);
            }

            //添加备注
            doc.Add(new Paragraph("", new Font(baseFont, 8, Font.COURIER)));
            var space = "\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";//空格
            foreach(var item in remarks)
            {
                doc.Add(new Paragraph(space+item, new Font(baseFont, 8, Font.COURIER)));
            }



            //byte[] b = Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAKEAAAChAgMAAADSPzclAAAADFBMVEUAAAACAgIBAQECAgJfmUP0AAAABHRSTlMADAYJlQ+A3AAAA3ZJREFUWMPtmDtu20AQhikaAWIZKnkBAypsw6WQVsgJWGi4hEEELFOqNFIYW/oCquMyUGqXQYTcgb3LIH2aAIENyRJ/zsw+SLjVVCT34878s+9Njna0wbZerx/6cOP/RGT+xMEzerUvPcE4mjbUWh0kpwQzNubb/Fuvt6qoDJCPRHS3q+pqi26CVd7gOVDpjOhT+3JBRNYnnIgeWCi+Sq+JbvmPxkOuRGKmvpyetFWi0tKXdSu/GI/zUnup3c43oZ9hEyqC32CZ4//U2aL4yNwv1bcRHF2u17Z1Xyny3cH5tSWi/IPdyzSOMOt92cctmZV79zqmZp/20Z6sDsmvVTaLxEWesozgiyZTkVF4AdnmSQmybnImJbVOJDkRklKqBIn3UgjKPWRCC9FC8zZdGyJaZm1NKyOk204o5+dJ2hnalklnP2p3sGbhJU+Z+JQJbIiW3XbOWedcso7OCqnkHvzkasGi3vjJrGBJSgTpK5yZhCuqmUPLQwmkad6ppQqQI6p1IjCKWdxL/gwTipLO2wlvMEk2ZSeSuSaZXqjbhMgM5IRsKM5p0cmt3AlYRhrxqE1XNCu8FIJDyLCfz89PvDlBVklI0QipWTEy1eS8JUs3qZuwyYNkqknEufSQKOCmytMBZB0EER1IPYeggyILWvtwEvkeDSDnrCBG6r4UJ2FvJ/Uc8nZF8SzFMz+0NXUPCZJh7U0+uCcn/UkR1le0e3xs5oIcOjPESbRh1nteyhbusYllIDLTgrSY8mOzNx57k5FVxohAtHbIADnvucaNwivsquq9apdsBffPISlK0f1hsoMM37FkReBUPqGN3jxBkdqbopH8WZoZHoqfXBW9d5WV3qnq1RDpdG7F39/fW+0PsfhsAg16Ky438J6t+Hg34i7hzogWw3L7a3eaWEKC6AWlPnegJH7qgTfYijzkFArwwXM6szJrefTEdwAqJ3mmj7tknORUCMKxVp9256rRDoE+2h35dOiBjnFVOEbxKS1cx3/rup/Jg8d/GCHMbqCFIyTjnAC0+xmyyReCWxU7Jprw3cs13Ej3tfAC56JAaJpo5+77pLSBc33HYyyLspQIkk837KINU7PWRJ/hGxdtzkrp9yv4l4QeXSndfUvG3xtClb7rQthTiByDQ9u67aIFfb6RRdx+xuyq2ckCGIj1x/bq92hH29kLE7KiTARdgn4AAAAASUVORK5CYII=");
            //MemoryStream ms = new MemoryStream(b);
            //System.Drawing.Image imgSignature = System.Drawing.Image.FromStream(ms);
            //doc.Add(imgSignature);
        }

        #region GenerateHeader
        /// <summary>  
        /// 生成页眉  
        /// </summary>  
        /// <param name="writer"></param>  
        /// <returns></returns>  
        public override PdfPTable GenerateHeader(iTextSharp.text.pdf.PdfWriter writer)
        {
            BaseFont baseFont = BaseFontForHeaderFooter;
            iTextSharp.text.Font font_logo = new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font_header1 = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font_header2 = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font font_headerContent = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);

            float[] widths = new float[] { 355, 50, 90, 15, 20, 15 };

            PdfPTable header = new PdfPTable(widths);
            PdfPCell cell = new PdfPCell();
            cell.BorderWidthBottom = 2;
            cell.BorderWidthLeft = 2;
            cell.BorderWidthTop = 2;
            cell.BorderWidthRight = 2;
            cell.FixedHeight = 35;

            cell = GenerateOnlyBottomBorderCell(2, iTextSharp.text.Element.ALIGN_LEFT);

            cell.Phrase = new Phrase("LOGO", font_logo);
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            cell.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            cell.PaddingTop = -1;
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(2, iTextSharp.text.Element.ALIGN_RIGHT);
            cell.Phrase = new Paragraph("日期:", font_header2);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(2, iTextSharp.text.Element.ALIGN_LEFT);
            cell.Phrase = new Paragraph(DateTime.Now.ToString("yyyy-MM-dd"), font_headerContent);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(2, iTextSharp.text.Element.ALIGN_RIGHT);
            cell.Phrase = new Paragraph("第", font_header2);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(2, iTextSharp.text.Element.ALIGN_CENTER);
            cell.Phrase = new Paragraph(writer.PageNumber.ToString(), font_headerContent);
            header.AddCell(cell);

            cell = GenerateOnlyBottomBorderCell(2, iTextSharp.text.Element.ALIGN_RIGHT);
            cell.Phrase = new Paragraph("页", font_header2);
            header.AddCell(cell);
            return header;
        }
        #region 
        /// <summary>  
        /// 生成只有底边的cell  
        /// </summary>  
        /// <param name="bottomBorder"></param>  
        /// <param name="horizontalAlignment">水平对齐方式<see cref="iTextSharp.text.Element"/></param>  
        /// <returns></returns>  
        private PdfPCell GenerateOnlyBottomBorderCell(int bottomBorder, int horizontalAlignment)
        {
            PdfPCell cell = GenerateOnlyBottomBorderCell(bottomBorder, horizontalAlignment, iTextSharp.text.Element.ALIGN_BOTTOM);
            cell.PaddingBottom = 5;
            return cell;
        }

        /// <summary>  
        /// 生成只有底边的cell  
        /// </summary>  
        /// <param name="bottomBorder"></param>  
        /// <param name="horizontalAlignment">水平对齐方式<see cref="iTextSharp.text.Element"/></param>  
        /// <param name="verticalAlignment">垂直对齐方式<see cref="iTextSharp.text.Element"/</param>  
        /// <returns></returns>  
        private PdfPCell GenerateOnlyBottomBorderCell(int bottomBorder, int horizontalAlignment, int verticalAlignment)
        {
            PdfPCell cell = GenerateOnlyBottomBorderCell(bottomBorder);
            cell.HorizontalAlignment = horizontalAlignment;
            cell.VerticalAlignment = verticalAlignment; ;
            return cell;
        }

        /// <summary>  
        /// 生成只有底边的cell  
        /// </summary>  
        /// <param name="bottomBorder"></param>  
        /// <returns></returns>  
        private PdfPCell GenerateOnlyBottomBorderCell(int bottomBorder)
        {
            PdfPCell cell = new PdfPCell();
            cell.BorderWidthBottom = 2;
            cell.BorderWidthLeft = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthRight = 0;
            return cell;
        }
        #endregion

        #endregion  

        #region GenerateFooter
        public override PdfPTable GenerateFooter(iTextSharp.text.pdf.PdfWriter writer)
        {
            BaseFont baseFont = BaseFontForHeaderFooter;
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.NORMAL);

            PdfPTable footer = new PdfPTable(new float[] { 1, 1, 2, 1 });
            AddFooterCell(footer, "电话:********", font);
            AddFooterCell(footer, "传真:********", font);
            AddFooterCell(footer, "电子邮件:********", font);
            AddFooterCell(footer, "联系人:********", font);
            return footer;
        }

        private void AddFooterCell(PdfPTable foot, string text, iTextSharp.text.Font font)
        {
            PdfPCell cell = new PdfPCell();
            cell.BorderWidthTop = 2;
            cell.BorderWidthRight = 0;
            cell.BorderWidthBottom = 0;
            cell.BorderWidthLeft = 0;
            cell.Phrase = new Phrase(text, font);
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            foot.AddCell(cell);
        }
        #endregion


    }
}
 