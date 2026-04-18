namespace MyProject
{
    public class DataProcessor
    {
        public List<CombinedRecord> JoinAndSort(
            List<StudentScore> students,
            List<SubjectInfo> subjects)
        {
            var result = students
                .Join(
                    subjects,
                    s => s.Subject,
                    sub => sub.Subject,
                    (s, sub) => new CombinedRecord
                    {
                        Name = s.Name,
                        Subject = s.Subject,
                        Grade = s.Grade,
                        Teacher = sub.Teacher,
                        Credits = sub.Credits
                    })
                .OrderBy(r => r.Name)
                .ToList();

            return result;
        }

        public List<(string Subject, string Teacher, double AvgGrade)> GetSubjectSummary(
            List<CombinedRecord> records)
        {
            return records
                .GroupBy(r => new { r.Subject, r.Teacher })
                .Select(g => (
                    Subject: g.Key.Subject,
                    Teacher: g.Key.Teacher,
                    AvgGrade: Math.Round(g.Average(r => r.Grade), 2)
                ))
                .OrderBy(x => x.Subject)
                .ToList();
        }
    }
}
