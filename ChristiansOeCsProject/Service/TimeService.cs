using System;

namespace ChristiansOeCsProject.Service
{
    public class TimeService
    {
        public double DistanceToMinutes(double distance)
        {
            //Assumed it takes minumum 10 minutes to walk 1 km.
            double kmInMinutes = distance * 10;
            
            return Math.Round(kmInMinutes);
        }
    }
}