using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    /**
     * Attraction nedarver fra location og implementere andre nødvendige getters og setters, samt konstruktors
     */
    public class Attraction : Location
    {
        // video og audio attributter gemmer Base64 representationer af bytes for en fil
        private string _video;
        private string _audio;
        private string _image;
        private string _description;

        public Attraction(string id, double latitiude, double longtitude, string name, string video, string audio, string image, string description) : base(id, latitiude, longtitude, name)
        {
            _video = video;
            _audio = audio;
            _image = image;
            _description = description;
        }

        public Attraction(double latitude, double longitude, string name, string video, string audio, string image, string description) : base(latitude, longitude, name)
        {
            _video = video;
            _audio = audio;            
            _image = image;
            _description = description;
        }

        [JsonConstructor]
        public Attraction()
        {
        }

        public string Video
        {
            get => _video;
            set => _video = value;
        }

        public string Audio
        {
            get => _audio;
            set => _audio = value;
        }

        public string Image
        {
            get => _image;
            set => _image = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
    }
}