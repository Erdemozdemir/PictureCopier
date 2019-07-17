using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PictureCopier
{
    class Program
    {
        private static readonly string savePath = "Images";

        static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            Console.WriteLine("Url");
            var url = "http://s2.hostamedia.com/ajwa";
            driver.Url = url;

            //To maximize the window. 	
            driver.Manage().Window.Maximize();
            ScrollToBottom(driver);
            // For loading images
            Thread.Sleep(15000);

            try
            {
                var images = driver.FindElementsByTagName("img");
                DownloadImages(images, savePath);
                var divs = driver.FindElementsByTagName("div");
                DownloadImages(divs, savePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Pictures cannot be download \n" + ex.Message);
            }


            Console.WriteLine("-----------------------------------Complete-------------------------------------");

            Console.ReadLine();
        }

        private static void ScrollToBottom(ChromeDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // This  will scroll down the page by 1000 pixel vertical		
            js.ExecuteScript("var scrolling=0; setInterval(function(){window.scrollBy(scrolling,scrolling+=300)},300)");
        }

        private static void DownloadImages(ReadOnlyCollection<IWebElement> elements, string savePath)
        {
            var client = new WebClient();
            foreach (var el in elements)
            {
                var src = el.GetAttribute("src");
                if (String.IsNullOrEmpty(src)) return;
                Console.WriteLine(el.GetAttribute("src"));
                var lastIndex = src.LastIndexOf('/');
                var fileName = src.Substring(lastIndex, (src.Length - lastIndex));

                client.DownloadFile(src, savePath+"\\"+fileName);
            }
            client.Dispose();
        }
    }
}
