namespace domain;

public class GameLoop
{
    private const int TargetFPS = 60;
    private const int Miliseconds = 1000;
    private bool _gameover = false;
    public async Task StartLoop()
    {
        while (!_gameover)
        {
            Task.Delay(Miliseconds / TargetFPS);
        }
    }
}