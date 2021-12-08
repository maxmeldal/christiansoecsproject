using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    /**
     * Attraction nedarver fra location og implementere andre nødvendige getters og setters, samt konstruktors
     */
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
        public Restaurant(double latitude, double longitude, string name, string url, double open, double close) : base(latitude, longitude, name)
        {
            _url = url;
            _open = open;
            _close = close;
        }
        [JsonConstructor]
        public Restaurant()
        {
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
    }
}