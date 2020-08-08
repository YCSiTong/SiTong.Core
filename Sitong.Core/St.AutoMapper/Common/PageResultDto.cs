
namespace St.AutoMapper.Common
{
    public class PageResultDto<TEntity>
    {
        /// <summary>
        /// 总数据量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public TEntity Result { get; set; }
    }
}
