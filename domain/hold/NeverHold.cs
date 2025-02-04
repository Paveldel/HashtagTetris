using domain.data;

namespace domain.hold;

public class NeverHold : IHold
{
    public IPiece? HoldPiece(IPiece p)
    {
        return p;
    }

    public bool IsEnabled()
    {
        return false;
    }

    public void EnableHold() { }
    public int GetHeldPieceType()
    {
        return 0;
    }
}