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

                // Search string
                string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                        Log.println(Helper.Folder,"Match: " + result[ctr].Value);

                    // Title
                    title = result[ctr].Groups[1].Value;

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Title: " + title);

                    // Author
                    author = Helper.ExtractValue(result[ctr].Value, "/user/", "class").Replace('"', ' ').TrimStart().TrimEnd();

                    if (string.IsNullOrEmpty(author))
                        author = Helper.ExtractValue(result[ctr].Value, " >", "</a>");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Author: " + author);

                    // Description
                    description = Helper.ExtractValue(result[ctr].Value, "dir=\"ltr\" class=\"yt-uix-redirect-link\">", "</div>");

                    if (string.IsNullOrEmpty(description))
                        description = Helper.ExtractValue(result[ctr].Value, "<div class=\"yt-lockup-description yt-ui-ellipsis yt-ui-ellipsis-2\" dir=\"ltr\">", "</div>");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Description: " + description);

                    // Duration
                    duration = Helper.ExtractValue(Helper.ExtractValue(result[ctr].Value, "id=\"description-id-", "span"), ": ", "<").Replace(".", "");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Duration: " + duration);

                    // Url
                    url = string.Concat("http://www.youtube.com/watch?v=", Helper.ExtractValue(result[ctr].Value, "watch?v=", "\""));

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Url: " + url);

                    // Thumbnail
                    thumbnail = "https://i.ytimg.com/vi/" + Helper.ExtractValue(result[ctr].Value, "watch?v=", "\"") + "/mqdefault.jpg";

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Thumbnail: " + thumbnail);

                    // Remove playlists
                    if (title != "__title__" && title != " ")
                    {
                        if (duration != "" && duration != " ")
                        {
                            // Add item to list
                            items.Add(new VideoSearchComponents(title, author, description, duration, url, thumbnail));
                        }
                    }
                }
            }

            return items;
        }
    }
}
