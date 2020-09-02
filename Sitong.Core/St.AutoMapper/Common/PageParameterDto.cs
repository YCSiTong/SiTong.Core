using St.Extensions;

namespace St.AutoMapper.Common
{
    /// <summary>
    /// 公共分页请求参数
    /// </summary>
    public class PageParameterDto
    {

        private int _skipCount;
        /// <summary>
        /// 跳过数量
        /// </summary>
        public int SkipCount
        {
            get => _skipCount;
            set
            {
                _skipCount.IsPositive(nameof(SkipCount));
                _skipCount = value;
            }
        }


        private int _maxResultCount;
        /// <summary>
        /// 返回数据量
        /// </summary>
        public int MaxResultCount
        {
            get
            {
                if (_maxResultCount == 0)
                    return _maxResultCount = 10;
                else
                    return _maxResultCount;
            }
            set
            {
                _maxResultCount.IsPositive(nameof(MaxResultCount));
                if (value == 0)
                    _maxResultCount = 10;
                else
                    _maxResultCount = value;
            }
        }
    }
}
