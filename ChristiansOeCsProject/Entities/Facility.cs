namespace ChristiansOeCsProject.Entities
{
    public class Facility : Location
    {
        public Facility(string id, double latitiude, double longitude, string name) : base(id, latitiude, longitude, name)
        {
        }

        public Facility(double latitude, double longitude, string name) : base(latitude, longitude, name)
        {
        }

        public override string ToString()
        {
            return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}";
        }
    }
}