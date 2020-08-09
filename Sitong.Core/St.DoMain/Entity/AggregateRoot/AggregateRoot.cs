namespace St.DoMain.Entity.AggregateRoot
{
    public class AggregateRoot<TPrimaryKey> : IAggregateRoot<TPrimaryKey>
        where TPrimaryKey : struct
    {
        public AggregateRoot()
        {
        }
        public AggregateRoot(TPrimaryKey id)
        {
            Id = id;
        }

        public TPrimaryKey Id { get; protected set; }
    }
}
