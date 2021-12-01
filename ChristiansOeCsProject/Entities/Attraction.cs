using System;
using System.IO;
//using Newtonsoft.Json;

using System.Text.Json.Serialization;

namespace ChristiansOeCsProject.Entities
{
    public class Attraction : Location
    {
        private byte[] _video;
        private byte[] _audio;
        public Attraction(string id, double latitiude, double longtitude, string name, byte[] video, byte[] audio) : base(id, latitiude, longtitude, name)
        {
            _video = video;
            _audio = audio;
        }
        public Attraction(double latitude, double longitude, string name, byte[] video, byte[] audio) : base(latitude, longitude, name)
        {
            _video = video;
            _audio = audio;
        }
        [JsonConstructor]
        public Attraction()
        {
        }

        public byte[] Video
        {
            get => _video;
            set => _video = value;
        }

        public byte[] Audio
        {
            get => _audio;
            set => _audio = value;
        }


        public override string ToString()
        {
            if (Video != null && Audio != null)
                return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}, video: {BitConverter.ToString(Video)}, audio: {BitConverter.ToString(Audio)}";
            if (Video != null && Audio == null)
                return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}, video: {BitConverter.ToString(Video)}, audio: no audio";
            if (Video==null && Audio != null)
                return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}, video: no video, audio: {BitConverter.ToString(Audio)}";
            return $"id: {Id}, lat: {Latitude}, long: {Longitude}, name: {Name}, video: no video, audio: no audio";
        }
    }
}