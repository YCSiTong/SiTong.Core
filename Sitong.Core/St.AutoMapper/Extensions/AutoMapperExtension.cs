using AutoMapper;
using St.Extensions;
using System.Collections.Generic;

namespace St.AutoMapper.Extensions
{
    public static class AutoMapperExtension
    {
        private static IMapper _mapper;
        /// <summary>
        /// 校验是否注入Mapper
        /// </summary>
        private static void CheckMapper()
        {
            CheckException.CustomVerify(_mapper.IsNull(), "使用AutoMapper转换扩展失败,请检查是否注入获取`IMapper`");
        }

        /// <summary>
        /// 初始化加载Mapper获取
        /// </summary>
        /// <param name="mapper"></param>
        public static void InitMapper(IMapper mapper)
        {
            mapper.NotNull(nameof(mapper));
            _mapper = mapper;
        }


        /// <summary>
        /// 将对象映射指定类型
        /// </summary>
        /// <typeparam name="TResult">需映射类型</typeparam>
        /// <param name="val">当前对象</param>
        /// <returns></returns>
        public static TResult ToMap<TResult>(this object val)
        {
            CheckMapper();
            if (val.IsNotNull())
                return _mapper.Map<TResult>(val);
            return default;
        }
        /// <summary>
        /// 将源对象映射赋值至目标源
        /// </summary>
        /// <typeparam name="TSouce">源对象类型</typeparam>
        /// <typeparam name="TResult">目标源类型</typeparam>
        /// <param name="val">源对象</param>
        /// <param name="result">目标源</param>
        /// <returns></returns>
        public static TResult ToMap<TSouce, TResult>(this TSouce val, TResult result)
        {
            CheckMapper();
            if (val.IsNotNull() && result.IsNotNull())
                return _mapper.Map(val, result);
            return default;
        }
        /// <summary>
        /// 将<see cref="IEnumerable{T}"/>对象映射指定<see cref="IEnumerable{T}"/>类型
        /// </summary>
        /// <typeparam name="TResult">需映射类型</typeparam>
        /// <param name="val">当前对象</param>
        /// <returns></returns>
        public static IEnumerable<TResult> ToMap<TResult>(this IEnumerable<object> val)
        {
            CheckMapper();
            if (val.IsNotNull())
                return _mapper.Map<IEnumerable<TResult>>(val);
            return default;
        }
    }
}
