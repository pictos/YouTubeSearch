[![Nuget](https://img.shields.io/nuget/v/Mayerch1.YoutubeSearch)](https://www.nuget.org/packages/Mayerch1.GithubUpdateCheck/)

This is a direct fork of [mrklintscher/YoutubeSearch](https://github.com/mrklintscher/YoutubeSearch)

This fork is featuring .NET Core (without modification) and offers a NuGet package for the newest version (including Async support). If the original repo and NuGet is updated, this fork will not be continued.

# YoutubeSearch
YoutubeSearch is a library for .NET, written in C#, to show search query results from YouTube.

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
<br/>

# NuGet
**Install-Package YoutubeSearch.dll**

# License
YoutubeSearch is licensed under the **GPL** license.

# Example code
```c#
// Keyword
string querystring = "test";

// Number of result pages
int querypages = 1;

// Offset value for querypages
int querypagesOffset = 2;

var items = new VideoSearch();

//change the encoding, using Encoding.Default if not set
items.encoding = Encoding.UTF8; 

foreach (var item in items.SearchQuery(querystring, querypages))
{
    Console.WriteLine(item.Title);
}

//For asynchronous execution use:
var result = await items.SearchQueryAsync(querystring, querypages);

//This will query from page 3 to 4
var offsetResult = items.SearchQuery(querystring, querypages, querypagesOffset);
```

# Supported Items

- Title
- Author
- Description
- Duration
- Thumbnail
- Video Url
- View Count


