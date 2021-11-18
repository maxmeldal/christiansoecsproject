using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    public class Restaurant : Location
    {
        private string _url;
        private double _open;
        private double _close;

        public Restaurant(string id, double latitiude, double longtitude, string name, string url, double open, double close) : base(id, latitiude, longtitude, name)
        {
            _url = url;
            _open = open;
            _close = close;
        }
        [JsonConstructor]
        public Restaurant(double latitude, double longitude, string name, string url, double open, double close) : base(latitude, longitude, name)
        {
            _url = url;
            _open = open;
            _close = close;
        }

        public string Url
        {
            get => _url;
            set => _url = value;
        }

        public double Open
        {
            get => _open;
            set => _open = value;
        }

        public double Close
        {
            get => _close;
            set => _close = value;
        }
        public override string ToString()
        {
            return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}, url: {Url}, open: {Open}-{Close}";
        }
    }
}