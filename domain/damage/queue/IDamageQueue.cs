namespace domain.damage.queue;

public interface IDamageQueue
{
    public void RegisterAsReceiver(IDamageReceiver receiver);
    public List<int> GetQueue();
    public void PiecePlaced(int amountOfDamageSent, bool lineCleared);
    public void AddGarbageToQueue(int amountOfGarbage);
}