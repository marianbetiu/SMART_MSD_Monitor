using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace MultiPack
{
    //Date time calculations utils
    public static class DateTimeLib
    {
        public static DateTime GetFirstDateTimeOfDay(DateTime day)
        {
            return new DateTime(day.Year, day.Month, day.Day, 0, 0, 0);
        }

        public static DateTime GetFirstDateTimeOfMonth(DateTime day)
        {
            return new DateTime(day.Year, day.Month, 1, 0, 0, 0);
        }

        public static DateTime GetLastDateTimeOfDay(DateTime day)
        {
            return new DateTime(day.Year, day.Month, day.Day, 23, 59, 59);
        }

        public static DateTime GetLastDateTimeOfMonth(DateTime day)
        {
            var dateFirstDateTimeNextMonth = GetFirstDateTimeOfMonth(day.AddMonths(1));
            return dateFirstDateTimeNextMonth.AddSeconds(-1);
        }

        public static int GetDayOfWeek(DateTime day)
        {
            int result = 0;

            //Get day of week 0 - Sunday, 1 - Monday, ..., 6 - Saturday
            result = (int)day.DayOfWeek;

            //Decrement to set Monday as 0
            result--;

            //Set Sunday as last day -> after decrement it is -1
            if (result < 0) result = 6;

            return result;
        }

        public static DateTime GetFirstDateTimeOfWeek(DateTime day, DayOfWeek startOfWeek)
        {
            int diff = (7 + (day.DayOfWeek - startOfWeek)) % 7;
            return day.AddDays(-1 * diff).Date;
        }

        public static DateTime GetFirstDateTimeOfWeek(DateTime day)
        {
            int diff = (7 + (day.DayOfWeek - DayOfWeek.Monday)) % 7;
            return day.AddDays(-1 * diff).Date;
        }

        public static DateTime GetLastDateTimeOfWeek(DateTime day, DayOfWeek startOfWeek)
        {
            return GetLastDateTimeOfDay(GetFirstDateTimeOfWeek(day, startOfWeek).AddDays(6));
        }

        public static DateTime GetLastDateTimeOfWeek(DateTime day)
        {
            return GetLastDateTimeOfDay(GetFirstDateTimeOfWeek(day).AddDays(6));
        }

        public static DateTime Max(DateTime date1, DateTime date2)
        {
            return ((date1 > date2) ? date1 : date2);
        }

        public static DateTime Min(DateTime date1, DateTime date2)
        {
            return ((date1 < date2) ? date1 : date2);
        }

        public static DateTime GetDateTimeForDateByHoursAndMinutes(int hour, int minute, DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetWeekOfYearIso8601(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        // This presumes that weeks start with Monday.
        public static int GetWeekOfYearNormal(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            /*DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }*/

            // Return the week of our adjusted day
            //return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            return GetWeekOfYearIso8601(time);
        }

        //Rounding dateTime - up, down or nearest
        //var date = new DateTime(2010, 02, 05, 10, 35, 25, 450); // 2010/02/05 10:35:25
        // roundedUp = date.RoundUp(TimeSpan.FromMinutes(15)); // 2010/02/05 10:45:00
        //var roundedDown = date.RoundDown(TimeSpan.FromMinutes(15)); // 2010/02/05 10:30:00
        //var roundedToNearest = date.RoundToNearest(TimeSpan.FromMinutes(15)); // 2010/02/05 10:30:00

        public static DateTime RoundUp(this DateTime dt, TimeSpan d)
        {
            var delta = (d.Ticks - (dt.Ticks % d.Ticks)) % d.Ticks;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }

        public static DateTime RoundDown(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }

        public static DateTime RoundNearest(this DateTime dt, TimeSpan d)
        {
            var delta = dt.Ticks % d.Ticks;
            bool roundUp = delta > d.Ticks / 2;

            return roundUp ? dt.RoundUp(d) : dt.RoundDown(d);
        }

        public static string GetMonthName(int month)
        {
            var result = string.Empty;
            switch (month)
            {
                case 1:
                    result = "January";
                    break;
                case 2:
                    result = "February";
                    break;
                case 3:
                    result = "March";
                    break;
                case 4:
                    result = "April";
                    break;
                case 5:
                    result = "May";
                    break;
                case 6:
                    result = "June";
                    break;
                case 7:
                    result = "July";
                    break;
                case 8:
                    result = "August";
                    break;
                case 9:
                    result = "September";
                    break;
                case 10:
                    result = "October";
                    break;
                case 11:
                    result = "November";
                    break;
                case 12:
                    result = "December";
                    break;
            }
            return result;
        }

        public static DateTime GetLastDateOfMonth(DateTime day)
        {
            var dateFirstDateNextMonth = GetFirstDateTimeOfMonth(day.AddMonths(1));
            return dateFirstDateNextMonth.AddDays(-1);
        }

        public static DateTime GetDateFromString(string dateString)
        {
            DateTime result = new DateTime();

            if (!string.IsNullOrEmpty(dateString))
            {
                //Marius - date is in format yyyy-mm-dd -> this returns error
                //    var year = int.Parse(dateString.Split('/')[2]);
                //    var month = int.Parse(dateString.Split('/')[0]);
                //    var day = int.Parse(dateString.Split('/')[1]);
                //    result = new DateTime(year, month, day);

                //Marius - new parsing method
                DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            }

            return result;
        }

        public static DateTime GetFirstDayOfCWWeek(int cwNr)
        {
            DateTime result = DateTime.Now;

            // get day of week from today
            var dayOfWeekToday = GetDayOfWeek(result);
            var firstDayOfCurrentWeek = result.AddDays(-dayOfWeekToday);

            // get current week number
            var currentWeekNr = GetWeekOfYearNormal(result);

            result = firstDayOfCurrentWeek.AddDays(-((currentWeekNr - cwNr)*7));

            return result;
        }
    }
}