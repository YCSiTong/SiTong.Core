namespace St.DoMain.Entity.AggregateRoot
{
    /// <summary>
    /// 核心聚合
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IAggregateRoot<out TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// 主键
        /// </summary>
        TPrimaryKey Id { get; }
    }
}
