using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace SmartSoft.Component
{
    public class tpExcel
    {
        public tpExcel()
        { 
        
        }

        /// <summary>
        /// 读取Excel文档
        /// </summary>
        /// <param name="Path">文件名称</param>
        /// <returns>返回一个数据集</returns>
        public DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }

        private string getCellValue(ICell cell)
        {
            try
            {
                if (cell != null)
                {
                    return cell.ToString();
                }
            }
            catch { }
            return "";
        }

        /// <summary>
        /// 把Sheet中的数据转换为DataTable
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private DataTable ExportToDataTable(ISheet sheet)
        {
            DataTable dt = new DataTable();

            //默认，第一行是字段
            IRow headRow = sheet.GetRow(0);

            //设置datatable字段
            for (int i = headRow.FirstCellNum, len = headRow.LastCellNum; i < len; i++)
            {
                dt.Columns.Add(headRow.Cells[i].StringCellValue);
            }
            //遍历数据行
            for (int i = (sheet.FirstRowNum + 1), len = sheet.LastRowNum + 1; i < len; i++)
            {
                IRow tempRow = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                //遍历一行的每一个单元格
                for (int r = 0, j = tempRow.FirstCellNum, len2 = tempRow.LastCellNum; j < len2; j++, r++)
                {

                    ICell cell = tempRow.GetCell(j);

                    if (cell != null)
                    {
                        switch (cell.CellType)
                        {
                            case CellType.String:
                                dataRow[r] = cell.StringCellValue;
                                break;
                            case CellType.Numeric:
                                dataRow[r] = cell.NumericCellValue;
                                break;
                            case CellType.Boolean:
                                dataRow[r] = cell.BooleanCellValue;
                                break;
                            default: dataRow[r] = "";
                                break;
                        }
                    }
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        public DataTable ExcelToDT(string path)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook wk;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                wk = new HSSFWorkbook(file);
                file.Close();
            }

            HSSFSheet sheet1 = (HSSFSheet)wk.GetSheetAt(0);

            return ExportToDataTable(sheet1);
            /*
            HSSFRow firstrow = (HSSFRow)sheet1.GetRow(0);
            foreach (ICell cell in firstrow.Cells)
            {
                dt.Columns.Add(getCellValue(cell));
            }
            HSSFRow row = null;
            for (int currentRowIndex = 1; currentRowIndex <= sheet1.LastRowNum; currentRowIndex++)
            {
                row = (HSSFRow)sheet1.GetRow(currentRowIndex);

                ICell c = sheet1.GetRow(currentRowIndex).GetCell(0);
                if (getCellValue(c) == "")
                {
                    break;
                }
                DataRow drow = dt.NewRow();
                dt.Rows.Add(drow);
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    ICell cell = row.Cells[i];
                    drow[getCellValue(firstrow.Cells[i])] = getCellValue(cell);
                }
            }
            return dt;*/
        }
    }
}
