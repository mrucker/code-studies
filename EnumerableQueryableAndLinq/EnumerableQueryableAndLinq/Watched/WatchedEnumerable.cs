using System.Collections;
using System.Collections.Generic;
using EnumerableQueryableAndLinq.Watcher;

namespace EnumerableQueryableAndLinq.Watched
{
    public class WatchedEnumerable<T>: IEnumerable<T>
    {
        #region Properties
        public string Name { get; private set; }
        public IEnumerable<T> Watched { get; private set; }
        public IWatcher Watcher { private get; set; }
        #endregion

        #region Constructor
        public WatchedEnumerable(IEnumerable<T> watched, string name, IWatcher watcher)
        {
            Watched = watched;
            Name    = name;
            Watcher = watcher;
        }
        #endregion

        #region IEnumerable Implementation
        public IEnumerator<T> GetEnumerator()
        {
            Watcher.Notify(Name + " enumeration");

            return Watched.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Public Methods
        public WatchedEnumerable<TNew> New<TNew>(IEnumerable<TNew> newWatched, string newName)
        {
            return new WatchedEnumerable<TNew>(newWatched, newName, Watcher);
        }
        #endregion
    }
}
