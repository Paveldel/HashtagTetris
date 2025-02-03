using domain.data;

namespace domain.hold;

public interface IHold
{
    public Piece? HoldPiece(Piece p);
    public bool IsEnabled();
    public void EnableHold();
    public int GetHeldPieceType();
}