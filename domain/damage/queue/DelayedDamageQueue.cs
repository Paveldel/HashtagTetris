using domain.timer;
using ITimer = domain.timer.ITimer;

namespace domain.damage.queue;

public class DelayedDamageQueue(Board board, long waitTime, bool allowCanceling = true, bool allowBlocking = true) : DamageQueue(board, allowCanceling, allowBlocking), IUpdatable
{
    
    private readonly List<long> _delays = [];
    private ITimer _timer;

    public override void AddGarbageToQueue(int amountOfGarbage)
    {
        base.AddGarbageToQueue(amountOfGarbage);
        _delays.Add(_timer.GetCurrentTime() + waitTime);
    }

    protected override bool CanTakeDamage()
    {
        return base.CanTakeDamage() && _timer.GetCurrentTime() >= _delays[0];
    }

    protected override void TakeLineFromQueue()
    {
        base.TakeLineFromQueue();
        if (_queue.Count < _delays.Count) _delays.RemoveAt(0);
    }

    public void Update(long currentTime) { }

    public void SetTimer(ITimer timer)
    {
        _timer = timer;
    }
}