namespace St.DoMain.Entity
{
    /// <summary>
    /// 核心聚合
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IAggregateRoot<TPrimaryKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TPrimaryKey Id { get; protected set; }
    }
}
