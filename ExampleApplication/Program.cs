using System;
using System.Collections.Generic;
using System.Linq;

using YouTubeSearch;

namespace ExampleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
            //Search();
            //Download();
            //Search_Playlist();
            //Search_PlaylistItems();
            //Search_Channel();
            Search_ChannelItems();
            Console.ReadLine();
        }

        static async void Search()
        {
            // Disable logging
            Log.setMode(false);

            // Keyword
            string querystring = "Kurdo";

            // Number of result pages
            int querypages = 1;

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            VideoSearch videos = new VideoSearch();
            var items = await videos.GetVideos(querystring, querypages);

            foreach (var item in items)
            {
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("Description: " + item.getDescription());
                Console.WriteLine("Duration: " + item.getDuration());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("ViewCount: " + item.getViewCount());
                Console.WriteLine("");
            }
        }

        static async void Search_PlaylistItems()
        {
            // Disable logging
            Log.setMode(false);

            // Url
            string playlisturl = "https://www.youtube.com/channel/UCU8Xw_KewvcC3LV3Qv--JZg";

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            PlaylistItemsSearch playlistItems = new PlaylistItemsSearch();
            var items = await playlistItems.GetPlaylistItems(playlisturl);

            foreach (var item in items)
            {
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("Duration: " + item.getDuration());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("");
            }
        }

        static async void Search_ChannelItems()
        {
            // Disable logging
            Log.setMode(false);

            // Url
            string channelurl = "https://www.youtube.com/channel/UCU8Xw_KewvcC3LV3Qv--JZg";

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            ChannelItemsSearch channelItems = new ChannelItemsSearch();
            var items = await channelItems.GetChannelItems(channelurl);

            foreach (var item in items)
            {
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("Duration: " + item.getDuration());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("");
            }
        }

        static async void Search_Playlist()
        {
            // Disable logging
            Log.setMode(false);

            // Keyword
            string querystring = "Bushido";

            // Number of result pages
            int querypages = 1;

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            PlaylistSearch playlist = new PlaylistSearch();
            var items = await playlist.GetPlaylists(querystring, querypages);

            foreach (var item in items)
            {
                Console.WriteLine("Id: " + item.getId());
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Author: " + item.getAuthor());
                Console.WriteLine("VideoCount: " + item.getVideoCount());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("");
            }
        }

        static async void Search_Channel()
        {
            // Disable logging
            Log.setMode(false);

            // Keyword
            string querystring = "Bushido";

            // Number of result pages
            int querypages = 1;

            ////////////////////////////////
            // Start searching
            ////////////////////////////////

            ChannelSearch channel = new ChannelSearch();
            var items = await channel.GetChannels(querystring, querypages);

            foreach (var item in items)
            {
                Console.WriteLine("Id: " + item.getId());
                Console.WriteLine("Title: " + item.getTitle());
                Console.WriteLine("Description: " + item.getDescription());
                Console.WriteLine("VideoCount: " + item.getVideoCount());
                Console.WriteLine("SubscriberCount: " + item.getSubscriberCount());
                Console.WriteLine("Thumbnail: " + item.getThumbnail());
                Console.WriteLine("Url: " + item.getUrl());
                Console.WriteLine("");
            }
        }

        static void Download()
        {
            // Disable logging
            Log.setMode(false);

            // YouTube url
            string link = "https://www.youtube.com/watch?v=LN--3zgY5oM";

            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link, false);

            DownloadVideo(videoInfos);
        }

        private static void DownloadVideo(IEnumerable<VideoInfo> videoInfos)
        {
            // Select the first .mp4 video with 360p resolution
            VideoInfo video = videoInfos
                .First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

            // Decrypt only if needed
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            // Create the video downloader.
            VideoDownloader dl = new VideoDownloader();
            dl.DownloadFile(video.DownloadUrl, video.Title, true, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), video.VideoExtension);
        }
    }
}
