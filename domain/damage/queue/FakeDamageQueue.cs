namespace domain.damage.queue;

public class FakeDamageQueue : IDamageQueue
{
    public void RegisterAsReceiver(IDamageReceiver receiver) { }

    public List<int> GetQueue()
    {
        return [];
    }

    public void PiecePlaced(int amountOfDamageSent, bool lineCleared) { }

    public void AddGarbageToQueue(int amountOfGarbage) { }
}