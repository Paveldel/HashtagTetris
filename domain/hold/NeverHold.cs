using domain.data;

namespace domain.hold;

public class NeverHold : IHold
{
    public Piece? HoldPiece(Piece p)
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