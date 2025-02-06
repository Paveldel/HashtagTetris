namespace domain.timer;

public class GameLoop() : ITimer
{
    private const int TargetFps = 60;
    private const int Milliseconds = 1000;
    private bool _gameover;
    private List<IUpdatable> _updatables = new ();
    
    public async void StartLoop()
    {
        while (!_gameover)
        {
            await Task.Delay(Milliseconds / TargetFps);
            Update();
        }
    }

    private void Update()
    {
        long currentTime = GetCurrentTime();
        foreach (var updatable in _updatables)
        {
            updatable.Update(currentTime);
        }
    }

    public long GetCurrentTime()
    {
        return DateTimeOffset.Now.ToUnixTimeMilliseconds();
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
        _gameover = true;
    }
}