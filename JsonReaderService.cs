using System.Text.Json;

namespace MyProject
{
    public class JsonReaderService
    {
        private readonly string _path;

        public JsonReaderService(string path)
        {
            _path = path;
        }

        public List<SubjectInfo> ReadSubjects()
        {
            if (!File.Exists(_path))
                return new List<SubjectInfo>();

            string json = File.ReadAllText(_path);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<SubjectInfo>>(json, options)
                   ?? new List<SubjectInfo>();
        }
    }
}
