using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.Util;
using NPOI.SS.UserModel;

namespace HIS_MIInterface.Winform.Controller
{
    /// <summary>
    /// 导出Excel
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>  
        /// DataTable导出到Excel文件  
        /// </summary>  
        /// <param name="dtSource">源DataTable</param>  
        /// <param name="strHeaderText">表头文本</param>  
        /// <param name="strFileName">保存位置</param>  
        /// <Author></Author>  
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText, new Dictionary<string, string>()))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        public static void Export(DataTable dtSource, string strHeaderText, Dictionary<string, string> columnNames, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText, columnNames))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        public static void Export(List<DataTable> dtSourceList, string strHeaderText, List<Dictionary<string, string>> columnNameList, string strFileName)
        {
            using (MemoryStream ms = Export(dtSourceList, strHeaderText, columnNameList))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }
        public static void Export(DataTable dtSource, string strHeaderText, string strMemoText, Dictionary<string, string> columnNames, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText, strMemoText, columnNames))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>  
        /// DataTable导出到Excel的MemoryStream  
        /// </summary>  
        /// <param name="dtSource">源DataTable</param>  
        /// <param name="strHeaderText">表头文本 暂时不写</param>  
        /// <param name="columnNames">列名转换</param>  
        /// <Author> 2010-5-8 22:21:41</Author>  
        public static MemoryStream Export(DataTable dtSource, string strHeaderText, Dictionary<string, string> columnNames)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "kakake"; //填加xls文件作者信息  
                si.ApplicationName = ""; //填加xls文件创建程序信息  
                si.LastAuthor = ""; //填加xls文件最后保存者信息  
                si.Comments = "说明信息"; //填加xls文件作者信息  
                si.Title = ""; //填加xls文件标题信息  
                si.Subject = "";//填加文件主题信息  
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽  
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }



            int rowIndex = 0;
            int index = 0;
            ICellStyle cellstyle = workbook.CreateCellStyle();
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 表头及样式
                    //{
                    //    HSSFRow headerRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    //    headerRow.HeightInPoints = 25;
                    //    headerRow.CreateCell(0).SetCellValue(strHeaderText);

                    //    HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                    //    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    //    HSSFFont font = (HSSFFont)workbook.CreateFont();
                    //    font.FontHeightInPoints = 20;
                    //    font.Boldweight = 700;

                    //    headStyle.SetFont(font);

                    //    headerRow.GetCell(0).CellStyle = headStyle;
                    //    if (columnNames.Count > 0)
                    //    {
                    //        sheet.AddMergedRegion(new Region(0, 0, 0, columnNames.Count - 1));
                    //    }
                    //    else
                    //        sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                    //    //headerRow.Dispose();
                    //    rowIndex++;
                    //}
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(rowIndex++);


                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;

                        headStyle.SetFont(font);

                        index = 0;
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            if (columnNames.Count > 0)
                            {
                                if (columnNames.ContainsKey(column.ColumnName))
                                {

                                    headerRow.CreateCell(index).SetCellValue(columnNames[column.ColumnName]);
                                    headerRow.GetCell(index).CellStyle = headStyle;

                                    //设置列宽  
                                    sheet.SetColumnWidth(index, (Encoding.GetEncoding(936).GetBytes(columnNames[column.ColumnName]).Length + 1) * 400);

                                    index++;
                                }
                            }
                            else
                            {
                                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                                headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                                //设置列宽  
                                sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 400);
                            }
                        }
                        //headerRow.Dispose();
                    }
                    #endregion

                    //rowIndex = 3;
                }
                #endregion


                #region 填充内容
                index = 0;
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

                foreach (DataColumn column in dtSource.Columns)
                {
                    if (columnNames.Count > 0)
                    {
                        if (columnNames.ContainsKey(column.ColumnName))
                        {
                            string drValue = row[column].ToString();
                            HSSFCell newCell = (HSSFCell)dataRow.CreateCell(index);
                            //ICellStyle cellstyle = workbook.CreateCellStyle();
                            cellstyle.BorderBottom = BorderStyle.THIN;
                            cellstyle.BorderLeft = BorderStyle.THIN;
                            cellstyle.BorderRight = BorderStyle.THIN;
                            cellstyle.BorderTop = BorderStyle.THIN;
                            cellstyle.BottomBorderColor = HSSFColor.BLACK.index;
                            cellstyle.LeftBorderColor = HSSFColor.BLACK.index;
                            cellstyle.RightBorderColor = HSSFColor.BLACK.index;
                            cellstyle.TopBorderColor = HSSFColor.BLACK.index;
                            if ((arrColWidth[column.Ordinal] + 1) * 400 < (drValue.Length + 1) * 400)
                                sheet.SetColumnWidth(column.Ordinal, (drValue.Length + 3) * 400);

                            switch (column.DataType.ToString())
                            {
                                case "System.String"://字符串类型  
                                    newCell.SetCellValue(drValue);
                                    break;
                                case "System.DateTime"://日期类型  
                                    DateTime dateV;
                                    DateTime.TryParse(drValue, out dateV);
                                    newCell.SetCellValue(dateV);

                                    newCell.CellStyle = dateStyle;//格式化显示  
                                    break;
                                case "System.Boolean"://布尔型  
                                    bool boolV = false;
                                    bool.TryParse(drValue, out boolV);
                                    newCell.SetCellValue(boolV);
                                    break;
                                case "System.Int16"://整型  
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(drValue, out intV);
                                    newCell.SetCellValue(intV);
                                    break;
                                case "System.Decimal"://浮点型  
                                case "System.Double":
                                    double doubV = 0;
                                    double.TryParse(drValue, out doubV);
                                    newCell.SetCellValue(doubV);
                                    break;
                                case "System.DBNull"://空值处理  
                                    newCell.SetCellValue("");
                                    break;
                                default:
                                    newCell.SetCellValue("");
                                    break;
                            }
                            newCell.CellStyle = cellstyle;
                            index++;
                        }
                    }
                    else
                    {
                        string drValue = row[column].ToString();
                        HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                        //ICellStyle cellstyle = workbook.CreateCellStyle();
                        cellstyle.BorderBottom = BorderStyle.THIN;
                        cellstyle.BorderLeft = BorderStyle.THIN;
                        cellstyle.BorderRight = BorderStyle.THIN;
                        cellstyle.BorderTop = BorderStyle.THIN;
                        cellstyle.BottomBorderColor = HSSFColor.BLACK.index;
                        cellstyle.LeftBorderColor = HSSFColor.BLACK.index;
                        cellstyle.RightBorderColor = HSSFColor.BLACK.index;
                        cellstyle.TopBorderColor = HSSFColor.BLACK.index;
                        if ((arrColWidth[column.Ordinal] + 1) * 400 < (drValue.Length + 1) * 400)
                            sheet.SetColumnWidth(column.Ordinal, (drValue.Length + 3) * 400);

                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型  
                                newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型  
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示  
                                break;
                            case "System.Boolean"://布尔型  
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型  
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型  
                            case "System.Double":
                                double doubV = 0;
                                double.TryParse(drValue, out doubV);
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理  
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                        newCell.CellStyle = cellstyle;
                    }
                }
                #endregion

                rowIndex++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //sheet.Workbook.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放 sheet  
                return ms;
            }

        }

        public static MemoryStream Export(List<DataTable> dtSourceList, string strHeaderText, List<Dictionary<string, string>> columnNameList)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "kakake"; //填加xls文件作者信息  
                si.ApplicationName = ""; //填加xls文件创建程序信息  
                si.LastAuthor = ""; //填加xls文件最后保存者信息  
                si.Comments = "说明信息"; //填加xls文件作者信息  
                si.Title = ""; //填加xls文件标题信息  
                si.Subject = "";//填加文件主题信息  
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");
            dateStyle.BorderBottom = BorderStyle.THIN;
            dateStyle.BorderLeft = BorderStyle.THIN;
            dateStyle.BorderRight = BorderStyle.THIN;
            dateStyle.BorderTop = BorderStyle.THIN;
            dateStyle.BottomBorderColor = HSSFColor.BLACK.index;
            dateStyle.LeftBorderColor = HSSFColor.BLACK.index;
            dateStyle.RightBorderColor = HSSFColor.BLACK.index;
            dateStyle.TopBorderColor = HSSFColor.BLACK.index;

            int colCount = 0;
            for (int m = 0; m < dtSourceList.Count; m++)
            {
                if (colCount < dtSourceList[m].Columns.Count)
                    colCount = dtSourceList[m].Columns.Count;
            }
            //取得列宽  
            int[] arrColWidth = new int[colCount];

            for (int w = 0; w < dtSourceList.Count; w++)
            {
                foreach (DataColumn item in dtSourceList[w].Columns)
                {
                    arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                }
                for (int i = 0; i < dtSourceList[w].Rows.Count; i++)
                {
                    for (int j = 0; j < dtSourceList[w].Columns.Count; j++)
                    {
                        int intTemp = Encoding.GetEncoding(936).GetBytes(dtSourceList[w].Rows[i][j].ToString()).Length;
                        if (intTemp > arrColWidth[j])
                        {
                            arrColWidth[j] = intTemp;
                        }
                    }
                }
            }
            int rowIndex = 0;

            for (int iList = 0; iList < dtSourceList.Count; iList++)
            {
                bool NewDt = true;

                int index = 0;
                ICellStyle cellstyle = workbook.CreateCellStyle();
                foreach (DataRow row in dtSourceList[iList].Rows)
                {
                    #region 新建表，填充表头，填充列头，样式
                    if (rowIndex == 65535 || rowIndex == 0 || NewDt)
                    {
                        if (rowIndex != 0 && !NewDt)
                        {
                            sheet = (HSSFSheet)workbook.CreateSheet();
                        }
                        #region //表头及样式
                        //{
                        //    HSSFRow headerRow = (HSSFRow)sheet.CreateRow(rowIndex);
                        //    headerRow.HeightInPoints = 25;
                        //    headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        //    HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                        //    HSSFFont font = (HSSFFont)workbook.CreateFont();
                        //    font.FontHeightInPoints = 20;
                        //    font.Boldweight = 700;

                        //    headStyle.SetFont(font);

                        //    headerRow.GetCell(0).CellStyle = headStyle;
                        //    if (columnNames.Count > 0)
                        //    {
                        //        sheet.AddMergedRegion(new Region(0, 0, 0, columnNames.Count - 1));
                        //    }
                        //    else
                        //        sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                        //    //headerRow.Dispose();
                        //    rowIndex++;
                        //}
                        #endregion
                        #region 列头及样式
                        {
                            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(rowIndex++);

                            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                            headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 10;
                            font.Boldweight = 700;

                            headStyle.SetFont(font);

                            index = 0;
                            foreach (DataColumn column in dtSourceList[iList].Columns)
                            {
                                if (columnNameList[iList].Count > 0)
                                {
                                    if (columnNameList[iList].ContainsKey(column.ColumnName))
                                    {
                                        headerRow.CreateCell(index).SetCellValue(columnNameList[iList][column.ColumnName]);
                                        headerRow.GetCell(index).CellStyle = headStyle;
                                        //设置列宽  
                                        sheet.SetColumnWidth(index, (Encoding.GetEncoding(936).GetBytes(columnNameList[iList][column.ColumnName]).Length + 1) * 400);
                                        index++;
                                    }
                                }
                                else
                                {
                                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                                    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                                    //设置列宽  
                                    sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 400);
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                    NewDt = false;
                    #region 填充内容
                    index = 0;
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in dtSourceList[iList].Columns)
                    {
                        #region 有配置的字段
                        if (columnNameList[iList].Count > 0)
                        {
                            if (columnNameList[iList].ContainsKey(column.ColumnName))
                            {
                                string drValue = row[column].ToString();
                                HSSFCell newCell = (HSSFCell)dataRow.CreateCell(index);
                                cellstyle.BorderBottom = BorderStyle.THIN;
                                cellstyle.BorderLeft = BorderStyle.THIN;
                                cellstyle.BorderRight = BorderStyle.THIN;
                                cellstyle.BorderTop = BorderStyle.THIN;
                                cellstyle.BottomBorderColor = HSSFColor.BLACK.index;
                                cellstyle.LeftBorderColor = HSSFColor.BLACK.index;
                                cellstyle.RightBorderColor = HSSFColor.BLACK.index;
                                cellstyle.TopBorderColor = HSSFColor.BLACK.index;
                                if ((arrColWidth[column.Ordinal] + 1) * 400 < (drValue.Length + 1) * 400)
                                    sheet.SetColumnWidth(column.Ordinal, (drValue.Length + 3) * 400);

                                newCell.CellStyle = cellstyle;

                                switch (column.DataType.ToString())
                                {
                                    case "System.String"://字符串类型  
                                        newCell.SetCellValue(drValue);
                                        break;
                                    case "System.DateTime"://日期类型  
                                        DateTime dateV;
                                        DateTime.TryParse(drValue, out dateV);
                                        newCell.SetCellValue(dateV);
                                        newCell.CellStyle = dateStyle;//格式化显示  
                                        break;
                                    case "System.Boolean"://布尔型  
                                        bool boolV = false;
                                        bool.TryParse(drValue, out boolV);
                                        newCell.SetCellValue(boolV);
                                        break;
                                    case "System.Int16"://整型  
                                    case "System.Int32":
                                    case "System.Int64":
                                    case "System.Byte":
                                        int intV = 0;
                                        int.TryParse(drValue, out intV);
                                        newCell.SetCellValue(intV);
                                        break;
                                    case "System.Decimal"://浮点型  
                                    case "System.Double":
                                        double doubV = 0;
                                        double.TryParse(drValue, out doubV);
                                        newCell.SetCellValue(doubV);
                                        break;
                                    case "System.DBNull"://空值处理  
                                        newCell.SetCellValue("");
                                        break;
                                    default:
                                        newCell.SetCellValue("");
                                        break;
                                }
                                index++;
                            }
                        }
                        #endregion
                        #region 不包含在配置字段中
                        else
                        {
                            string drValue = row[column].ToString();
                            HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                            cellstyle.BorderBottom = BorderStyle.THIN;
                            cellstyle.BorderLeft = BorderStyle.THIN;
                            cellstyle.BorderRight = BorderStyle.THIN;
                            cellstyle.BorderTop = BorderStyle.THIN;
                            cellstyle.BottomBorderColor = HSSFColor.BLACK.index;
                            cellstyle.LeftBorderColor = HSSFColor.BLACK.index;
                            cellstyle.RightBorderColor = HSSFColor.BLACK.index;
                            cellstyle.TopBorderColor = HSSFColor.BLACK.index;
                            if ((arrColWidth[column.Ordinal] + 1) * 400 < (drValue.Length + 1) * 400)
                                sheet.SetColumnWidth(column.Ordinal, (drValue.Length + 3) * 400);

                            switch (column.DataType.ToString())
                            {
                                case "System.String"://字符串类型  
                                    newCell.SetCellValue(drValue);
                                    break;
                                case "System.DateTime"://日期类型  
                                    DateTime dateV;
                                    DateTime.TryParse(drValue, out dateV);
                                    newCell.SetCellValue(dateV);
                                    newCell.CellStyle = dateStyle;//格式化显示  
                                    break;
                                case "System.Boolean"://布尔型  
                                    bool boolV = false;
                                    bool.TryParse(drValue, out boolV);
                                    newCell.SetCellValue(boolV);
                                    break;
                                case "System.Int16"://整型  
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(drValue, out intV);
                                    newCell.SetCellValue(intV);
                                    break;
                                case "System.Decimal"://浮点型  
                                case "System.Double":
                                    double doubV = 0;
                                    double.TryParse(drValue, out doubV);
                                    newCell.SetCellValue(doubV);
                                    break;
                                case "System.DBNull"://空值处理  
                                    newCell.SetCellValue("");
                                    break;
                                default:
                                    newCell.SetCellValue("");
                                    break;
                            }
                            newCell.CellStyle = cellstyle;
                        }
                        #endregion
                    }
                    #endregion
                    rowIndex++;
                }

            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }

        }

        /// <summary>  
        /// DataTable导出到Excel的MemoryStream  
        /// </summary>  
        /// <param name="dtSource">源DataTable</param>  
        /// <param name="strHeaderText">表头文本</param>  
        /// <Author> 2010-5-8 22:21:41</Author>  
        public static MemoryStream Export(DataTable dtSource, string strHeaderText, string strMemoText, Dictionary<string, string> columnNames)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();
            sheet.PrintSetup.Landscape = true;
            sheet.PrintSetup.PaperSize = (short)PaperSize.A4;
            sheet.SetMargin(MarginType.LeftMargin, 1.2);
            sheet.SetMargin(MarginType.RightMargin, 0);
            sheet.SetMargin(MarginType.TopMargin, 0);
            sheet.SetMargin(MarginType.BottomMargin, 0);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "kakake"; //填加xls文件作者信息  
                si.ApplicationName = ""; //填加xls文件创建程序信息  
                si.LastAuthor = ""; //填加xls文件最后保存者信息  
                si.Comments = "说明信息"; //填加xls文件作者信息  
                si.Title = ""; //填加xls文件标题信息  
                si.Subject = "";//填加文件主题信息  
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽  
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }



            int rowIndex = 0;
            int index = 0;
            ICellStyle cellstyle = workbook.CreateCellStyle();
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        //设置边框
                        //headStyle.BorderBottom = BorderStyle.THIN;
                        //headStyle.BorderLeft = BorderStyle.THIN;
                        //headStyle.BorderRight = BorderStyle.THIN;
                        //headStyle.BorderTop = BorderStyle.THIN;
                        //headStyle.BottomBorderColor = HSSFColor.BLACK.index;
                        //headStyle.LeftBorderColor = HSSFColor.BLACK.index;
                        //headStyle.RightBorderColor = HSSFColor.BLACK.index;
                        //headStyle.TopBorderColor = HSSFColor.BLACK.index;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;
                        if (columnNames.Count > 0)
                        {
                            sheet.AddMergedRegion(new Region(0, 0, 0, columnNames.Count - 1));
                            //合并设置边框
                            //CellRangeAddress region = new CellRangeAddress(0, 0, 0, columnNames.Count - 1);
                            //((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region, BorderStyle.DOTTED, NPOI.HSSF.Util.HSSFColor.RED.index);
                        }
                        else
                        {
                            sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                            //CellRangeAddress region = new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1);
                            //((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region, BorderStyle.DOTTED, NPOI.HSSF.Util.HSSFColor.RED.index);
                        }


                        HSSFRow row2 = (HSSFRow)sheet.CreateRow(1);
                        row2.HeightInPoints = 20;
                        row2.CreateCell(0).SetCellValue(strMemoText);

                        HSSFCellStyle headStyle2 = (HSSFCellStyle)workbook.CreateCellStyle();
                        headStyle2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.LEFT;
                        HSSFFont font2 = (HSSFFont)workbook.CreateFont();
                        font2.FontHeightInPoints = 13;
                        headStyle2.SetFont(font2);

                        //headStyle2.BorderBottom = BorderStyle.THIN;
                        //headStyle2.BorderLeft = BorderStyle.THIN;
                        //headStyle2.BorderRight = BorderStyle.THIN;
                        //headStyle2.BorderTop = BorderStyle.THIN;
                        //headStyle2.BottomBorderColor = HSSFColor.BLACK.index;
                        //headStyle2.LeftBorderColor = HSSFColor.BLACK.index;
                        //headStyle2.RightBorderColor = HSSFColor.BLACK.index;
                        //headStyle2.TopBorderColor = HSSFColor.BLACK.index;

                        row2.GetCell(0).CellStyle = headStyle2;



                        if (columnNames.Count > 0)
                        {
                            sheet.AddMergedRegion(new Region(1, 0, 1, columnNames.Count - 1));
                        }
                        else
                            sheet.AddMergedRegion(new Region(1, 0, 1, dtSource.Columns.Count - 1));


                        //headerRow.Dispose();
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(2);


                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                        headStyle.VerticalAlignment = VerticalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 12;
                        font.Boldweight = 700;
                        headStyle.BorderBottom = BorderStyle.THIN;
                        headStyle.BorderLeft = BorderStyle.THIN;
                        headStyle.BorderRight = BorderStyle.THIN;
                        headStyle.BorderTop = BorderStyle.THIN;
                        headStyle.BottomBorderColor = HSSFColor.BLACK.index;
                        headStyle.LeftBorderColor = HSSFColor.BLACK.index;
                        headStyle.RightBorderColor = HSSFColor.BLACK.index;
                        headStyle.TopBorderColor = HSSFColor.BLACK.index;
                        headStyle.WrapText = true;
                        headStyle.SetFont(font);

                        index = 0;
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            if (columnNames.Count > 0)
                            {
                                if (columnNames.ContainsKey(column.ColumnName))
                                {
                                    if (columnNames[column.ColumnName] == "门诊转住院人次")
                                        headerRow.CreateCell(index).SetCellValue("转住院人次");
                                    else
                                        headerRow.CreateCell(index).SetCellValue(columnNames[column.ColumnName]);
                                    headerRow.GetCell(index).CellStyle = headStyle;

                                    //设置列宽  
                                    sheet.SetColumnWidth(index, (Encoding.GetEncoding(936).GetBytes(columnNames[column.ColumnName]).Length + 3) * 256);

                                    index++;
                                }
                            }
                            else
                            {
                                if (column.ColumnName == "门诊转住院人次")
                                    headerRow.CreateCell(index).SetCellValue("转住院人次");
                                else
                                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                                headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                                //设置列宽  
                                sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 3) * 256);
                            }
                        }
                        //headerRow.Dispose();
                    }
                    #endregion

                    rowIndex = 3;
                }
                #endregion


                #region 填充内容
                index = 0;
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    if (columnNames.Count > 0)
                    {
                        if (columnNames.ContainsKey(column.ColumnName))
                        {
                            string drValue = row[column].ToString();
                            HSSFCell newCell = (HSSFCell)dataRow.CreateCell(index);

                            cellstyle.BorderBottom = BorderStyle.THIN;
                            cellstyle.BorderLeft = BorderStyle.THIN;
                            cellstyle.BorderRight = BorderStyle.THIN;
                            cellstyle.BorderTop = BorderStyle.THIN;
                            cellstyle.BottomBorderColor = HSSFColor.BLACK.index;
                            cellstyle.LeftBorderColor = HSSFColor.BLACK.index;
                            cellstyle.RightBorderColor = HSSFColor.BLACK.index;
                            cellstyle.TopBorderColor = HSSFColor.BLACK.index;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 12;
                            cellstyle.SetFont(font);

                            if (column.ColumnName == "就诊人次" || column.ColumnName == "门诊转住院人次" || column.ColumnName == "转住院人次")
                                sheet.SetColumnWidth(column.Ordinal, 1500);
                            else if ((Encoding.GetEncoding(936).GetBytes(drValue).Length + 3) * 256 < 3000)
                                sheet.SetColumnWidth(column.Ordinal, 3000);
                            else
                                sheet.SetColumnWidth(column.Ordinal, (Encoding.GetEncoding(936).GetBytes(drValue).Length + 3) * 256);
                            double doubV = 0;
                            switch (column.DataType.ToString())
                            {
                                case "System.String"://字符串类型
                                    if (IsNumberic(drValue))
                                    {
                                        if (drValue.Contains("."))
                                        {
                                            double.TryParse(drValue, out doubV);
                                            cellstyle.DataFormat = format.GetFormat("#,##0.00");
                                            newCell.SetCellValue(doubV);
                                        }
                                        else
                                            newCell.SetCellValue(drValue);
                                    }
                                    else
                                        newCell.SetCellValue(drValue);
                                    break;
                                case "System.DateTime"://日期类型  
                                    DateTime dateV;
                                    DateTime.TryParse(drValue, out dateV);
                                    newCell.SetCellValue(dateV);

                                    newCell.CellStyle = dateStyle;//格式化显示  
                                    break;
                                case "System.Boolean"://布尔型  
                                    bool boolV = false;
                                    bool.TryParse(drValue, out boolV);
                                    newCell.SetCellValue(boolV);
                                    break;
                                case "System.Int16"://整型  
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(drValue, out intV);
                                    newCell.SetCellValue(intV);
                                    break;
                                case "System.Decimal"://浮点型
                                case "System.Double":
                                    double.TryParse(drValue, out doubV);
                                    cellstyle.DataFormat = format.GetFormat("#,##0.00");
                                    newCell.SetCellValue(doubV);
                                    break;
                                case "System.DBNull"://空值处理  
                                    newCell.SetCellValue("");
                                    break;
                                default:
                                    newCell.SetCellValue("");
                                    break;
                            }
                            newCell.CellStyle = cellstyle;
                            index++;
                        }
                    }
                    else
                    {
                        string drValue = row[column].ToString();
                        HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                        //ICellStyle cellstyle = workbook.CreateCellStyle();
                        cellstyle.BorderBottom = BorderStyle.THIN;
                        cellstyle.BorderLeft = BorderStyle.THIN;
                        cellstyle.BorderRight = BorderStyle.THIN;
                        cellstyle.BorderTop = BorderStyle.THIN;
                        cellstyle.BottomBorderColor = HSSFColor.BLACK.index;
                        cellstyle.LeftBorderColor = HSSFColor.BLACK.index;
                        cellstyle.RightBorderColor = HSSFColor.BLACK.index;
                        cellstyle.TopBorderColor = HSSFColor.BLACK.index;
                        sheet.SetColumnWidth(column.Ordinal, (drValue.Length + 1) * 400);
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 12;
                        cellstyle.SetFont(font);
                        if (column.ColumnName == "就诊人次" || column.ColumnName == "门诊转住院人次" || column.ColumnName == "转住院人次")
                            sheet.SetColumnWidth(column.Ordinal, 1500);
                        else if ((Encoding.GetEncoding(936).GetBytes(drValue).Length + 3) * 256 < 3000)
                            sheet.SetColumnWidth(column.Ordinal, 3000);
                        else
                            sheet.SetColumnWidth(column.Ordinal, (Encoding.GetEncoding(936).GetBytes(drValue).Length + 3) * 256);
                        double doubV = 0;
                        switch (column.DataType.ToString())
                        {
                            case "System.String"://字符串类型  
                                if (IsNumberic(drValue))
                                {
                                    if (drValue.Contains("."))
                                    {
                                        double.TryParse(drValue, out doubV);
                                        cellstyle.DataFormat = format.GetFormat("#,##0.00");
                                        newCell.SetCellValue(doubV);
                                    }
                                    else
                                        newCell.SetCellValue(drValue);
                                }
                                else
                                    newCell.SetCellValue(drValue);
                                break;
                            case "System.DateTime"://日期类型  
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);

                                newCell.CellStyle = dateStyle;//格式化显示  
                                break;
                            case "System.Boolean"://布尔型  
                                bool boolV = false;
                                bool.TryParse(drValue, out boolV);
                                newCell.SetCellValue(boolV);
                                break;
                            case "System.Int16"://整型  
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Byte":
                                int intV = 0;
                                int.TryParse(drValue, out intV);
                                newCell.SetCellValue(intV);
                                break;
                            case "System.Decimal"://浮点型  
                            case "System.Double":
                                double.TryParse(drValue, out doubV);
                                cellstyle.DataFormat = format.GetFormat("#,##0.00");
                                newCell.SetCellValue(doubV);
                                break;
                            case "System.DBNull"://空值处理  
                                newCell.SetCellValue("");
                                break;
                            default:
                                newCell.SetCellValue("");
                                break;
                        }
                        newCell.CellStyle = cellstyle;
                    }
                }
                #endregion

                rowIndex++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //sheet.Workbook.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放 sheet  
                return ms;
            }
        }


        /// <summary>读取excel  
        /// 默认第一行为标头  
        /// </summary>  
        /// <param name="strFileName">excel文档路径</param>  
        /// <returns></returns>  
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            DataView dv = dt.DefaultView;
            dv.RowFilter = " ItemType<>'' or ItemType is not null ";

            return dv.ToTable();
        }
        /// <summary>
        /// 读取excel 默认第一行为标头 
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="columnNames">字段转换对应</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, Dictionary<string, string> columnNames)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                if (columnNames.Count > 0)
                {
                    if (columnNames.ContainsKey(cell.ToString()))
                    {
                        dt.Columns.Add(columnNames[cell.ToString()]);
                    }
                    else
                    {
                        dt.Columns.Add(cell.ToString());
                    }
                }
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                //for (int j = row.FirstCellNum; j < dt.Columns.Count; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            //return dt;
            DataView dv = dt.DefaultView;
            dv.RowFilter = " ItemType<>'' or ItemType is not null ";

            return dv.ToTable();
        }

        public static DataTable ImportText(string strFileName, Dictionary<string, string> columnNames)
        {
            DataTable dt = new DataTable();

            if (columnNames.Count > 0)
            {
                //for (int i = 0; i < columnNames.Count; i++)
                //{
                //    dt.Columns.Add(columnNames.Keys[i]);
                //}
                foreach (var k in columnNames)
                {
                    dt.Columns.Add(k.Value);
                }

            }

            FileStream fs = new FileStream(strFileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                DataRow dr = dt.NewRow();
                string[] sResult = line.Split('|');
                for (int j = 0; j < sResult.Length; j++)
                {
                    dr[j] = sResult[j];
                }
                if (dt.Columns.Contains("AuditFlag") && dr["AuditFlag"].ToString() == "")
                {
                    dr["AuditFlag"] = "已审核";
                    dr["AuditDate"] = System.DateTime.Now.ToString("yyyy-MM-dd");
                }

                dt.Rows.Add(dr);
            }
            sr.Close();

            //string[] lines = System.IO.File.ReadAllLines(strFileName);
            //foreach (string line in lines)
            //{
            //    DataRow dr = dt.NewRow();
            //    string[] sResult=line.Split('|');
            //    for (int j = 0; j < sResult.Length; j++)
            //    {
            //        dr[j] = sResult[j];
            //    }
            //    if (dr["AuditFlag"].ToString() == "")
            //    {
            //        dr["AuditFlag"] = "已审核";
            //        dr["AuditDate"] = System.DateTime.Now.ToString("yyyy-MM-dd");
            //    }

            //    dt.Rows.Add(dr);
            //}

            //DataView dv = dt.DefaultView;
            //dv.RowFilter = " ItemType<>'' or ItemType is not null ";

            return dt;
        }

        public static bool IsNumberic(object sText)
        {
            try
            {
                decimal var1 = Convert.ToDecimal(sText);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
