using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Lib
{
    public class ExcelHelper
    {

        #region 基础操作、样式

        private static IWorkbook OpenExcel(string filePath)
        {
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return WorkbookFactory.Create(file); //使用接口，自动识别excel2003/2007格式
            }
        }

        private static bool IsXlsx(string filePath)
        {
            var index = filePath.LastIndexOf(".");
            var ext = filePath.Substring(index + 1); // 保留逗号
            return ext == "xlsx";
        }

        private static IWorkbook CreateWorkbook(string filePath = "")
        {
            bool isXlsx = string.IsNullOrEmpty(filePath) || IsXlsx(filePath);

            IWorkbook workbook;

            if (isXlsx)
            {
                workbook = new XSSFWorkbook();
            }
            else
            {
                workbook = new HSSFWorkbook();
            }

            return workbook;
        }

        /// <summary>
        /// 自动设置Excel列宽
        /// </summary>
        /// <param name="sheet">Excel表</param>
        private static void AutoSizeColumns(ISheet sheet)
        {
            if (sheet.PhysicalNumberOfRows > 0)
            {
                var headerRow = sheet.GetRow(0);

                for (int i = 0, l = headerRow.LastCellNum; i < l; i++)
                {
                    try
                    {
                        sheet.AutoSizeColumn(i);
                    }
                    catch { }
                }
            }
        }

        //private static Dictionary<int, ICellStyle> GetCellStylesByLastRow(ISheet sheet, DataTable table)
        //{
        //    var styles = new Dictionary<int, ICellStyle>();
        //    var styleRow = sheet.GetRow(sheet.LastRowNum);
        //    foreach (DataColumn column in table.Columns)
        //    {
        //        styles[column.Ordinal] = styleRow.Cells[column.Ordinal].CellStyle;
        //    }
        //    return styles;
        //}

        private static Dictionary<int, ICellStyle> GetCellStylesByLastRow(ISheet sheet, int columnCount)
        {
            var styles = new Dictionary<int, ICellStyle>();
            var styleRow = sheet.GetRow(sheet.LastRowNum);
            for (int i = 0; i < columnCount; i++)
            {
                try
                {
                    styles[i] = styleRow.Cells[i].CellStyle;
                }
                catch { }
            }
            return styles;
        }

        private static ICellStyle GetHeaderCellStyle(IWorkbook workbook)
        {
            var font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;

            var style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.FillBackgroundColor = HSSFColor.Grey40Percent.Index;
            style.WrapText = true;//设置自动换行
            //style.FillBackgroundColor = NPOI.SS.UserModel.IndexColors.GREY_40_PERCENT.Index;
            SetCellBorder(style);
            style.SetFont(font);

            return style;
        }

        private static void SetCellBorder(ICellStyle style)
        {
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
        }

        private static ICellStyle GetStyleCellStyle(IWorkbook workbook, string columnName, string columnDataType)
        {
            var style = workbook.CreateCellStyle();
            style.WrapText = true;//设置自动换行
            style.VerticalAlignment = VerticalAlignment.Center;
            SetCellBorder(style);

            switch (columnDataType)
            {
                case "System.DateTime": //日期类型   
                    style.DataFormat = workbook.CreateDataFormat().GetFormat("yyyy-mm-dd");
                    break;

                case "System.Decimal": //浮点型   
                case "System.Double":
                case "System.Single":
                    if (columnName.Substring(columnName.Length - 1, 1) == "%")
                    {
                        style.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.0%");
                    }
                    else
                    {
                        style.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                    }
                    style.Alignment = HorizontalAlignment.Right;
                    break;
                default:
                    style.Alignment = HorizontalAlignment.Left;
                    break;
            }

            return style;
        }

        /// <summary>
        ///     根据Excel列类型获取列的值
        /// </summary>
        /// <param name="cell">Excel列</param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();
                //This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.String:
                    return cell.StringCellValue?.Trim();//去除首尾空格
                case CellType.Formula:
                    try
                    {
                        var e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

        #endregion

        #region 导出

        /// <summary>
        /// 保存Excel文档流到文件 [指定路径]
        /// </summary>
        /// <param name="ms">Excel文档流</param>
        /// <param name="filePath">路径+文件名</param>
        private static void ToFile(IWorkbook workbook, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (var ms = new MemoryStream())
                {
                    workbook.Write(ms);
                    ms.Flush();

                    var data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                    fs.Close();
                    data = null;

                    ms.Close();
                    ms.Dispose();
                }
            }

            workbook.Close();
        }

        private static void SetCellValue(ICell cell, string cellValue, string columnDataType)
        {
            if (cellValue.Length > 30000) //一个Cell最多可写入字符长度为32767，超出报错。考虑到过长用户并不看完，过长时截取前3w个字符。
            {
                cellValue = cellValue.Substring(0, 30000);
            }

            switch (columnDataType)
            {
                case "System.String": //字符串类型   
                    cell.SetCellValue(cellValue);
                    break;

                case "System.DateTime": //日期类型   
                    DateTime dateV;
                    if (DateTime.TryParse(cellValue, out dateV))
                    {
                        dateV = dateV.Date;
                        cell.SetCellValue(dateV);
                    }
                    else
                    {
                        cell.SetCellValue("");
                    }
                    break;

                case "System.Boolean": //布尔型   
                    var boolV = false;
                    bool.TryParse(cellValue, out boolV);
                    cell.SetCellValue(boolV);
                    break;

                case "System.Int16": //整型   
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    var intV = 0;
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        int.TryParse(cellValue, out intV);
                        cell.SetCellValue(intV);
                    }
                    break;

                case "System.Decimal": //浮点型   
                case "System.Double":
                case "System.Single":
                    double doubV = 0;
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        double.TryParse(cellValue, out doubV);
                        cell.SetCellValue(doubV);
                    }
                    else
                    {
                        cell.SetCellValue(cellValue);
                    }
                    break;

                case "System.DBNull": //空值处理   
                    cell.SetCellValue("");
                    break;

                default:
                    cell.SetCellValue("");
                    break;
            }
        }

        private static void SetupByOption(IWorkbook workbook, ExcellExportOption option)
        {
            if (option == null) return;

            // 合并单元格
            if (option.SheetMergedCell != null)
            {
                foreach (var sheetMergedCellOption in option.SheetMergedCell)
                {
                    var sheet = workbook.GetSheetAt(sheetMergedCellOption.Key);

                    foreach (var item in sheetMergedCellOption.Value)
                    {
                        sheet.AddMergedRegion(item);
                    }
                }
            }

            // 重算
            if (option.FormulaRecalculationSheetIndex != null)
            {
                foreach (var index in option.FormulaRecalculationSheetIndex)
                {
                    workbook.GetSheetAt(index).ForceFormulaRecalculation = true;
                }
            }
        }

        private static ISheet CreateSheetHeader(IWorkbook workbook, DataTable table, string sheetName)
        {
            var sheet = workbook.CreateSheet(sheetName);

            // 创建列标题行和样式
            var headerRow = sheet.CreateRow(0);
            var styleRow = sheet.CreateRow(1);
            foreach (DataColumn column in table.Columns)
            {
                var columnName = column.Caption ?? column.ColumnName;
                var headerCell = headerRow.CreateCell(column.Ordinal);
                headerCell.SetCellValue(columnName);
                headerCell.CellStyle = GetHeaderCellStyle(workbook);

                var styleCell = styleRow.CreateCell(column.Ordinal);
                styleCell.CellStyle = GetStyleCellStyle(workbook, columnName, column.DataType.ToString());
            }

            return sheet;
        }

        private static ISheet CreateSheetHeader(IWorkbook workbook, PropertyInfo[] entityProperties, string sheetName, string[] titlesColumns)
        {
            var sheet = workbook.CreateSheet(sheetName);

            // 创建列标题行和样式
            var headerRow = sheet.CreateRow(0);
            var styleRow = sheet.CreateRow(1);
            for (int i = 0; i < entityProperties.Length; i++)
            {
                try
                {
                    var item = entityProperties[i];
                    var headerCell = headerRow.CreateCell(i);
                    headerCell.SetCellValue(titlesColumns[i]);
                    headerCell.CellStyle = GetHeaderCellStyle(workbook);

                    var styleCell = styleRow.CreateCell(i);
                    styleCell.CellStyle = GetStyleCellStyle(workbook, item.Name, item.PropertyType.ToString());
                }
                catch { }

            }

            return sheet;
        }

        private static void FillSheet(ISheet sheet, DataTable table,int insertRowIndex = 0)
        {
            var styles = GetCellStylesByLastRow(sheet, table.Columns.Count);
            insertRowIndex = insertRowIndex > 0 ? insertRowIndex : sheet.LastRowNum - 1;

            //插入新行
            IRow mySourceRow = sheet.GetRow(insertRowIndex);
            InsertRow(sheet, insertRowIndex, table.Rows.Count, mySourceRow);

            foreach (DataRowView row in table.DefaultView)
            {
                var sheetRow = sheet.CreateRow(insertRowIndex);
                insertRowIndex += 1;

                foreach (DataColumn column in table.Columns)
                {
                    var cell = sheetRow.CreateCell(column.Ordinal);
                    SetCellValue(cell, row.Row[column].ToString(), column.DataType.ToString());
                    //cell.CellStyle = styles[column.Ordinal];
                }
            }
        }

        private static void FillSheet<T>(ISheet sheet, List<T> entitys, PropertyInfo[] entityProperties)
        {
            var styles = GetCellStylesByLastRow(sheet, entityProperties.Length);

            for (int index = sheet.LastRowNum; index < entitys.Count; index++)
            {
                var sheetRow = sheet.CreateRow(index);

                for (int columnIndex = 0; columnIndex < entityProperties.Length; columnIndex++)
                {
                    try
                    {
                        var cell = sheetRow.CreateCell(columnIndex);
                        var entityValue = entityProperties[columnIndex].GetValue(entitys[index]);
                        var entityValueString = entityValue == null ? string.Empty : entityValue.ToString();
                        SetCellValue(cell, entityValueString, entityProperties[columnIndex].PropertyType.ToString());
                        cell.CellStyle = styles[columnIndex];
                    }
                    catch { }
                }
            }
        }

        private static byte[] OutputExcel<T>(IWorkbook workbook, List<T> entitys, string[] titlesColumns = null)
        {
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            var sheet = CreateSheetHeader(workbook, entityType.GetProperties(), "sheet1", titlesColumns);
            FillSheet(sheet, entitys, entityProperties);
            AutoSizeColumns(sheet);

            byte[] buffer = new byte[1024 * 2];
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.GetBuffer();
                ms.Close();
            }

            return buffer;
        }

        public static void Export<T>(List<T> entitys, string outputFileName, string[] titlesColumns = null, ExcellExportOption option = null)
        {
            IWorkbook workbook = CreateWorkbook(outputFileName);
            var data = OutputExcel(workbook, entitys, titlesColumns);
            workbook.Write(new MemoryStream(data));
            SetupByOption(workbook, option);
            ToFile(workbook, outputFileName);
            workbook.Close();
        }

        public static byte[] ExportByte<T>(List<T> entitys, string outputFileName, string[] titlesColumns = null, ExcellExportOption option = null)
        {
            IWorkbook workbook = CreateWorkbook(outputFileName);
            var data = OutputExcel(workbook, entitys, titlesColumns);
            //workbook.Write(new MemoryStream(data));
            //SetupByOption(workbook, option);
            //ToFile(workbook, outputFileName);
            //workbook.Close();
            return data;
        }

        public static void ExportByTemplate<T>(List<T> entitys, string outputFileName, string templateFileName, ExcellExportOption option = null)
        {
            var workbook = OpenExcel(templateFileName);
            var data = OutputExcel<T>(workbook, entitys);
            workbook.Write(new MemoryStream(data));
            SetupByOption(workbook, option);
            ToFile(workbook, outputFileName);
            workbook.Close();
        }

        public static byte[] Export2(params DataTable[] tables)
        {
            var workbook = CreateWorkbook();

            for (var i = 0; i < tables.Length; i++)
            {
                var table = tables[i];
                var sheetName = string.IsNullOrEmpty(table.TableName) ? $"sheet{i}" : table.TableName;
                var sheet = CreateSheetHeader(workbook, table, sheetName);
                AutoSizeColumns(sheet);
                FillSheet(sheet, table);
            }

            SetupByOption(workbook, null);

            byte[] data = null;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();

                data = ms.ToArray();

                ms.Close();
                ms.Dispose();
            }

            workbook.Close();

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnsName">修改表头 columnsName 顺序请与  DataTable 顺序一致</param>
        /// <param name="widths">修改表头宽度 key表头名称 value 表头宽度（字数）</param>
        /// <param name="tables"></param>
        /// <returns></returns>
        public static byte[] Export2(string [] columnsName,Dictionary<string,int> widths, params DataTable[] tables)
        {
            var workbook = CreateWorkbook();

            for (var i = 0; i < tables.Length; i++)
            {
                var table = tables[i];
                var sheetName = string.IsNullOrEmpty(table.TableName) ? $"sheet{i}" : table.TableName;
                if (columnsName != null && columnsName.Length > 0)
                {
                    foreach (System.Data.DataColumn item in table.Columns)
                    {
                        item.ColumnName = columnsName[table.Columns.IndexOf(item)];
                    }
                }
                var sheet = CreateSheetHeader(workbook, table, sheetName);
                AutoSizeColumns(sheet);
                if (widths != null) {
                    foreach (var item in widths)
                    {
                        if (table.Columns.IndexOf(item.Key) > -1)
                        {
                            sheet.SetColumnWidth(table.Columns.IndexOf(item.Key), item.Value * 256 );
                        }
                    }
                }

                FillSheet(sheet, table, 1);
            }

            SetupByOption(workbook, null);

            byte[] data = null;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();

                data = ms.ToArray();

                ms.Close();
                ms.Dispose();
            }

            workbook.Close();

            return data;
        }

        public static void Export(string outputFileName, params DataTable[] tables)
        {
            Export(outputFileName, null, tables);
        }

        public static void Export(string outputFileName, ExcellExportOption option, params DataTable[] tables)
        {
            var workbook = CreateWorkbook();

            for (var i = 0; i < tables.Length; i++)
            {
                var table = tables[i];
                var sheetName = string.IsNullOrEmpty(table.TableName) ? $"sheet{i}" : table.TableName;
                var sheet = CreateSheetHeader(workbook, table, sheetName);
                FillSheet(sheet, table);
                AutoSizeColumns(sheet);
            }

            SetupByOption(workbook, option);
            ToFile(workbook, outputFileName);
            workbook.Close();
        }

        public static void ExportByTemplate(string outputFileName, string templateFileName, params DataTable[] tables)
        {
            ExportByTemplateOption(outputFileName, templateFileName, null, tables);
        }

        public static void ExportByTemplateOption(string outputFileName, string templateFileName, ExcellExportOption option, params DataTable[] tables)
        {
            var workbook = OpenExcel(templateFileName);

            for (var i = 0; i < tables.Length; i++)
            {
                var sheet = workbook.GetSheetAt(i);
                FillSheet(sheet, tables[i]);
            }

            SetupByOption(workbook, option);
            ToFile(workbook, outputFileName);
            workbook.Close();
        }

        /// <summary>
        /// 通过模板导出Excel
        /// </summary>
        /// <param name="templateFileName"></param>
        /// <param name="setList">设置指定单元格值</param>
        /// <param name="rowIndex">第几行插入tables</param>
        /// <param name="tables"></param>
        /// <returns></returns>
        public static byte[] ExportByTemplate2(string templateFileName,int insertTableRowIndex, DataTable table, List<SetExcelDto> setList, int insertPicRowIndex = 0, byte[] picBytes = null)
        {
            return ExportByTemplateOption2(templateFileName, null, setList, insertPicRowIndex, picBytes, insertTableRowIndex,table);
        }

        public static byte[] ExportByTemplateOption2(string templateFileName, ExcellExportOption option, List<SetExcelDto> setList, int insertPicRowIndex, byte[] picBytes, int insertTableRowIndex, DataTable table)
        {
            var workbook = OpenExcel(templateFileName);
            var sheet = workbook.GetSheetAt(0);
            FillSheet(sheet, table, insertTableRowIndex);
            SetupByOption(workbook, option);

            //指定单元格赋值
            if (setList != null)
            {
                SetExcelValue(workbook, setList);
            }
            //插入图片
            if (picBytes != null)
            {
                int pictureIndex = workbook.AddPicture(picBytes, PictureType.PNG);
                IDrawing patriarch = sheet.CreateDrawingPatriarch();
                XSSFClientAnchor anchor = new XSSFClientAnchor(50, 50, 225, 225, 0, insertPicRowIndex, 1, insertPicRowIndex + 5);
                IPicture pict = patriarch.CreatePicture(anchor, pictureIndex);
            }

            byte[] data = null;
            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                data = ms.ToArray();
                ms.Close();
                ms.Dispose();
            }
            workbook.Close();
            return data;
        }
        #endregion
     
        #region 导入
        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="file">导入文件</param>
        /// <returns>List<T></returns>
        public static List<T> Import<T>(string filePath)
        {
            List<T> list = new List<T> { };
            IWorkbook workbook = OpenExcel(filePath);
            ISheet sheet = workbook.GetSheetAt(0);
            IRow cellNum = sheet.GetRow(0);
            var propertys = typeof(T).GetProperties();
            string value = null;
            int num = cellNum.LastCellNum;

            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                var obj = System.Activator.CreateInstance<T>();
                if (row.Cells.Count != num) continue;
                for (int j = 0; j < num; j++)
                {
                    value = row.GetCell(j).ToString();
                    if (string.IsNullOrEmpty(value)) continue;
                    string str = (propertys[j].PropertyType).FullName;
                    if (str == "System.String")
                    {
                        propertys[j].SetValue(obj, value, null);
                    }
                    else if (str == "System.DateTime")
                    {
                        DateTime pdt = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                        propertys[j].SetValue(obj, pdt, null);
                    }
                    else if (str == "System.Boolean")
                    {
                        bool pb = Convert.ToBoolean(value);
                        propertys[j].SetValue(obj, pb, null);
                    }
                    else if (str == "System.Int16")
                    {
                        short pi16 = Convert.ToInt16(value);
                        propertys[j].SetValue(obj, pi16, null);
                    }
                    else if (str == "System.Int32")
                    {
                        int pi32 = Convert.ToInt32(value);
                        propertys[j].SetValue(obj, pi32, null);
                    }
                    else if (str == "System.Int64")
                    {
                        long pi64 = Convert.ToInt64(value);
                        propertys[j].SetValue(obj, pi64, null);
                    }
                    else if (str == "System.Byte")
                    {
                        byte pb = Convert.ToByte(value);
                        propertys[j].SetValue(obj, pb, null);
                    }
                    else
                    {
                        propertys[j].SetValue(obj, null, null);
                    }
                }

                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headerRowIndex">第几行开始读取</param>
        /// <param name="rowCount">读取行数</param>
        /// <returns></returns>
        public static DataTable ImportToDataTable2(Stream data, int sheetIndex = 0, int headerRowIndex = 0, string breakStr = "")
        {
            var workbook = WorkbookFactory.Create(data);
            var sheet = workbook.GetSheetAt(sheetIndex);
            return ToDataTable(sheet, headerRowIndex, breakStr);
        }

        public static DataTable ImportToDataTable(string fileName, int sheetIndex = 0)
        {
            var workbook = OpenExcel(fileName);
            var sheet = workbook.GetSheetAt(sheetIndex);
            return ToDataTable(sheet, 0);
        }

        public static DataTable ImportToDataTable(string fileName, string sheetName)
        {
            var workbook = OpenExcel(fileName);
            var sheet = workbook.GetSheet(sheetName);
            return ToDataTable(sheet, 0);
        }

        /// <summary>
        ///     Excel表格转换成DataTable
        /// </summary>
        /// <param name="sheet">表格</param>
        /// <param name="headerRowIndex">标题行索引号，如第一行为0</param>
        /// <returns></returns>
        private static DataTable ToDataTable(ISheet sheet, int headerRowIndex,string breakStr="")
        {
            var table = new DataTable();
            try
            {
                var headerRow = sheet.GetRow(headerRowIndex);
                var styleRow = sheet.GetRow(sheet.LastRowNum);
                int cellCount = headerRow.LastCellNum;
                var rowCount = sheet.LastRowNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    var column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    if (column.ColumnName.LastIndexOf("日期") == column.ColumnName.Length - 2)
                        column.DataType = Type.GetType("System.DateTime");

                    table.Columns.Add(column);
                }

                for (var i = sheet.FirstRowNum + 1+ headerRowIndex; i <= rowCount; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null) continue;

                    var dataRow = table.NewRow();
                    int isEmptyCount = 0;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            if (table.Columns[j].DataType.Equals(Type.GetType("System.DateTime")))
                                dataRow[j] = row.GetCell(j).DateCellValue.ToString();
                            else
                                dataRow[j] = GetCellValue(row.GetCell(j));
                        if (string.IsNullOrEmpty(dataRow[j].ToString().Trim())) {
                            isEmptyCount++;
                        }
                    }
                    if (!string.IsNullOrEmpty(breakStr) && GetCellValue(row.GetCell(0)) == breakStr) break;
                    if (isEmptyCount != cellCount - row.FirstCellNum)
                    {
                        table.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception e)
            {
            }
            return table;
        }

        #endregion

        private static void InsertRow(ISheet sheet, int insertRowIndex, int insertRowCount, IRow formatRow)
        {
            sheet.ShiftRows(insertRowIndex, sheet.LastRowNum, insertRowCount, true, false);
            for (int i = insertRowIndex; i < insertRowIndex + insertRowCount; i++)
            {
                IRow targetRow = null;
                ICell sourceCell = null;
                ICell targetCell = null;
                targetRow = sheet.CreateRow(i);
                for (int m = formatRow.FirstCellNum; m < formatRow.LastCellNum; m++)
                {
                    sourceCell = formatRow.GetCell(m);
                    if (sourceCell == null)
                    {
                        continue;
                    }
                    targetCell = targetRow.CreateCell(m);
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);
                }
            }
            for (int i = insertRowIndex; i < insertRowIndex + insertRowCount; i++)
            {
                IRow firstTargetRow = sheet.GetRow(i);
                ICell firstSourceCell = null;
                ICell firstTargetCell = null;
                for (int m = formatRow.FirstCellNum; m < formatRow.LastCellNum; m++)
                {
                    firstSourceCell = formatRow.GetCell(m, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    if (firstSourceCell == null)
                    {
                        continue;
                    }
                    firstTargetCell = firstTargetRow.GetCell(m, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                    firstTargetCell.CellStyle = firstSourceCell.CellStyle;
                    firstTargetCell.SetCellType(firstSourceCell.CellType);
                    //if (this.insertData != null && this.insertData.Count > 0)
                    //{
                    //    firstTargetCell.SetCellValue(insertData[m]);
                    //}
                }
            }
        }

        private static void SetExcelValue(IWorkbook workbook, List<SetExcelDto> infoDto)
        {
            ISheet sheet = workbook.GetSheetAt(0);
            foreach (var item in infoDto)
            {
                IRow row = sheet.GetRow(item.RowNum);  //读取当前行数据
                if (row != null)
                {
                    var CradNo = row.Cells[1].ToString();
                    row.GetCell(item.CellNum).SetCellValue(item.Value);
                }
            }
        }
    }
    public class SetExcelDto
    {
        /// <summary> 
        /// 行
        /// </summary> 
        public int RowNum { get; set; }
        /// <summary> 
        /// 列
        /// </summary> 
        public int CellNum { get; set; }
        /// <summary> 
        /// 值
        /// </summary> 
        public string Value { get; set; } = "";
    }

    }