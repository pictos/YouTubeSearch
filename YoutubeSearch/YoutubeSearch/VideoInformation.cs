// This file is part of YoutubeSearch.
//
// YoutubeSearch is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// YoutubeSearch is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with YoutubeSearch. If not, see<http://www.gnu.org/licenses/>.

namespace YoutubeSearch
{
    public class VideoInformation
    {
        /// <summary>
        /// Title of the video
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Name of the Youtube Channel
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Description of the video as shown in the browser. Empty string if not provided by Youtube
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Duration of the video (hh:mm:ss)
        /// </summary>
        public string Duration { get; set; }
        /// <summary>
        /// Full browser url to the video
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// url to the thumbnail (quality is "mqdefault")
        /// </summary>
        public string Thumbnail { get; set; }
        /// <summary>
        /// True if video has no description.
        /// </summary>
        public bool NoDescription { get; set; }

        public bool NoAuthor { get; set; }
        /// <summary>
        /// Number of views at the moment of the request. This is the number which is shown in the browser and not necessarily the true view number.
        /// </summary>
        public string ViewCount { get; set; }
    }
}
