namespace domain.timer;

public class VersusTimer(MasterTimer master) : ITimer
{
    private readonly List<IUpdatable> _updatables = new ();

    private bool _gameEnded;
    
    public long GetCurrentTime()
    {
        return master.GetCurrentTime();
    }

    public void RegisterUpdatable(params IUpdatable[] updatables)
    {
        if (_gameEnded) return;
        foreach (var updatable in updatables)
        {
            updatable.SetTimer(this);
            _updatables.Add(updatable);
        }
    }

    public void EndTimer()
    {
        _gameEnded = true;
    }

    public void Update(long currentTime)
    {
        foreach (var updatable in _updatables)
        {
            updatable.Update(currentTime);
        }
    }
    
    public void StartLoop() { }
}