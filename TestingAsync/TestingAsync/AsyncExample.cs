using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestingAsync
{
    class AsyncExample
    {
         
        public static void Example()
        {
            Console.WriteLine("Returned");
            var r = GetTitleCsAsync(
                "http://www.pluralsight-training.net/microsoft/Products/Individual");
            Console.WriteLine("Returned");
            var t = GetTitleCsAsync("http://www.bbc.co.uk/news/");
            Console.WriteLine("Returned");

            Task<string>[] data = { t, r };
            var result = Task.WaitAny(data);
            Console.WriteLine(data[result].Result);

           // string title = t.Result;
           // Console.WriteLine(title);
           //title = r.Result;
           // Console.WriteLine(title);
            Console.ReadLine();
        }
        //this is the same as the one below but instead of a 
        static async Task<string> GetTitleCsAsync(string url)
        {
            using (var w = new WebClient())
            {
                string content = await w.DownloadStringTaskAsync(url);
                return ExtractTitle(content);
            }
        }

        static Task<string> GetTitleTplAsync(string url)
        {
            var w = new WebClient();
            Task<string> contentTask = w.DownloadStringTaskAsync(url);
            return contentTask.ContinueWith(t =>
            {
                string result = ExtractTitle(t.Result);
                w.Dispose();
                return result;
            });
        }
        static string GetTitle(string url)
        {
            using (var w = new WebClient())
            {
                string content = w.DownloadString(url);
                return ExtractTitle(content);
            }
        }

        private static string ExtractTitle(string content)
        {
            const string TitleTag = "<title>";
            var comp = StringComparison.InvariantCultureIgnoreCase;
            int titleStart = content.IndexOf(TitleTag, comp);
            if (titleStart < 0)
            {
                return null;
            }
            int titleBodyStart = titleStart + TitleTag.Length;
            int titleBodyEnd = content.IndexOf("</title>", titleBodyStart, comp);
            return content.Substring(titleBodyStart, titleBodyEnd - titleBodyStart);
        }
    }
}
