using System;

namespace Donum.Helper;

public static class DateTimeHelper
{
	public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp) =>
		new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();
}