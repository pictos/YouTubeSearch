using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeSearch;

namespace Example_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Keyword
            string querystring = "test";

            // Number of result pages
            int querypages = 1;

            ////////////////////////////////
            // Starting searchquery
            ////////////////////////////////
            
            var items = new VideoSearch();

            //specify the encoding for this object
            items.encoding = Encoding.UTF8;
            

            int i = 1;

            foreach (var item in items.SearchQuery(querystring, querypages))
            {
                Console.WriteLine(i + ". ###########################");
                Console.WriteLine("Title: " + item.Title);
                Console.WriteLine("Author: " + item.Author);
                Console.WriteLine("Description: " + item.Description);
                Console.WriteLine("Duration: " + item.Duration);
                Console.WriteLine("Url: " + item.Url);
                Console.WriteLine("Thumbnail: " + item.Thumbnail);
                Console.WriteLine("");

                i++;
            }

            //----------------------------------------
            //see difference of sync and async
            SyncQuery();
            AsyncQuery();
            OffsetQuery();


            Console.ReadLine();
        }    

        //example for synchronous execution
        private static void SyncQuery()
        {
            var querystring = "test";
            int querypages = 1;

            var items = new VideoSearch();

            foreach (var item in items.SearchQuery(querystring, querypages))
            {
                Console.WriteLine("##########################");
                Console.WriteLine(item.Title);
                Console.WriteLine("");
            }
        }

        //example for asynchronous execution
        private static async void AsyncQuery()
        {
            var querystring = "test";
            int querypages = 1;

            var items = new VideoSearch();
            foreach (var item in await items.SearchQueryTaskAsync(querystring, querypages))
            {
                Console.WriteLine("##########################");
                Console.WriteLine(item.Title);
                Console.WriteLine("");
            }
        }

        //example for using the offset value
        private static void OffsetQuery()
        {
            //this function will return all results from page 1 to page 10
            var querystring = "test";
            int querypages = 1;
            int querypagesOffset;

            //create list and search
            var pages = new List<List<VideoInformation>>();
            var items = new VideoSearch();

            for (int i = 0; i < 10; i++)
            {
                //increase offset
                querypagesOffset = i;

                //each iteration will return the next page
                //useful, if pages are needed in a sequence, in order to reduce waiting time
                pages.Add(items.SearchQuery(querystring, querypages, querypagesOffset));
            }
            //page 1-10 (index in list 0-9) is now stored in 'pages'
        }
    }
}
