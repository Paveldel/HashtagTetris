using domain.Input;
using domain.timer;

namespace domain;

public class Game
{
    public BoardRenderer Renderer { get; }
    
    private readonly Input.Input _input;
    private readonly GameLoop _gameLoop;

    public Game(BoardRenderer renderer, Input.Input input, GameLoop loop)
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