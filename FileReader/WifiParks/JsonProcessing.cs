using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WifiParks
{
    public class JsonProcessing
    {
        public static List<Park> ReadFile(string path)
        {
            string jsonString = File.ReadAllText(path);
            List<Park> parks = JsonSerializer.Deserialize<List<Park>>(jsonString)!;

            return parks;
        }

        public static void WriteToFile(List<Park> parks, string fileName)
        {
            string jsonString = JsonSerializer.Serialize<List<Park>>(parks);
            File.WriteAllText($@"../../../../data_file/{fileName}", jsonString);
        }
    }
}
