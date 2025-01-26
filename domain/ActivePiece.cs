namespace domain;

public class ActivePiece
{
    private bool _spin = false;
    
    private IPieceQueue _queue;
    private Hold _hold;
    private IRotationSystem _rotationSystem;
    private IPieceData _pieceData;
    private Piece _currentPiece;
    private Board _board;
    private Gravity _gravity;
    private ISpinDetector _spinDetector;

    public ActivePiece(Board board, Gravity gravity, IPieceData pieceData)
    {
        _board = board;
        _gravity = gravity;
        _pieceData = pieceData;
        _hold = new Hold(pieceData);
        _queue = new SevenBag(pieceData);
        _rotationSystem = new SRS();
        _spinDetector = new FourCorner();
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
        return TryToMove(movedPiece);
    }
    
    public bool MoveLeft()
    {
        Piece movedPiece = _currentPiece.MoveLeft();
        return TryToMove(movedPiece);
    }
    
    public bool MoveRight()
    {
        Piece movedPiece = _currentPiece.MoveRight();
        return TryToMove(movedPiece);
    }

    private bool TryToMove(Piece movedPiece)
    {
        if (!_board.IntersectPiece(movedPiece))
        {
            _spin = false;
            ReplacePiece(movedPiece);
            return true;
        }
        
        return false;
    }

    private void ReplacePiece(Piece movedPiece)
    {
        _currentPiece = movedPiece;
        _gravity.PieceMoved(IsPieceOnGround());
    }

    private bool IsPieceOnGround()
    {
        return _board.IntersectPiece(_currentPiece.MoveDown());
    }

    public bool Rotate(Rotation rotation)
    {
        Piece rotatedPiece = _rotationSystem.RotatePiece(_currentPiece, _board, rotation);
        if (Equals(rotatedPiece, _currentPiece)) return false;
        ReplacePiece(rotatedPiece);
        _spin = true;
        return true;
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
        var spinType = GetSpinType();
        _board.Lock(_currentPiece, spinType);
        _queue.AddPiece();
        NextPiece();
        _hold.EnableHold();
        _gravity.Reset();
    }

    private SpinType GetSpinType()
    {
        if (!_spin) return SpinType.NoSpin;
        int lastKick = _rotationSystem.LastUsedRotationIndex();
        return _spinDetector.DetectSpin(_currentPiece, _board, lastKick);
    }

    public void Hold()
    {
        Piece? result = _hold.HoldPiece(_currentPiece);
        if (!Equals(result, _currentPiece)) _gravity.Reset();
        if (result == null) NextPiece();
        else _currentPiece = result;
    }
}