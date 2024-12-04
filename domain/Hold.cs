namespace domain;

public class Hold
{
    private int _heldPieceIndex = 0;
    private bool _holdEnabled = true;

    public Piece? HoldPiece(Piece p)
    {
        if (!_holdEnabled) return p;
        int temp = _heldPieceIndex;
        _heldPieceIndex = p.GetPieceIndex();
        _holdEnabled = false;
        return PieceData.Pieces[temp];
    }

    public bool IsEnabled()
    {
        return _holdEnabled;
    }

    public int GetHeldPieceType()
    {
        return _heldPieceIndex;
    }
}