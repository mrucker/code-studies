using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ThreeAsynchronousModels
{
    [TestFixture]
    class AsyncAwaitPattern
    {
        [Test(Description = "Only Works When There Is An Internet Connection")]
        public async void AsyncAwaitExample()
        {
            using (var httpClient = new HttpClient())
            {
                string result = await httpClient.GetStringAsync(new Uri("http://www.google.com"));

                Assert.IsNotNull("Async", result);
            }
        }

        [Test]
        [ExpectedException(typeof(HttpRequestException), ExpectedMessage = "An error occurred while sending the request.")]
        public async void AsyncAwaitErrorHandling()
        {
            using (var httpClient = new HttpClient())
            {
                await httpClient.GetStringAsync(new Uri("http://A_Website_That_Doesnt_Exist"));
            }
        }

        [Test]
        public async void CustomTaskAwaiting()
        {
            string result = await Task.Run(() => "My Await");

            Assert.AreEqual("My Await", result);

        }
    }
}
