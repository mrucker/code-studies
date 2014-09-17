using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EnumerableQueryableAndLinq.Watched;

namespace EnumerableQueryableAndLinq.WatchedExtensions
{
    public static class WatchedQueryableExtensions
    {
        public static WatchedQueryable<T> Where<T>(this WatchedQueryable<T> watchedQueryable, Expression<Func<T, bool>> predicate)
        {
            var queryableWhere = (watchedQueryable as IQueryable<T>).Where(predicate);

            return watchedQueryable.New(queryableWhere, "where");
        }

        public static List<T> ToList<T>(this WatchedQueryable<T> watchedEnumerable)
        {
            return (watchedEnumerable as IQueryable<T>).ToList();
        }
    }
}
