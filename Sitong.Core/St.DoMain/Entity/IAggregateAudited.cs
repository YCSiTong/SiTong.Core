using St.DoMain.Entity.Audited;

namespace St.DoMain.Entity
{
    /// <summary>
    /// 普通聚合审计(新增/修改)
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IAggregateAudited<TPrimaryKey> : ICreationAudited<TPrimaryKey>, IModificationAudited<TPrimaryKey>
        where TPrimaryKey : struct
    {

    }
}
