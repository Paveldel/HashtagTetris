namespace domain;

public class ActivePiece
{
    private IPieceQueue _queue;
    private Hold _hold;
    
    private IPieceData _pieceData;
    private Piece _currentPiece;
    private Board _board;
    private Gravity _gravity;

    public ActivePiece(Board board, Gravity gravity, IPieceData pieceData)
    {
        _board = board;
        _gravity = gravity;
        _pieceData = pieceData;
        _hold = new Hold(pieceData);
        _queue = new SevenBag(pieceData);
        NextPiece();
    }

    public Piece GetPiece()
    {
        return _currentPiece;
    }
    
    public Piece[] GetNextPieces(int amountOfNextPieces)
    {
        return _queue.GetPiecePreviews(amountOfNextPieces);
    }

    private void NextPiece()
    {
        _currentPiece = _queue.GetNextPiece();
        _currentPiece.X = _board.getXCenter();
        _currentPiece.Y = _board.getTop() + 1;
    }

    public int GetHeldType()
    {
        return _hold.GetHeldPieceType();
    }

    public bool isHoldEnabled()
    {
        return _hold.IsEnabled();
    }

    public bool MoveDown()
    {
        Piece movedPiece = _currentPiece.MoveDown();
        return ReplaceIfValid(movedPiece);
    }
    
    public bool MoveLeft()
    {
        Piece movedPiece = _currentPiece.MoveLeft();
        return ReplaceIfValid(movedPiece);
    }
    
    public bool MoveRight()
    {
        Piece movedPiece = _currentPiece.MoveRight();
        return ReplaceIfValid(movedPiece);
    }

    private bool ReplaceIfValid(Piece movedPiece)
    {
        if (!_board.IntersectPiece(movedPiece))
        {
            _currentPiece = movedPiece;
            _gravity.PieceMoved(IsPieceOnGround());
            return true;
        }
        
        return false;
    }

    private bool IsPieceOnGround()
    {
        return _board.IntersectPiece(_currentPiece.MoveDown());
    }

    public bool Rotate(Rotation rotation)
    {
        Piece rotatedPiece = _currentPiece.Rotate(rotation);
        return ReplaceIfValid(rotatedPiece);
    }

    public Piece DeepDrop()
    {
        Piece nextStep = _currentPiece.Clone();
        while (!_board.IntersectPiece(nextStep.MoveDown()))
        {
            nextStep = nextStep.MoveDown();
        }
        return nextStep;
    }

    public void HardDrop()
    {
        _currentPiece = DeepDrop();
        LockPiece();
    }

    public void LockPiece()
    {
        _board.Lock(_currentPiece);
        _queue.AddPiece();
        NextPiece();
        _hold.EnableHold();
        _gravity.Reset();
    }

    public void Hold()
    {
        Piece? result = _hold.HoldPiece(_currentPiece);
        if (result != _currentPiece) _gravity.Reset();
        if (result == null) NextPiece();
        else _currentPiece = result;
    }
}