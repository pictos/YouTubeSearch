using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeSearch
{
    public class VideoSearch
    {
        static List<VideoSearchComponents> items;

        static string title;
        static string author;
        static string description;
        static string duration;
        static string url;
        static string thumbnail;
        static string viewcount;

        /// <summary>
        /// Search videos
        /// </summary>
        /// <param name="querystring"></param>
        /// <param name="querypages"></param>
        /// <returns></returns>
        public async Task<List<VideoSearchComponents>> GetVideos(string querystring, int querypages)
        {
            items = new List<VideoSearchComponents>();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                // Search address
                string content = await Web.getContentFromUrl("https://www.youtube.com/results?search_query=" + querystring + "&page=" + i);

                content = Helper.ExtractValue(content, "window[\"ytInitialData\"]", "window[\"ytInitialPlayerResponse\"]");

                // Search string
                string pattern = "videoRenderer.*?serviceEndpoint";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                        Log.println(Helper.Folder,"Match: " + result[ctr].Value);

                    // Title
                    title = Helper.ExtractValue(result[ctr].Value, "\"title\":{\"runs\":[{\"text\":\"", "\"}]").Replace(@"\u0026", "&");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Title: " + title);

                    // Author
                    author = Helper.ExtractValue(result[ctr].Value, "\"ownerText\":{\"runs\":[{\"text\":\"", "\",\"").Replace(@"\u0026", "&");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Author: " + author);

                    // Description
                    description = Helper.ExtractValue(result[ctr].Value, "descriptionSnippet\":{\"runs\":[{\"text\":\"", "\"}]},").Replace(@"\u0026", "&");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Description: " + description);

                    // Duration
                    duration = Helper.ExtractValue(result[ctr].Value, "lengthText\"", "viewCountText");
                    duration = Helper.ExtractValue(duration, "simpleText\":\"", "\"");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Duration: " + duration);

                    // Url
                    url = string.Concat("http://www.youtube.com/watch?v=", Helper.ExtractValue(result[ctr].Value, "videoId\":\"", "\""));

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Url: " + url);

                    // Thumbnail
                    thumbnail = Helper.ExtractValue(result[ctr].Value, "\"thumbnail\":{\"thumbnails\":[{\"url\":\"", "\"").Replace(@"\u0026", "&");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Thumbnail: " + thumbnail);

                    // View count
                    {
                        string strView = Helper.ExtractValue(result[ctr].Value, "\"viewCountText\":{\"simpleText\":\"", "\"},\"");
                        if (strView.IsValid())//if (!string.IsNullOrEmpty(strView) && !string.IsNullOrWhiteSpace(strView))
                        {
                            string[] strParsedArr =
                                strView.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                            string parsedText = strParsedArr[0];
                            parsedText = parsedText.Trim().Replace(",", ".");

                            viewcount = parsedText;
                        }
                    }

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Viewcount: " + viewcount);

                    // Remove playlists
                    if (title != "__title__" && title.IsValid()/*title != " "*/)
                    {
                        if (duration.IsValid())//if (duration != "" && duration != " ")
                        {
                            // Add item to list
                            items.Add(new VideoSearchComponents(Utilities.HtmlDecode(title), 
                                Utilities.HtmlDecode(author), Utilities.HtmlDecode(description), duration, url, thumbnail, viewcount));
                        }
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Search videos paged
        /// </summary>
        /// <param name="querystring"></param>
        /// <param name="querypage"></param>
        /// <returns></returns>
        public async Task<List<VideoSearchComponents>> GetVideosPaged(string querystring, int querypagenum)
        {
            items = new List<VideoSearchComponents>();

            // Do search
            // Search address
            string content = await Web.getContentFromUrl("https://www.youtube.com/results?search_query=" + querystring + "&page=" + querypagenum);

            content = Helper.ExtractValue(content, "window[\"ytInitialData\"]", "window[\"ytInitialPlayerResponse\"]");

            // Search string
            string pattern = "videoRenderer.*?serviceEndpoint";
            MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

            for (int ctr = 0; ctr <= result.Count - 1; ctr++)
            {
                if (Log.getMode())
                    Log.println(Helper.Folder, "Match: " + result[ctr].Value);

                // Title
                title = Helper.ExtractValue(result[ctr].Value, "\"title\":{\"runs\":[{\"text\":\"", "\"}]").Replace(@"\u0026", "&");

                if (Log.getMode())
                    Log.println(Helper.Folder, "Title: " + title);

                // Author
                author = Helper.ExtractValue(result[ctr].Value, "\"ownerText\":{\"runs\":[{\"text\":\"", "\",\"").Replace(@"\u0026", "&");

                if (Log.getMode())
                    Log.println(Helper.Folder, "Author: " + author);

                // Description
                description = Helper.ExtractValue(result[ctr].Value, "descriptionSnippet\":{\"runs\":[{\"text\":\"", "\"}]},").Replace(@"\u0026", "&");

                if (Log.getMode())
                    Log.println(Helper.Folder, "Description: " + description);

                // Duration
                duration = Helper.ExtractValue(result[ctr].Value, "lengthText\"", "viewCountText");
                duration = Helper.ExtractValue(duration, "simpleText\":\"", "\"");

                if (Log.getMode())
                    Log.println(Helper.Folder, "Duration: " + duration);

                // Url
                url = string.Concat("http://www.youtube.com/watch?v=", Helper.ExtractValue(result[ctr].Value, "videoId\":\"", "\""));

                if (Log.getMode())
                    Log.println(Helper.Folder, "Url: " + url);

                // Thumbnail
                thumbnail = Helper.ExtractValue(result[ctr].Value, "\"thumbnail\":{\"thumbnails\":[{\"url\":\"", "\"").Replace(@"\u0026", "&");

                if (Log.getMode())
                    Log.println(Helper.Folder, "Thumbnail: " + thumbnail);

                // View count
                {
                    string strView = Helper.ExtractValue(result[ctr].Value, "\"viewCountText\":{\"simpleText\":\"", "\"},\"");
                    if(strView.IsValid())//if (!string.IsNullOrEmpty(strView) && !string.IsNullOrWhiteSpace(strView))
                    {
                        string[] strParsedArr =
                            strView.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        string parsedText = strParsedArr[0];
                        parsedText = parsedText.Trim().Replace(",", ".");

                        viewcount = parsedText;
                    }
                }

                if (Log.getMode())
                    Log.println(Helper.Folder, "Viewcount: " + viewcount);

                // Remove playlists
                if (title != "__title__" && title.IsValid() /*!= " "*/)
                {
                    if  (duration.IsValid())//(!string.IsNullOrEmpty(duration) && duration != " ") // The second condition can be !string.IsNullOrWhiteSpace(duration).
                    {
                        // Add item to list
                        items.Add(new VideoSearchComponents(Utilities.HtmlDecode(title),
                            Utilities.HtmlDecode(author), Utilities.HtmlDecode(description), duration, url, thumbnail, viewcount));
                    }
                }
            }

            return items;
        }
    }
}
