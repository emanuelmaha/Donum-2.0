namespace Donum.Helper;

public static class DateTimeHelper
{
	public static DateTime UnixTimeStampToDateTime(this string unixTimeStamp) =>
		DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTimeStamp)).LocalDateTime;
}