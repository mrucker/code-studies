using System;
using System.Collections.Generic;
using System.Linq;
using EnumerableQueryableAndLinq.Watched;

namespace EnumerableQueryableAndLinq.WatchedExtensions
{
    public static class WatchedEnumerableExtensions
    {
        public static WatchedEnumerable<T> Where<T>(this WatchedEnumerable<T> watchedEnumerable, Func<T, bool> predicate)
        {
            var enumerableWhere = (watchedEnumerable as IEnumerable<T>).Where(predicate);
            
            return watchedEnumerable.New(enumerableWhere, "where");
        }
        
        public static List<T> ToList<T>(this WatchedEnumerable<T> watchedEnumerable)
        {
            return (watchedEnumerable as IEnumerable<T>).ToList();
        }
    }
}
