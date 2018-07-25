using System.Linq;
using EnumerableQueryableAndLinq.Watched;
using EnumerableQueryableAndLinq.WatchedExtensions;
using EnumerableQueryableAndLinq.Watcher;
using NUnit.Framework;

namespace EnumerableQueryableAndLinq.BehaviorTests
{
    [TestFixture]
    public class BehaviorOfQueryable
    {
        private IQueryable<int> _integerQueryable;
        private WatchedQueryable<int> _watchedQueryable;
        private ListWatcher _listWatcher = new ListWatcher();

        [SetUp]
        public void Setup()
        {
            _listWatcher = new ListWatcher();
            _integerQueryable = (new[] { 1, 2, 3, 4, 5 }).AsQueryable();
            _watchedQueryable = new WatchedQueryable<int>(_integerQueryable, "base", _listWatcher);
        }

        [Test]
        public void QueryableLinqEnumeration()
        {
            _watchedQueryable.ToList();

            Assert.AreEqual(1, _listWatcher.Messages.Count());
            Assert.AreEqual("base enumeration", _listWatcher.Messages.First());
        }

        [Test]
        public void QueryableLinqOnlyUnwindsFinalStatement()
        {
            var result = _watchedQueryable.Where(i => i > 2).Where(i => i > 3).ToList();

            //only the final statement is enumerated because the expression from the base and 
            //the first where are all contained in the final linq statement when using IQueryable
            Assert.AreEqual(1, _listWatcher.Messages.Count());
            Assert.AreEqual("where enumeration", _listWatcher.Messages.ElementAt(0));
        }

        [Test]
        public void QueryableLinqDelayedExecution()
        {
            var watchedQueryable_Where_Where = _watchedQueryable.Where(i => i > 2).Where(i => i > 3);

            //no enumeration of LINQ statement until the values are needed
            Assert.AreEqual(0, _listWatcher.Messages.Count());

            //ToList() is just one of many ways to force an enumeration of a LINQ statement
            var result = watchedQueryable_Where_Where.ToList();

            //now that ToList() has been called the LINQ statement is evaluated
            Assert.AreEqual(1, _listWatcher.Messages.Count());
            Assert.AreEqual("where enumeration", _listWatcher.Messages.ElementAt(0));
        }
    }
}
