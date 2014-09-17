using System;
using System.Threading;
using NUnit.Framework;

namespace ThreeAsynchronousModels
{
    [TestFixture]
    class BeginEndPattern
    {

        [Test]
        public void DelegateBeginInvokeWithoutCallback1()
        {
            Func<string> action = () =>
            {
                Thread.Sleep(3000);
                return "Async";
            };

            //Invoke the delegate asynchronously
            IAsyncResult asyncResult = action.BeginInvoke(null, null);

            //A call to EndInvoke while action is still running will block until action is finished
            string result = action.EndInvoke(asyncResult);

            Assert.AreEqual("Async", result);
        }

        [Test]
        public void DelegateBeginInvokeWithoutCallback2()
        {
            Func<string> action = () =>
            {
                Thread.Sleep(3000);
                return "Async";
            };

            IAsyncResult asyncResult = action.BeginInvoke(null, null);
            
            //waits until the delegate completes its work (in this case 3000 miliseconds)
            asyncResult.AsyncWaitHandle.WaitOne();

            string result = action.EndInvoke(asyncResult);

            Assert.AreEqual("Async", result);
        }

        [Test]
        public void DelegateBeginInvokeWithCallbackExample()
        {
            var waitForCallback  = new ManualResetEvent(false);
            string result        = null;
            var asyncStateIn     = new object();
            object asyncStateOut = null;


            Func<string> action = () =>
            {
                return "Async";
            };

            AsyncCallback callback = asyncResult =>
            {
                asyncStateOut = asyncResult.AsyncState;
                result        = action.EndInvoke(asyncResult);

                waitForCallback.Set();
            };

            WaitHandle waitForDelegate = action.BeginInvoke(callback, asyncStateIn).AsyncWaitHandle;

            waitForDelegate.WaitOne();

            waitForCallback.WaitOne();

            Assert.AreEqual("Async", result);
            Assert.AreSame(asyncStateIn, asyncStateOut);
        }
    }
}
