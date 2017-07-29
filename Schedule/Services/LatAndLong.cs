using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schedule.Services
{
    public class LatAndLong
    {
        private const Double BaseLat = 23.239933;
        private const Double BaseLong = 77.372615;
        private const Double BaseLatRange = 0.400000;
        private const Double BaseLongRange = 0.800000;

        public String GetLat()
        {
            Double lat = BaseLat + GetNextRandomNumber(BaseLatRange);
            return lat.ToString();
        }
        public String GetLong()
        {
            Double lon= BaseLong + GetNextRandomNumber(BaseLongRange);
            return lon.ToString();
        }

        public Double GetNextRandomNumber(Double Base)
        {
            Random random = new Random();
            int res = (int)(random.NextDouble()*1000000*Base);
            Double result = (double)(res) / 1000000;
            return result;
        }
    }
}