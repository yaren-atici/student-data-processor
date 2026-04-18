namespace MyProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string assetsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            Directory.CreateDirectory(assetsFolder);

            string wordFile = Path.Combine(assetsFolder, "data1.docx");
            string jsonFile = Path.Combine(assetsFolder, "data2.json");
            string excelOut = Path.Combine(assetsFolder, "result_xls.xlsx");
            string jsonOut  = Path.Combine(assetsFolder, "result_json.json");

            // 1. Read Word
            Console.WriteLine("Reading Word file...");
            var wordReader = new WordReader(wordFile);
            var students = wordReader.ReadStudents();
            Console.WriteLine($"  Loaded {students.Count} student records.");

            // 2. Read JSON
            Console.WriteLine("Reading JSON file...");
            var jsonReader = new JsonReaderService(jsonFile);
            var subjects = jsonReader.ReadSubjects();
            Console.WriteLine($"  Loaded {subjects.Count} subject records.");

            // 3. Join + Sort
            Console.WriteLine("Processing data...");
            var processor = new DataProcessor();
            var combined = processor.JoinAndSort(students, subjects);
            Console.WriteLine($"  Combined records: {combined.Count}");

            Console.WriteLine("\n--- Combined Data (sorted by Name) ---");
            foreach (var r in combined)
                Console.WriteLine($"  {r.Name,-10} {r.Subject,-10} Grade:{r.Grade}  Teacher:{r.Teacher}  Credits:{r.Credits}");

            // 4. Summary
            var summary = processor.GetSubjectSummary(combined);
            Console.WriteLine("\n--- Subject Summary ---");
            foreach (var s in summary)
                Console.WriteLine($"  {s.Subject,-10} Teacher:{s.Teacher,-15} AvgGrade:{s.AvgGrade}");

            // 5. Write Excel
            Console.WriteLine("\nWriting Excel file...");
            var excelWriter = new ExcelWriter(excelOut);
            excelWriter.Write(combined, summary);
            Console.WriteLine($"  Saved: {excelOut}");

            // 6. Write JSON
            Console.WriteLine("Writing JSON file...");
            var jsonWriter = new JsonWriterService(jsonOut);
            jsonWriter.Write(combined);
            Console.WriteLine($"  Saved: {jsonOut}");

            Console.WriteLine("\nDone!");
        }
    }
}
