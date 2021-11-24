using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    public class Facility : Location
    {
        
        public Facility(string id, double latitude, double longitude, string name) : base(id, latitude, longitude, name)
        {
        }
        
        
        public Facility(double latitude, double longitude, string name) : base(latitude, longitude, name)
        {
        }

        [JsonConstructor]
        public Facility()
        {
        }

        public override string ToString()
        {
            return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}";
        }
    }
}