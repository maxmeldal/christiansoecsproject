namespace ChristiansOeCs.Entities
{
    public abstract class Location
    {
        private double _latitude;
        private double _longitude;
        private string _name;

        protected Location(double latitude, double longitude, string name)
        {
            _latitude = latitude;
            _longitude = longitude;
            _name = name;
        }

        public double Latitude
        {
            get => _latitude;
            set => _latitude = value;
        }

        public double Longitude
        {
            get => _longitude;
            set => _longitude = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }
    }
}