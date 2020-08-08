using System;

namespace St.DoMain.Entity.Audited
{
    /// <summary>
    /// 创造者审计模型
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface ICreationAudited<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// 创造者Id
        /// </summary>
        TPrimaryKey CreatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedTime { get; set; }
    }
}
