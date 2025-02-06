using domain.timer;
using ITimer = domain.timer.ITimer;

namespace domain;

public class Game
{
    public BoardRenderer Renderer { get; }
    
    private readonly Input.Input _input;
    private readonly ITimer _gameLoop;

    public Game(BoardRenderer renderer, Input.Input input, ITimer loop)
    {
        Renderer = renderer;
        _input = input;
        _gameLoop = loop;
    }

    public void HandleKeyPress(string keyCode)
    {
        _input.HandleKeyPress(keyCode);
    }

    public void HandleKeyRelease(string keyCode)
    {
        _input.HandleKeyRelease(keyCode);
    }

    public void StartGame(IUpdatable screen)
    {
        _gameLoop.RegisterUpdatable(screen);
        _ = _gameLoop.StartLoop();
    }
}