using St.DoMain.Entity;

namespace St.DoMain.Core.Entity
{
    public class AggregateRoot<TKey> : IAggregateRoot<TKey>
    {
        public TKey Id { get; protected set; }
    }
}
