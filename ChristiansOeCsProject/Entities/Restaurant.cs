namespace ChristiansOeCs.Entities
{
    public class Restaurant : Location
    {
        private string _url;
        private int _open;
        private int _close;

        public Restaurant(double latitiude, double longtitude, string name, string url, int open, int close) : base(latitiude, longtitude, name)
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

        public int Open
        {
            get => _open;
            set => _open = value;
        }

        public int Close
        {
            get => _close;
            set => _close = value;
        }
    }
}