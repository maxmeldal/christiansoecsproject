using System;

namespace ChristiansOeCsProject.Service
{
    public class DistanceService
    {
        private const double _LAT_ENDPOINT = 55.32064;
        private const double _LONG_ENDPOINT = 15.18629;
        
        public double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }
        
        //GET distance in km
        public double Distance(double lat2, double lon2)
        {
            double lon1 = ToRadians(_LONG_ENDPOINT);
            lon2 = ToRadians(lon2);
            double lat1 = ToRadians(_LAT_ENDPOINT);
            lat2 = ToRadians(lat2);

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;

            double a = Math.Pow(Math.Sin(dlat / 2), 2)
                       + Math.Cos(lat1) * Math.Cos(lat2)
                                        * Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in kilometers
            double r = 6371;

            return (c * r);
        }
    }
}