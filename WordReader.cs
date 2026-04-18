using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MyProject
{
    public class WordReader
    {
        private readonly string _path;

        public WordReader(string path)
        {
            _path = path;
        }

        public List<StudentScore> ReadStudents()
        {
            var students = new List<StudentScore>();

            using var doc = WordprocessingDocument.Open(_path, false);
            var body = doc.MainDocumentPart?.Document.Body;
            if (body == null) return students;

            var table = body.Elements<Table>().FirstOrDefault();
            if (table == null) return students;

            var rows = table.Elements<TableRow>().ToList();

            // Skip header row (index 0)
            for (int i = 1; i < rows.Count; i++)
            {
                var cells = rows[i].Elements<TableCell>().ToList();
                if (cells.Count < 3) continue;

                string name = cells[0].InnerText.Trim();
                string subject = cells[1].InnerText.Trim();
                string gradeStr = cells[2].InnerText.Trim();

                if (double.TryParse(gradeStr, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double grade))
                {
                    students.Add(new StudentScore
                    {
                        Name = name,
                        Subject = subject,
                        Grade = grade
                    });
                }
            }

            return students;
        }
    }
}
