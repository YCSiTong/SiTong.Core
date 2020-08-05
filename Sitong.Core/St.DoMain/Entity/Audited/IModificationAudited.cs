using System;

namespace St.DoMain.Entity.Audited
{
    /// <summary>
    /// 修改者审计模型
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IModificationAudited<TPrimaryKey> : IAggregateRoot<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        TPrimaryKey? LastModifierId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime? LastModifierTime { get; set; }
    }
}
