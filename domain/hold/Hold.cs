using domain.data;
using domain.piecedata;

namespace domain.hold;

public class Hold(IPieceData pieceData) : IHold
{
    private int _heldPieceIndex;
    private bool _holdEnabled = true;

    public IPiece? HoldPiece(IPiece p)
    {
        if (!_holdEnabled) return p;
        int temp = _heldPieceIndex;
        _heldPieceIndex = p.GetPieceIndex();
        _holdEnabled = false;
        return pieceData.GetPieceByIndex(temp);
    }

    public bool IsEnabled()
    {
        return _holdEnabled;
    }

    public int GetHeldPieceType()
    {
        return _heldPieceIndex;
    }

    public void EnableHold()
    {
        _holdEnabled = true;
    }
}