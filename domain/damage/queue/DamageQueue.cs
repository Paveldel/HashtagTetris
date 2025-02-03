namespace domain.damage.queue;

public class DamageQueue : IDamageQueue
{
    private const int DefaultGarbageCap = 8;
    private const double DefaultMessinessOnChange = 1;
    private const double DefaultMessinessInBatch = 0;
    
    private readonly Board _board;
    private readonly bool _allowBlocking;
    private readonly bool _allowCanceling;

    private IDamageReceiver _receiver = new MockReceiver();
    private int _garbageCap = DefaultGarbageCap;
    private double _messinessOnChange = DefaultMessinessOnChange;
    private double _messinessInBatch = DefaultMessinessInBatch;
    
    private readonly List<int> _queue = [];
    private readonly Random _random = new();
    private int _well;

    public DamageQueue(Board board, bool allowCanceling = true, bool allowBlocking = true)
    {
        _board = board;
        _allowCanceling = allowCanceling;
        _allowBlocking = allowBlocking;
        RandomiseWell();
    }

    public void SetMessinessOnChange(double messiness)
    {
        _messinessOnChange = messiness;
    }
    
    public void SetMessinessInBatch(double messiness)
    {
        _messinessInBatch = messiness;
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
    
    public void AddGarbageToQueue(int amountOfGarbage)
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
        while (_queue.Any() && amountOfLinesTaken < _garbageCap)
        {
            amountOfLinesTaken++;
            PutLineOnBoard();
        }
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

    private void TakeLineFromQueue()
    {
        _queue[0]--;
        if (_queue[0] != 0) return;
        _queue.RemoveAt(0);
        MessinessOnChange();
    }

    private void MessinessOnChange()
    {
        if (_messinessOnChange >= _random.NextDouble()) RandomiseWell();
    }
    
    private void MessinessInBatch()
    {
        if (_messinessInBatch >= _random.NextDouble())  RandomiseWell();
    }
    
    private void RandomiseWell()
    {
        _well = _random.Next(_board.GetWidth());
    }
}