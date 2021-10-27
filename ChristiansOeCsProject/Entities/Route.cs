using System.Collections.Generic;

namespace ChristiansOeCsProject.Entities
{
    public class Route
    {
        private string _name;
        private string _info;
        private Theme _theme;
        private List<Attraction> _attractions;

        public Route(string name, string info, Theme theme, List<Attraction> attractions)
        {
            _name = name;
            _info = info;
            _theme = theme;
            _attractions = attractions;
        }

        public void AddAttraction(Attraction attraction)
        {
            _attractions.Add(attraction);
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Info
        {
            get => _info;
            set => _info = value;
        }

        public Theme Theme
        {
            get => _theme;
            set => _theme = value;
        }

        public List<Attraction> Attractions
        {
            get => _attractions;
            set => _attractions = value;
        }
    }
}