using System;
using System.Linq;
using System.Linq.Expressions;

namespace St.Extensions
{
    /// <summary>
    /// IQueryable延迟加载扩展
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
            query.NotNull(nameof(query));
            expression.NotNull(nameof(Expression<Func<T, bool>>));

            if (verify)
                return query.Where(expression);
            else
                return query;
        }

        /// <summary>
        /// 数据筛选分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="resultCount">返回数据量</param>
        /// <returns></returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int skipCount, int resultCount)
        {
            query.NotNull(nameof(query));
            skipCount.IsPositive(nameof(skipCount));
            resultCount.CustomVerify(resultCount > 0, "resultCount必须为>0的正整数");

            return query.Skip(skipCount).Take(resultCount);
        }

    }
}
