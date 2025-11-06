namespace Porter.Common.Domain.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static DateTime ToBrazilDatetime(this DateTime date)
        {
            TimeZoneInfo brazilZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");

            DateTime brazilTZ = TimeZoneInfo.ConvertTimeFromUtc(date.ToUniversalTime(), brazilZone);

            return brazilTZ;
        }
    }


}
