using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YouTubeSearch
{
    public class ChannelSearch
    {
        static List<ChannelSearchComponents> items;

        private String Id;
        private String Title;
        private String Description;
        private String VideoCount;
        private String SubscriberCount;
        private String Thumbnail;
        private String Url;

        public async Task<List<ChannelSearchComponents>> GetChannels(string querystring, int querypages)
        {
            items = new List<ChannelSearchComponents>();

            // Do search
            for (int i = 1; i <= querypages; i++)
            {
                // Search address
                string content = await Web.getContentFromUrlWithProperty("https://www.youtube.com/results?search_query=" + querystring + "&sp=EgIQAg%253D%253D&page=" + i);

                // Search string
                string pattern = "channelRenderer\":\\{\"channelId\":\"(?<ID>.*?)\".*?\",\"title\":\\{\"simpleText\":\"(?<TITLE>.*?)\".*?\\{\"webCommandMetadata\":\\{\"url\":\"(?<URL>.*?)\".*?\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?descriptionSnippet\":\\{\"runs\":\\[\\{\"text\":\"(?<DESCRIPTION>.*?)\".*?videoCountText\":\\{\"runs\":\\[\\{\"text\":\"(?<VIDEOCOUNT>.*?)\".*?subscriberCountText\":\\{\"simpleText\":\"(?<SUBSCRIBERCOUNT>.*?) .*?\"";
                MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    if (Log.getMode())
                        Log.println(Helper.Folder, "Match: " + result[ctr].Value);

                    // Id
                    Id = result[ctr].Groups[1].Value;

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Id: " + Id);

                    // Title
                    Title = result[ctr].Groups[2].Value;

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Title: " + Title);

                    // Description
                    Description = result[ctr].Groups[5].Value;

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Description: " + Description);

                    // VideoCount
                    VideoCount = result[ctr].Groups[6].Value;

                    if (VideoCount.Contains(" ")) // -> 1 Video
                        VideoCount = VideoCount.Replace(" Video", "");

                    if (Log.getMode())
                        Log.println(Helper.Folder, "VideoCount: " + VideoCount);

                    // SubscriberCount
                    SubscriberCount = result[ctr].Groups[7].Value;

                    if (Log.getMode())
                        Log.println(Helper.Folder, "SubscriberCount: " + SubscriberCount);

                    // Thumbnail
                    Thumbnail = "https:" + result[ctr].Groups[4].Value;

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);

                    // Url
                    Url = "http://youtube.com" + result[ctr].Groups[3].Value;

                    if (Log.getMode())
                        Log.println(Helper.Folder, "Url: " + Url);

                    // Add item to list
                    items.Add(new ChannelSearchComponents(Id, Utilities.HtmlDecode(Title),
                        Utilities.HtmlDecode(Description), VideoCount, SubscriberCount, Url, Thumbnail));
                }
            }

            return items;
        }

        public async Task<List<ChannelSearchComponents>> GetPlaylistsPaged(string querystring, int querypagenum)
        {
            items = new List<ChannelSearchComponents>();

            // Do search
            // Search address
            string content = await Web.getContentFromUrlWithProperty("https://www.youtube.com/results?search_query=" + querystring + "&sp=EgIQAg%253D%253D&page=" + querypagenum);

            // Search string
            string pattern = "channelRenderer\":\\{\"channelId\":\"(?<ID>.*?)\".*?\",\"title\":\\{\"simpleText\":\"(?<TITLE>.*?)\".*?\\{\"webCommandMetadata\":\\{\"url\":\"(?<URL>.*?)\".*?\\{\"thumbnails\":\\[\\{\"url\":\"(?<THUMBNAIL>.*?)\".*?descriptionSnippet\":\\{\"runs\":\\[\\{\"text\":\"(?<DESCRIPTION>.*?)\".*?videoCountText\":\\{\"runs\":\\[\\{\"text\":\"(?<VIDEOCOUNT>.*?)\".*?subscriberCountText\":\\{\"simpleText\":\"(?<SUBSCRIBERCOUNT>.*?).*?\"";
            MatchCollection result = Regex.Matches(content, pattern, RegexOptions.Singleline);

            for (int ctr = 0; ctr <= result.Count - 1; ctr++)
            {
                if (Log.getMode())
                    Log.println(Helper.Folder, "Match: " + result[ctr].Value);

                // Id
                Id = result[ctr].Groups[1].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "Id: " + Id);

                // Title
                Title = result[ctr].Groups[2].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "Title: " + Title);

                // Description
                Description = result[ctr].Groups[5].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "Description: " + Description);

                // VideoCount
                VideoCount = result[ctr].Groups[6].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "VideoCount: " + VideoCount);

                // SubscriberCount
                SubscriberCount = result[ctr].Groups[7].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "SubscriberCount: " + SubscriberCount);

                // Thumbnail
                Thumbnail = "https:" + result[ctr].Groups[4].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "Thumbnail: " + Thumbnail);

                // Url
                Url = "http://youtube.com" + result[ctr].Groups[3].Value;

                if (Log.getMode())
                    Log.println(Helper.Folder, "Url: " + Url);

                // Add item to list
                items.Add(new ChannelSearchComponents(Id, Utilities.HtmlDecode(Title),
                    Utilities.HtmlDecode(Description), VideoCount, SubscriberCount, Url, Thumbnail));
            }

            return items;
        }
    }
}
