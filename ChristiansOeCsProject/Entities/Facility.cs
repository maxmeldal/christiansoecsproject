namespace ChristiansOeCsProject.Entities
{
    public class Facility : Location
    {
        public Facility(double latitiude, double longtitude, string name) : base(latitiude, longtitude, name)
        {
        }
        
        public override string ToString()
        {
            return "lat: " + Latitude + ", long: " + Longitude + ", name: " + Name;
        }
    }
}