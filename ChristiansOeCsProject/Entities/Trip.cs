using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    public class Trip
    {
        private string _id;
        private string _name;
        private string _info;
        private Theme _theme;
        private List<Attraction> _attractions;


        public Trip(string id, string name, string info, Theme theme, List<Attraction> attractions)
        {
            _id = id;
            _name = name;
            _info = info;
            _theme = theme;
            _attractions = attractions;
        }
        public Trip(string name, string info, Theme theme, List<Attraction> attractions)
        {
            _id = Convert.ToString(Guid.NewGuid());
            _name = name;
            _info = info;
            _theme = theme;
            _attractions = attractions;
        }
        [JsonConstructor]
        public Trip()
        {
        }
        

        public void AddAttraction(Attraction attraction)
        {
            _attractions.Add(attraction);
        }

        public void RemoveAttraction(string id)
        {
            int index = _attractions.FindIndex(a => a.Id == id);
            _attractions.RemoveAt(index);
        }

        public string Id
        {
            get => _id;
            set => _id = value;
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

        public override string ToString()
        {
            var str = "";
            foreach (var attractionStr in Attractions)
            {
                str += attractionStr + " ";
            }
            return $"id: {Id}, name: {Name}, name: {Info}, theme: {Theme}, attractions: [{str}]";
        }
    }
}