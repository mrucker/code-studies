using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EnumerableQueryableAndLinq.Watcher;

namespace EnumerableQueryableAndLinq.Watched
{
    public class WatchedQueryable<T>: IQueryable<T>
    {
        #region Properties
        public string Name { get; private set; }
        public IQueryable<T> Watched { get; private set; } 
        public IWatcher Watcher { private get; set; }
        #endregion

        #region Constructor
        public WatchedQueryable(IQueryable<T> watched, string name, IWatcher watcher)
        {
            Watched = watched;
            Name    = name;
            Watcher = watcher;
        }
        #endregion

        #region IQueryable Implementation
        public Expression Expression
        {
            get { return Watched.Expression; }
        }

        public Type ElementType
        {
            get { return Watched.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return Watched.Provider; }
        }
        
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
        public WatchedQueryable<TNew> New<TNew>(IQueryable<TNew> newWatched, string newName)
        {
            return new WatchedQueryable<TNew>(newWatched, newName, Watcher);
        }
        #endregion
    }
}
