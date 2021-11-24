using System;

namespace ChristiansOeCsProject.Entities
{
    public abstract class Location
    {
        private string _id;
        private double _latitude;
        private double _longitude;
        private string _name;

        protected Location(string id, double latitude, double longitude, string name)
        {
            _id = id;
            _latitude = latitude;
            _longitude = longitude;
            _name = name;
        }

        protected Location(double latitude, double longitude, string name)
        {
            _id = Convert.ToString(Guid.NewGuid());
            _latitude = latitude;
            _longitude = longitude;
            _name = name;
        }

        protected Location()
        {
        }

        public string Id
        {
            get => _id;
            set => _id = value;
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