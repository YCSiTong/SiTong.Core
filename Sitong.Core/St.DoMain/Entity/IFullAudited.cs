using St.DoMain.Entity.Audited;

namespace St.DoMain.Entity
{
    /// <summary>
    /// 全部聚合审计(新增/修改/删除)
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IFullAudited<TPrimaryKey> : ICreationAudited<TPrimaryKey>, IModificationAudited<TPrimaryKey>, ISoftDelete, IAggregateRoot<TPrimaryKey>
        where TPrimaryKey : struct
    {

    }
}
