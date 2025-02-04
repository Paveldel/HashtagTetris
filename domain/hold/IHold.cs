using domain.data;

namespace domain.hold;

public interface IHold
{
    public IPiece? HoldPiece(IPiece p);
    public bool IsEnabled();
    public void EnableHold();
    public int GetHeldPieceType();
}