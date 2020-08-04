using System;

namespace St.DoMain.Entity.Audited
{
    /// <summary>
    /// 创造者审计模型
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface ICreationAudited<TUserKey>
        where TUserKey : struct
    {
        /// <summary>
        /// 创造者Id
        /// </summary>
        TUserKey CreatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedTime { get; set; }
    }
}
