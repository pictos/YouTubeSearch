using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeSearch
{
    class Web
    {
        static WebClient webclient;

        public static async Task<String> getContentFromUrl(String Url)
        {
            try
            {
                webclient = new WebClient();
                webclient.Encoding = Encoding.UTF8;

                Task<string> downloadStringTask = webclient.DownloadStringTaskAsync(new Uri(Url));
                var content = await downloadStringTask;

                webclient.DownloadStringAsync(new Uri(Url));

                return content.Replace('\r', ' ').Replace('\n', ' ');
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
