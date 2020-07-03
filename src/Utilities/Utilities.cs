using System.Net;

namespace YouTubeSearch
{
	class Utilities
	{
        public static string HtmlDecode(string value)
        {
            try
            {
                return WebUtility.HtmlDecode(value);
            }
            catch { return value; }
        }
		
	public static bool IsValid(this string val)
	{
		return !string.IsNullOrEmpty(val) && !string.IsNullOrWhiteSpace(val);
	}
    }
}
