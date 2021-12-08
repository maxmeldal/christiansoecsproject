using System;

namespace ChristiansOeCsProject.Entities
{
    /**
     * Abstrakt klasse skal nedarbes af all klasser der har en fysisk location
     *
     * 1. constructor tager id, i tilfælde af at et objekt skal opdateres og når man skal hente fra database
     * 2. constructor bruges når der skal oprettes objekter, så de automatisk får tildelt et id
     * 3. contructor (tom) er en Json contructor som rest bruger til at oprette et objekt,
     * og så efterfølgende kalder getters og setters der hvor den kan
     */
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