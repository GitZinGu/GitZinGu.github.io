using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;                       

namespace salotto.Tool
{
    public class ExcelHelper
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dt">数据列表</param>
        /// <param name="sheetName">标签名</param>
        /// <param name="excelpath">存放路径/+文件名.xls||.xlsx</param>
        public static void ExportExcel(DataTable dt, string sheetName, string excelpath)
        {
            try
            {
                IWorkbook workbook;
                if (excelpath.Contains(".xlsx"))
                    workbook = new XSSFWorkbook();
                else if (excelpath.Contains(".xls"))
                    workbook = new HSSFWorkbook();
                else
                    workbook = null;

                ISheet sheet = null;
                int headRowIndex = 0;
                if (!string.IsNullOrEmpty(dt.TableName))
                {
                    sheetName = dt.TableName;
                }
                sheet = workbook.CreateSheet(sheetName);
                int rowIndex = 0;

                #region 列头及样式
                {
                    IRow headerRow = sheet.CreateRow(headRowIndex);

                    ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = HorizontalAlignment.Center;
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    font.IsBold = true;
                    headStyle.SetFont(font);

                    foreach (DataColumn column in dt.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                        headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                    }
                }
                #endregion

                #region 填充内容

                foreach (DataRow row in dt.Rows)
                {
                    rowIndex++;
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dt.Columns)
                    {
                        string drValue = row[column].ToString();
                        dataRow.CreateCell(column.Ordinal).SetCellValue(drValue);
                    }
                }
                #endregion

                using (FileStream file = new FileStream(excelpath, FileMode.Create))//创建文件流，将灌好数据的excel文件写入服务器的硬盘
                {
                    workbook.Write(file);
                }
                return;
            }

            catch (Exception ex)
            {                                                                              
                throw ex;
            }

        }
    }
}
