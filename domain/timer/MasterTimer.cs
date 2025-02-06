namespace domain.timer;

public class MasterTimer
{
    private const int TargetFps = 60;
    private const int Milliseconds = 1000;
    private bool _gamesOver;
    private readonly List<VersusTimer> _versusTimers = new ();
    
    public async void StartLoop()
    {
        while (!_gamesOver)
        {
            await Task.Delay(Milliseconds / TargetFps);
            Update();
        }
    }

    private void Update()
    {
        long currentTime = GetCurrentTime();
        foreach (var childTimer in _versusTimers)
        {
            childTimer.Update(currentTime);
        }
    }

    public long GetCurrentTime()
    {
        return DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    public VersusTimer GetChildTimer()
    {
        VersusTimer newTimer = new VersusTimer(this);
        _versusTimers.Add(newTimer);
        return newTimer;
    }

    public void PlayerDied(VersusTimer player)
    {
        _versusTimers.Remove(player);
    }
}