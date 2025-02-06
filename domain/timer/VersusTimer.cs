namespace domain.timer;

public class VersusTimer(MasterTimer master) : ITimer
{
    private readonly List<IUpdatable> _updatables = new (); 
    
    public long GetCurrentTime()
    {
        return master.GetCurrentTime();
    }

    public void RegisterUpdatable(params IUpdatable[] updatables)
    {
        foreach (var updatable in updatables)
        {
            updatable.SetTimer(this);
            _updatables.Add(updatable);
        }
    }

    public void EndTimer()
    {
        master.PlayerDied(this);
    }

    public void Update(long currentTime)
    {
        foreach (var updatable in _updatables)
        {
            updatable.Update(currentTime);
        }
    }
    
    public Task StartLoop()
    {
        throw new NotImplementedException();
    }
}