using ClosedXML.Excel;

namespace MyProject
{
    public class ExcelWriter
    {
        private readonly string _path;

        public ExcelWriter(string path)
        {
            _path = path;
        }

        public void Write(
            List<CombinedRecord> records,
            List<(string Subject, string Teacher, double AvgGrade)> summary)
        {
            using var workbook = new XLWorkbook();

            // --- Main sheet ---
            var ws = workbook.Worksheets.Add("Results");

            // Headers
            ws.Cell(1, 1).Value = "Name";
            ws.Cell(1, 2).Value = "Subject";
            ws.Cell(1, 3).Value = "Grade";
            ws.Cell(1, 4).Value = "Teacher";
            ws.Cell(1, 5).Value = "Credits";

            var headerRow = ws.Range(1, 1, 1, 5);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.LightBlue;

            // Data rows
            for (int i = 0; i < records.Count; i++)
            {
                var r = records[i];
                ws.Cell(i + 2, 1).Value = r.Name;
                ws.Cell(i + 2, 2).Value = r.Subject;
                ws.Cell(i + 2, 3).Value = r.Grade;
                ws.Cell(i + 2, 4).Value = r.Teacher;
                ws.Cell(i + 2, 5).Value = r.Credits;
            }

            ws.Columns().AdjustToContents();

            // --- Summary sheet ---
            var ws2 = workbook.Worksheets.Add("Summary");

            ws2.Cell(1, 1).Value = "Subject";
            ws2.Cell(1, 2).Value = "Teacher";
            ws2.Cell(1, 3).Value = "Average Grade";

            var headerRow2 = ws2.Range(1, 1, 1, 3);
            headerRow2.Style.Font.Bold = true;
            headerRow2.Style.Fill.BackgroundColor = XLColor.LightGreen;

            for (int i = 0; i < summary.Count; i++)
            {
                var s = summary[i];
                ws2.Cell(i + 2, 1).Value = s.Subject;
                ws2.Cell(i + 2, 2).Value = s.Teacher;
                ws2.Cell(i + 2, 3).Value = s.AvgGrade;
            }

            ws2.Columns().AdjustToContents();

            workbook.SaveAs(_path);
        }
    }
}
