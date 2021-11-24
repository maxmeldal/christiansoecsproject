using System.IO;
using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    public class Attraction : Location
    {
        //[JsonConstructor]
        public Attraction(string id, double latitiude, double longtitude, string name) : base(id, latitiude, longtitude, name)
        {
        }
        [JsonConstructor]
        public Attraction(double latitude, double longitude, string name) : base(latitude, longitude, name)
        {
        }

        public override string ToString()
        {
            return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}";
        }
    }
}