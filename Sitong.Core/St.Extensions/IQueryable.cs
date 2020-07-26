using System;
using System.Linq;
using System.Linq.Expressions;

namespace St.Extensions
{
    /// <summary>
    /// <see cref="IQueryable{T}"/>
    /// </summary>
    public static class IQueryable
    {
        /// <summary>
        /// 根据校验条件,是否追加该<see cref="Expression{Func{T,Boolean}}"/>条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="verify">校验条件</param>
        /// <param name="expression">追加条件</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool verify, Expression<Func<T, bool>> expression)
        {
            query.NotNull(nameof(IQueryable<T>));

            if (verify)
                return query.Where(expression);
            else
                return query;
        }
    }
}
