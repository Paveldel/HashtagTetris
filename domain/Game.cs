using domain.damage;
using domain.timer;
using ITimer = domain.timer.ITimer;

namespace domain;

public class Game(BoardRenderer renderer, Input.Input input, ITimer loop, Board board) : IDamageReceiver
{
    public BoardRenderer Renderer { get; } = renderer;

    public void HandleKeyPress(string keyCode)
    {
        input.HandleKeyPress(keyCode);
    }

    public void HandleKeyRelease(string keyCode)
    {
        input.HandleKeyRelease(keyCode);
    }

    public void StartGame(IUpdatable screen)
    {
        loop.RegisterUpdatable(screen); 
        loop.StartLoop();
    }

    public void SetTarget(IDamageReceiver target)
    {
        board.SetTarget(target);
    }

    public void ReceiveDamage(int amountOfLines)
    {
        board.ReceiveDamage(amountOfLines);
    }
}