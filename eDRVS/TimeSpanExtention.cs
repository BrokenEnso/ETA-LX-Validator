using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDRVS
{
    public static class TimeSpanExtensions
    {
        public static int GetApproxYears(this TimeSpan timespan)
        {
            return (int)((double)timespan.Days / 365.2425);
        }

        public static int GetApproxMonths(this TimeSpan timespan)
        {
            return (int)((double)timespan.Days / 30.436875);
        }
    }
}
