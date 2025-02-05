namespace domain.damage.queue;

public abstract class AbstractGarbageQueue : IDamageQueue
{
    private const int DefaultGarbageCap = 8;
    
    private readonly Board _board;
    private readonly bool _allowBlocking;
    private readonly bool _allowCanceling;

    private IDamageReceiver _receiver = new MockReceiver();
    private int _garbageCap = DefaultGarbageCap;
    
    private readonly List<int> _queue = [];
    protected readonly Random Random = new();
    private int _well;

    protected AbstractGarbageQueue(Board board, bool allowCanceling = true, bool allowBlocking = true)
    {
        _board = board;
        _allowCanceling = allowCanceling;
        _allowBlocking = allowBlocking;
        RandomiseWell();
    }

    public void SetGarbageCap(int cap)
    {
        _garbageCap = cap;
    }

    public void RegisterAsReceiver(IDamageReceiver receiver)
    {
        _receiver = receiver;
    }

    public List<int> GetQueue()
    {
        return [.._queue];
    }

    public virtual void AddGarbageToQueue(int amountOfGarbage)
    {
        if (amountOfGarbage < 1) return;
        _queue.Add(amountOfGarbage);
    }

    public void PiecePlaced(int amountOfDamageSent, bool lineCleared)
    {
        amountOfDamageSent = CancelGarbage(amountOfDamageSent);
        SendLines(amountOfDamageSent);
        TakeLines(lineCleared);
    }

    private void TakeLines(bool lineCleared)
    {
        if (_allowBlocking && lineCleared) return;
        int amountOfLinesTaken = 0;
        while (CanTakeDamage() && amountOfLinesTaken < _garbageCap)
        {
            amountOfLinesTaken++;
            PutLineOnBoard();
        }
    }

    protected virtual bool CanTakeDamage()
    {
        return _queue.Any();
    }

    private void PutLineOnBoard()
    {
        _board.TakeGarbageLine(_well);
        TakeLineFromQueue();
        MessinessInBatch();
    }

    private int CancelGarbage(int amountOfDamageSent)
    {
        if (!_allowCanceling || amountOfDamageSent == 0) return amountOfDamageSent;
        while (_queue.Any() && amountOfDamageSent > 0)
        {
            amountOfDamageSent--;
            TakeLineFromQueue();
        }
        return amountOfDamageSent;
    }

    private void SendLines(int amountOfLines)
    {
        if (amountOfLines > 0);
        _receiver.ReceiveDamage(amountOfLines);
    }

    protected virtual void TakeLineFromQueue()
    {
        _queue[0]--;
        if (_queue[0] != 0) return;
        _queue.RemoveAt(0);
        MessinessOnChange();
    }

    protected abstract void MessinessOnChange();

    protected abstract void MessinessInBatch();
    
    protected void RandomiseWell()
    {
        _well = Random.Next(_board.GetWidth());
    }
}