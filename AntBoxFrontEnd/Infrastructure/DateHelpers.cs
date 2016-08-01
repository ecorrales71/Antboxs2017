using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Infrastructure
{
    public class DateHelpers
    {
        public static DateTime DefaultCreateDate => DateTime.Now;

        public static DateTime FromUnixTime(long unixTime)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTime).UtcDateTime;
        }

        public static long ToUnixTime(DateTime time)
        {
            var timeUtc = DateTime.SpecifyKind(time, DateTimeKind.Utc);
            DateTimeOffset timeOffset = timeUtc;
            return timeOffset.ToUnixTimeMilliseconds();
        }

        public static DateTime AppendTimeToDate(DateTime date, TimeSpan time)
        {
            DateTime combine = date.Add(time);
            return combine;
        }
    }
}