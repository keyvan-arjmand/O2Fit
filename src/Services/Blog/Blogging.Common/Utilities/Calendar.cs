using System;
using System.Globalization;

namespace Blogging.Common.Utilities
{
    public static class Calendar
    {
        public static string ToPersianTime(this DateTime calendar)
        {
            PersianCalendar pc = new PersianCalendar();
            return string.Format("{0}/{1}/{2}", pc.GetYear(calendar), pc.GetMonth(calendar),
                pc.GetDayOfMonth(calendar));
        }
    }
}