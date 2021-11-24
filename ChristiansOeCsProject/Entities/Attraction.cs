using System.IO;
//using Newtonsoft.Json;

using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    public class Attraction : Location
    {
        public Attraction(string id, double latitiude, double longtitude, string name) : base(id, latitiude, longtitude, name)
        {
        }
        public Attraction(double latitude, double longitude, string name) : base(latitude, longitude, name)
        {
        }
        [JsonConstructor]
        public Attraction()
        {
        }
        

        public override string ToString()
        {
            return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}";
        }
    }
}