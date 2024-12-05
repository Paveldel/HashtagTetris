namespace domain;

public class GameLoop(Input input)
{
    private const int TargetFps = 60;
    private const int Milliseconds = 1000;
    private bool _gameover = false;
    private Input _playerInput = input;
    public async Task StartLoop()
    {
        while (!_gameover)
        {
            await Task.Delay(Milliseconds / TargetFps);
            _playerInput.Update();
        }
    }
}