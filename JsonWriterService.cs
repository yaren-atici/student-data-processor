using System.Text.Json;

namespace MyProject
{
    public class JsonWriterService
    {
        private readonly string _path;

        public JsonWriterService(string path)
        {
            _path = path;
        }

        public void Write(List<CombinedRecord> records)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(records, options);
            File.WriteAllText(_path, json);
        }
    }
}
