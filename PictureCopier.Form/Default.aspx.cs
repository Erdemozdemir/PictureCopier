using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
using System.Web.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using PictureCopier.Helpers;

namespace PictureCopier
{
    public partial class Default : Page
    {
        private const string UriTemplate = "{0}://{1}{2}";
        private const string SavePathTemplate = "{0}\\{1}";
        private string SavePath = "";

        protected void btnDownloadFile_OnClick(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                ltrMessage.Text = "There is some missing information";
                return;
            }

            var driver = new ChromeDriver();

            if (string.IsNullOrEmpty(txtUrl.Text)) { ltrMessage.Text = "Url cannot be null or empty string"; return; }
            var url = txtUrl.Text;
            driver.Url = url;

            //To maximize the window. 	
            driver.Manage().Window.Maximize();
            ScrollToBottom(driver);
            // For loading images
            var sleepTime = Convert.ToInt32(txtTimeToSleep.Text) * 1000;
            Thread.Sleep(sleepTime);

            var images = driver.FindElementsByTagName("img");
            DownloadImages(images);
            var divs = driver.FindElementsByTagName("div");
            DownloadImages(divs);

            driver.Close();
        }

        private static void ScrollToBottom(ChromeDriver driver)
        {
            IJavaScriptExecutor js = driver;

            // This  will scroll down the page by 100 pixel vertical		
            js.ExecuteScript("var scrolling=0; setInterval(function(){window.scrollBy(scrolling,scrolling+=100)},300)");
        }

        private void DownloadImages(ReadOnlyCollection<IWebElement> elements)
        {
            var client = new WebClient();
            var hostName = "";
            string fileName = "";
            Uri url = new Uri(txtUrl.Text);
            hostName = url.Authority;
            SavePath = SavePathTemplate.FormatString(txtSavePath.Text, hostName);

            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);

            foreach (var el in elements)
            {
                try
                {
                    var src = el.GetAttribute("src");
                    if (String.IsNullOrEmpty(src)) continue;
                    // src can contains only path without hostname like /Recources/Image/x.jpg
                    if (src.StartsWith("/"))
                        src = UriTemplate.FormatString(url.Scheme, hostName, src);

                    var lastIndex = src.LastIndexOf('/');
                    fileName = src.Substring(lastIndex, (src.Length - lastIndex));
                    //Instagram photos has ? * / < > etc. 
                    fileName=fileName.ReplaceIllegalCharacters();

                    //Instagram photos has '.net' extension/text
                    if (Path.GetExtension(fileName)!=".jpg"||Path.GetExtension(fileName)!=".png")
                        fileName = Path.ChangeExtension(fileName, ".jpg");

                    if (!File.Exists(SavePath + "\\" + fileName))
                        client.DownloadFile(src, SavePath + "\\" + fileName);
                }
                catch (Exception ex)
                {
                    ltrMessage.Text += "{0} {1} {2}".FormatString("Pictures cannot be download \n", fileName, ex.Message);
                }
            }


            client.Dispose();
        }
    }
}