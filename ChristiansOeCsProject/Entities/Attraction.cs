using System.IO;

namespace ChristiansOeCsProject.Entities
{
    public class Attraction : Location
    {
        //private string _audio;
        
        public Attraction(string id, double latitiude, double longtitude, string name/*, string audio*/) : base(id, latitiude, longtitude, name)
        {
            //_audio = audio;
        }
        // public string Audio
        // {
        //     get => _audio;
        //     set => _audio = value;
        // }
        
        public override string ToString()
        {
            return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}";
        }
    }
}