namespace domain.timer;

public interface ITimer
{
    public long GetCurrentTime();
    public void RegisterUpdatable(params IUpdatable[] updatables);
}