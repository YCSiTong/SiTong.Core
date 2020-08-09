using System.Collections.Generic;

namespace St.AutoMapper.Common
{
    /// <summary>
    /// 分页数据Dto
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PageResultDto<TEntity>
    {
        /// <summary>
        /// 总数据量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public IEnumerable<TEntity> Result { get; set; }
    }
}
