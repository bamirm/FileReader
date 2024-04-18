using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiParks
{
    public class CsvProcessing
    {
        public static (List<Park>, string[]) ReadFile(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            List<Park> parks = new List<Park>();
            string[] titles = streamReader.ReadLine()[..^1].Split(';');
            List<string> id = new List<string>();
            List<string> globalId = new List<string>();
            List<string> name = new List<string>();
            List<string> admArea = new List<string>();
            List<string> district = new List<string>();
            List<string> parkName = new List<string>();
            List<string> wifiName = new List<string>();
            List<string> coverageArea = new List<string>();
            List<string> functionFlag = new List<string>();
            List<string> accessFlag = new List<string>();
            List<string> password = new List<string>();
            List<string> longitudeWGS84 = new List<string>();
            List<string> latitudeWGS84 = new List<string>();
            List<string> geoDataCenter = new List<string>();
            List<string> geoArea = new List<string>();
            string line;
            string[] lines;
            while ((line = streamReader.ReadLine()) != null)
            {
                lines = line[..^1].Split(";");
                for (int i = 0; i < lines.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            id.Add(lines[i][1..^1]);
                            break;
                        case 1:
                            globalId.Add(lines[i][1..^1]);
                            break;
                        case 2:
                            name.Add(lines[i][1..^1]);
                            break;
                        case 3:
                            admArea.Add(lines[i][1..^1]);
                            break;
                        case 4:
                            district.Add(lines[i][1..^1]);
                            break;
                        case 5:
                            parkName.Add(lines[i][1..^1]);
                            break;
                        case 6:
                            wifiName.Add(lines[i][1..^1]);
                            break;
                        case 7:
                            coverageArea.Add(lines[i][1..^1]);
                            break;
                        case 8:
                            functionFlag.Add(lines[i][1..^1]);
                            break;
                        case 9:
                            accessFlag.Add(lines[i][1..^1]);
                            break;
                        case 10:
                            password.Add(lines[i][1..^1]);
                            break;
                        case 11:
                            longitudeWGS84.Add(lines[i][1..^1]);
                            break;
                        case 12:
                            latitudeWGS84.Add(lines[i][1..^1]);
                            break;
                        case 13:
                            geoDataCenter.Add(lines[i][1..^1]);
                            break;
                        case 14:
                            geoArea.Add(lines[i][1..^1]);
                            break;
                    }
                }
            }

            for (int i = 0; i < id.Count; i++)
            {
                parks.Add(new Park(id[i], globalId[i], name[i], admArea[i], district[i], parkName[i], wifiName[i], coverageArea[i],
                    functionFlag[i], accessFlag[i], password[i], longitudeWGS84[i], latitudeWGS84[i], geoDataCenter[i], geoArea[i]));
            }

            streamReader.Close();
            return (parks, titles);
        }

        public static void WriteToFile(List<Park> parks, string[] titles, string fileName)
        {
            StreamWriter streamWriter = new StreamWriter($@"../../../../data_file/{fileName}");
            streamWriter.WriteLine($"{string.Join(';', titles)};");
            for (int i = 0; i < parks.Count; i++)
            {
                streamWriter.WriteLine($"{parks[i].Id};{parks[i].GlobalId};{parks[i].Name};{parks[i].AdmArea};" +
                    $"{parks[i].District};{parks[i].ParkName};{parks[i].WifiName};{parks[i].CoverageArea};" +
                    $"{parks[i].FunctionFlag};{parks[i].AccessFlag};{parks[i].Password};{parks[i].LongitudeWGS84};" +
                    $"{parks[i].LatitudeWGS84};{parks[i].GeoDataCenter};{parks[i].GeoArea};");
            }
            
            streamWriter.Close();
        }
    }
}
