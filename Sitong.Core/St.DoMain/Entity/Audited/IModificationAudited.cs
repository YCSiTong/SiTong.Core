using System;
using System.Collections.Generic;
using System.Text;

namespace St.DoMain.Entity.Audited
{
    /// <summary>
    /// 修改者审计模型
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface IModificationAudited<TUserKey>
        where TUserKey : struct
    {
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        TUserKey? LastModifierId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime? LastModifierTime { get; set; }
    }
}
