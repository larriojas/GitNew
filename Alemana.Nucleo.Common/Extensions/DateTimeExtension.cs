using System;

namespace Alemana.Nucleo.Common.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Calcula la diferencia en años, siempre positivo
        /// </summary>
        /// <param name="later"></param>
        /// <param name="earlier"></param>
        /// <returns></returns>
        public static int YearsDiference(this DateTime later, DateTime earlier)
        {
            if (earlier < later)
            {
                var aux = later;
                later = earlier;
                earlier = aux;
            }
            else if (earlier == new DateTime() || later == new DateTime())
                return -1;

            int age = earlier.Year - later.Year;

            if (earlier.Month < later.Month || (earlier.Month == later.Month && earlier.Day < later.Day))
                age--;

            return age;
        }

        /// <summary>
        /// Calcula la diferencia en años flotante, siempre positivo
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double TotalYearsDifference(this DateTime start, DateTime end)
        {
            // Get difference in total months.
            int months = ((end.Year - start.Year) * 12) + (end.Month - start.Month);

            // substract 1 month if end month is not completed
            if (end.Day < start.Day)
            {
                months--;
            }

            double totalyears = months / 12d;
            return Math.Abs(totalyears);
        }

        /// <summary>
        /// Calula la diferencia en meses, siempre positivo
        /// </summary>
        /// <param name="later"></param>
        /// <param name="earlier"></param>
        /// <returns></returns>
        public static int MonthsDiference(this DateTime later, DateTime earlier)
        {
            if (earlier < later)
            {
                var aux = later;
                later = earlier;
                earlier = aux;
            }
            else if (earlier == new DateTime() || later == new DateTime())
                return -1;

            var months = -1;
            var tempLaterDate = earlier;
            while (tempLaterDate.CompareTo(later) >= 0)
            {
                months++;
                tempLaterDate = tempLaterDate.AddMonths(-1);
            }
            tempLaterDate = tempLaterDate.AddMonths(1);
            return months;
        }

        /// <summary>
        /// Calcula la diferencia en semanas
        /// </summary>
        /// <param name="later"></param>
        /// <param name="earlier"></param>
        /// <returns></returns>
        public static int TotalWeeks(this DateTime later, DateTime earlier)
        {
            if (earlier < later)
            {
                var aux = later;
                later = earlier;
                earlier = aux;
            }
            else if (earlier == new DateTime() || later == new DateTime())
                return -1;

            return Math.Abs(later.Subtract(earlier).Days / 7);
        }
    }
}
