using System;
using System.Diagnostics.CodeAnalysis;

namespace Contracts.Api.Domain.Util
{
    [ExcludeFromCodeCoverage]
    public static class Dates
    {
        public static DateTime Peru(this DateTime date, string TimeZone)
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
            DateTime cstTime = TimeZoneInfo.ConvertTime(date, cstZone);
            return cstTime;
        }

        public static string GetMonthName(this DateTime date)
        {
            switch (date.Month) {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Setiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default: return "";
            }
        }
    }
}
