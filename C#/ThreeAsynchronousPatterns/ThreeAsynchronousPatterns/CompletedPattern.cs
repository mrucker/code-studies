using System;
using System.Net;
using System.Threading;
using NUnit.Framework;

namespace ThreeAsynchronousModels
{
    [TestFixture]
    class CompletedPattern
    {
        [Test(Description="Only Works When There Is An Internet Connection")]
        public void DownloadStringCompletedExample()
        {
            var waitForAsync = new ManualResetEvent(false);
            var webClient    = new WebClient();
            string result    = null;

            webClient.DownloadStringCompleted += (sender, args) =>
            {
                if (args.Error != null)
                {
                    throw args.Error;
                }
                else
                {
                    result = args.Result;
                }

                waitForAsync.Set();
            };

            webClient.DownloadStringAsync(new Uri("http://www.google.com/"));

            waitForAsync.WaitOne();

            Assert.IsNotNull(result);

        }

        [Test(Description="Only Works When There Is An Internet Connection")]
        public void DownloadDataCompletedExample()
        {
            var waitForAsync = new ManualResetEvent(false);
            var webClient    = new WebClient();
            byte[] result    = null;

            webClient.DownloadDataCompleted += (sender, args) =>
            {
                if (args.Error != null)
                {
                    throw args.Error;
                }
                else
                {
                    result = args.Result;
                }
                
                waitForAsync.Set();
            };

            webClient.DownloadDataAsync(new Uri("http://www.google.com/"));

            waitForAsync.WaitOne();

            Assert.IsNotNull(result);
        }
    }
}
