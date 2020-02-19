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
            Search();
            //Download();
            Console.ReadLine();
        }

        static async void Search()
        {
            // Disable logging
            Log.setMode(false);

            // Keyword
            string querystring = "Usher";

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

        static void Download()
        {
            // Disable logging
            Log.setMode(false);

            // YouTube url
            string link = "https://www.youtube.com/watch?v=daKz_b7LrsE";

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
