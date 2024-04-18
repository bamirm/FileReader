using System;
using System.Text.Json.Serialization;

namespace WifiParks
{
    public class Park
    {
        private string _id, _globalId, _name, _admArea, _district, _parkName, _wifiName,
                       _coverageArea, _functionFlag, _accessFlag, _password, _longitudeWGS84,
                       _latitudeWGS84, _geoDataCenter, _geoArea;

        [JsonPropertyName("id")]
        public string Id { get { return _id; } set { _id = value; } }

        [JsonPropertyName("global_id")]
        public string GlobalId { get { return _globalId; } set { _globalId = value; } }

        [JsonPropertyName("Name")]
        public string Name { get { return _name; } set { _name = value; } }

        [JsonPropertyName("AdmArea")]
        public string AdmArea { get { return _admArea; } set { _admArea = value; } }

        [JsonPropertyName("District")]
        public string District { get { return _district; } set { _district = value; } }

        [JsonPropertyName("ParkName")]
        public string ParkName { get { return _parkName; } set { _parkName = value; } }

        [JsonPropertyName("WiFiName")]
        public string WifiName { get { return _wifiName; } set { _wifiName = value; } }

        [JsonPropertyName("CoverageArea")]
        public string CoverageArea { get { return _coverageArea; } set { _coverageArea = value; } }

        [JsonPropertyName("FunctionFlag")]
        public string FunctionFlag { get { return _functionFlag;} set { _functionFlag = value; } }

        [JsonPropertyName("AccessFlag")]
        public string AccessFlag { get { return _accessFlag; } set { _accessFlag = value; } }

        [JsonPropertyName("Password")]
        public string Password { get { return _password; } set { _password = value; } }

        [JsonPropertyName("Longitude_WGS84")]
        public string LongitudeWGS84 { get { return _longitudeWGS84; } set { _longitudeWGS84 = value; } }

        [JsonPropertyName("Latitude_WGS84")]
        public string LatitudeWGS84 { get { return _latitudeWGS84; } set { _latitudeWGS84 = value; } }

        [JsonPropertyName("geodata_center")]
        public string GeoDataCenter { get { return _geoDataCenter; } set { _geoDataCenter = value; } }

        [JsonPropertyName("geoarea")]
        public string GeoArea { get { return _geoArea; } set { _geoArea = value; } }

        public Park() { }

        public Park(string id, string globalId, string name, string admArea, string district, string parkName, string wifiName,
            string coverageArea, string functionFlag, string accessFlag, string password, string longitudeWGS84,
            string latitudeWGS84, string geoDataCenter, string geoArea)
        {
            _id = id;
            _globalId = globalId;
            _name = name;
            _admArea = admArea;
            _district = district;
            _parkName = parkName;
            _wifiName = wifiName;
            _coverageArea = coverageArea;
            _functionFlag = functionFlag;
            _accessFlag = accessFlag;
            _password = password;
            _longitudeWGS84 = longitudeWGS84;
            _latitudeWGS84 = latitudeWGS84;
            _geoDataCenter = geoDataCenter;
            _geoArea = geoArea;
        }
    }
}