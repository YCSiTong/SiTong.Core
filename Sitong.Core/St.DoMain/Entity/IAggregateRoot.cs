namespace St.DoMain.Entity
{
    public interface IAggregateRoot<out TKey>
    {
        TKey Id { get; }
    }
}
