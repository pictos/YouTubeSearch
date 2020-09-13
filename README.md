# YouTubeSearch ![YouTubeSearch](https://i.ibb.co/XkmN09L/1200px-Logo-of-You-Tube-2013-2015-svg.jpg)
An complete private YouTube Api for .NET (C#, VB.NET).

YouTubeSearch is a library for .NET, written in C#, to search and extract the download link from YouTube videos and download them.

| Target | Branch | Version | Download link |
| ------ | ------ | ------ | ------ |
| Nuget | master | v2.2.4 | [![NuGet](https://img.shields.io/badge/nuget-v2.2.4-blue)](https://www.nuget.org/packages/YouTubeSearch/) |

# Target platforms

This library is using .NET Standard 2.0 and is therefore compatible with the following platforms (see [Microsoft Docs](https://docs.microsoft.com/de-de/dotnet/standard/net-standard#net-implementation-support)).
- .NET Framework 4.6.1+
- .NET Core 2.0+
- Mono 5.4+
- Xamarin.iOS 10.14+
- Xamarin.Mac 3.8+
- Xamarin.Android 8.0+
- UWP 10.0.16299+
- Unity 2018.1+

# Example code
```c#
>> Search <<

string querystring = "Usher";
int querypages = 1;

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

>> Download <<

string link = "https://www.youtube.com/watch?v=daKz_b7LrsE";

IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link, false);

DownloadVideo(videoInfos);
```

# Supported Items

- Title
- Author
- Description
- Duration
- Thumbnail
- Url
- View count
<br>
Torsten Klinger - (c) 2020 | Nuremberg, Germany.
