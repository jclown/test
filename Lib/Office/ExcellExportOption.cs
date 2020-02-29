using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib
{
    public class ExcellExportOption
    {
        public Dictionary<int, List<CellRangeAddress>> SheetMergedCell { get; set; }
        public int[] FormulaRecalculationSheetIndex { get; set; }
    }
}
