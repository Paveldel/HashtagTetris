namespace domain;

public class ActivePiece
{
    private readonly IPieceQueue _queue;
    private readonly Hold _hold;
    private readonly IRotationSystem _rotationSystem;
    private readonly Board _board;
    private readonly Gravity _gravity;
    private readonly ISpinDetector _spinDetector;
    
    private bool _spin;
    private Piece _currentPiece;
    private bool _isGameOver = false;

    public ActivePiece(Board board, Gravity gravity, IPieceData pieceData)
    {
        _board = board;
        _gravity = gravity;
        _gravity.SetActivePiece(this);
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
        SetCurrentPiece(_queue.GetNextPiece());
    }

    public int GetHeldType()
    {
        return _hold.GetHeldPieceType();
    }

    public bool IsHoldEnabled()
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
        if (_isGameOver) return;
        var spinType = GetSpinType();
        _board.Lock(_currentPiece, spinType);
        _queue.AddPiece();
        NextPiece();
        _hold.EnableHold();
    }

    private SpinType GetSpinType()
    {
        if (!_spin) return SpinType.NoSpin;
        int lastKick = _rotationSystem.LastUsedRotationIndex();
        return _spinDetector.DetectSpin(_currentPiece, _board, lastKick);
    }

    public void Hold()
    {
        if (_isGameOver) return;
        Piece? result = _hold.HoldPiece(_currentPiece);
        if (result == null) NextPiece();
        else SetCurrentPiece(result);
    }

    private void SetCurrentPiece(Piece piece)
    {
        _currentPiece = piece;
        _currentPiece.X = _board.GetXCenter();
        _currentPiece.Y = _board.GetTop() + 1;
        _gravity.Reset(IsPieceOnGround());
        if (_board.IntersectPiece(_currentPiece)) GameOver();
    }

    private void GameOver()
    {
        _isGameOver = true;
        _currentPiece = new Piece([], 0, 0, 0);
        _board.GrayOutBoard();
    }

    public bool HasPlayerLost()
    {
        return _isGameOver;
    }
}