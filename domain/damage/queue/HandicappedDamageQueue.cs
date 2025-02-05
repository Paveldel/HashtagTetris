namespace domain.damage.queue;

public class HandicappedDamageQueue(IDamageQueue original, double multiplier) : IDamageQueue
{
    private double _leftOvers;
    
    public void RegisterAsReceiver(IDamageReceiver receiver)
    {
        original.RegisterAsReceiver(receiver);
    }

    public List<int> GetQueue()
    {
        return original.GetQueue();
    }

    public void PiecePlaced(int amountOfDamageSent, bool lineCleared)
    {
        original.PiecePlaced(amountOfDamageSent, lineCleared);
    }

    public void AddGarbageToQueue(int amountOfGarbage)
    {
        double multipliedDamage = amountOfGarbage * multiplier;
        multipliedDamage += _leftOvers;
        _leftOvers = multipliedDamage % 1;
        original.AddGarbageToQueue((int)Math.Floor(multipliedDamage));
    }
}