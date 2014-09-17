using System.Linq;
using EnumerableQueryableAndLinq.Watched;
using EnumerableQueryableAndLinq.WatchedExtensions;
using EnumerableQueryableAndLinq.Watcher;
using NUnit.Framework;
using System.Collections.Generic;

namespace EnumerableQueryableAndLinq.BehaviorTests
{
    [TestFixture]
    public class BehaviorOfEnumerable
    {
        private IEnumerable<int> _integerEnumerable;
        private WatchedEnumerable<int> _watchedEnumerable;
        private ListWatcher _listWatcher;
        
        [SetUp]
        public void Setup()
        {
            _integerEnumerable = new[] { 1, 2, 3, 4 };
            _listWatcher       = new ListWatcher();
            _watchedEnumerable = new WatchedEnumerable<int>(_integerEnumerable, "base", _listWatcher);
        }

        [Test]
        public void EnumerableLinqEnumeration()
        {
            _watchedEnumerable.ToList();

            Assert.AreEqual(1, _listWatcher.Messages.Count());
            Assert.AreEqual("base enumeration", _listWatcher.Messages.First());
        }

        [Test]
        public void EnumerableLinqUnwindsAllStatements()
        {
            var result = _watchedEnumerable.Where(i => i > 2).Where(i => i > 3).ToList();

            //_watchedEnumerable, Where, and Where are each unwound after calling ToList()
            Assert.AreEqual(3, _listWatcher.Messages.Count());
            Assert.AreEqual("where enumeration", _listWatcher.Messages.ElementAt(0));
            Assert.AreEqual("where enumeration", _listWatcher.Messages.ElementAt(1));
            Assert.AreEqual("base enumeration", _listWatcher.Messages.ElementAt(2));
        }

        [Test]
        public void EnumerableLinqDelayedExecution()
        {
            var watchedEnumerable_Where_Where = _watchedEnumerable.Where(i => i > 2).Where( i => i > 3);

            //no enumeration of LINQ statement until the values are needed
            Assert.AreEqual(0, _listWatcher.Messages.Count());

            //ToList() is just one of many ways to force an enumeration of a LINQ statement
            var result = watchedEnumerable_Where_Where.ToList();

            //now that ToList() has been called the LINQ statement is evaluated
            Assert.AreEqual(3, _listWatcher.Messages.Count());
            Assert.AreEqual("where enumeration", _listWatcher.Messages.ElementAt(0));
            Assert.AreEqual("where enumeration", _listWatcher.Messages.ElementAt(1));
            Assert.AreEqual("base enumeration", _listWatcher.Messages.ElementAt(2));
        }
    }
}
