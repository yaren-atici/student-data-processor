using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MyProject
{
    public class JSonService
    {

        public static void SerializeJson(string path, List<EquationRecord> records)
        {
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(records, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static List<EquationRecord> DeserializeJson(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return new List<EquationRecord>();
                }

                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<EquationRecord>>(json);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
