using domain.data;
using domain.piecedata;

namespace domain.hold;

public class AlwaysHold(IPieceData pieceData) : IHold
{
    private int _heldPieceIndex;

    public IPiece? HoldPiece(IPiece p)
    {
        int temp = _heldPieceIndex;
        _heldPieceIndex = p.GetPieceIndex();
        return pieceData.GetPieceByIndex(temp);
    }

    public bool IsEnabled()
    {
        return true;
    }

    public int GetHeldPieceType()
    {
        return _heldPieceIndex;
    }

    public void EnableHold() { }
}