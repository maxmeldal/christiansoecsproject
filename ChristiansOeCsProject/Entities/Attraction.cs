using System.IO;

namespace ChristiansOeCs.Entities
{
    public class Attraction : Location
    {
        private QR _qr;
        private string _audio;
        
        public Attraction(double latitiude, double longtitude, string name, QR qr, string audio) : base(latitiude, longtitude, name)
        {
            _qr = qr;
            _audio = audio;
        }
        
        public QR Qr
        {
            get => _qr;
            set => _qr = value;
        }

        public string Audio
        {
            get => _audio;
            set => _audio = value;
        }
    }
}