using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject
{
    public class ExcelReader
    {
        readonly string _path;

        public ExcelReader(string path)
        {
            _path = path;
        }

        public List<string> ReadEquations()
        {
            using var workbook = new XLWorkbook(_path);

            var worksheet = workbook.Worksheet(1);

            return worksheet.RowsUsed()
                .Select(row => row.Cell(1).GetValue<string>()).ToList();
        }
    }
}
