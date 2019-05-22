using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToSqlProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel excel = new Excel(@"C:\Users\kiran\myProjects\Tech-Academy-C-Sharp-Coding-Projects\ExcelToSqlProject\Scoresheet", 1);

            Database.ClearTable();
            for(int y = 2; y <= excel.RowCount(); y++)
            {
                string firstName = excel.ReadCell(1, y);
                string lastName = excel.ReadCell(2, y);
                string course = excel.ReadCell(3, y);
                decimal score = Convert.ToDecimal(excel.ReadCell(4, y));

                Database.AddScore(firstName, lastName, course, score);
            }
        }
    }
}
