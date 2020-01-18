using System;

public class DateTimeHelper
{
    public DateTimeHelper()
    {
    }
}

public static class DateTimeExtensions
{
    public static int ToUnixTimeStamp(this DateTime date)
    {
        return Convert.ToInt32((date - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
    }

    public static bool IsBetween(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
    {
        return dateToCheck >= startDate && dateToCheck <= endDate;
    }
}