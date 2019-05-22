using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToSqlProject
{
    class Excel
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook workbook;
        Worksheet worksheet;
        public Excel(string path, int sheet)
        {
            this.path = path;
            workbook = excel.Workbooks.Open(path);
            worksheet = workbook.Worksheets[sheet];
        }

        public string ReadCell(int x, int y)
        {
            if(worksheet.Cells[y, x].Value2 != null)
            {
                return Convert.ToString(worksheet.Cells[y, x].Value2);
            }
            else
            {
                return "";
            }
        }
        public int RowCount()
        {
            int i = 1;
            while(ReadCell(1, i+1) != "")
            {
                i++;
            }
            return i - 1;
        }
    }
}
