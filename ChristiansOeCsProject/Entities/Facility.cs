using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    /**
     * Facility nedarver fra location og implementere konstruktors
     */
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
    }
}