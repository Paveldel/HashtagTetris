using domain.data;

namespace domain.spindetectors;

public class Immobile(Board board) : ISpinDetector
{
    public SpinType DetectSpin(IPiece piece, int lastKick)
    {
        if (CanPieceMoveUp(piece)) return SpinType.NoSpin;
        if (CanPieceMoveLeft(piece)) return SpinType.NoSpin;
        return CanPieceMoveRight(piece) ? SpinType.NoSpin : SpinType.FullSpin;
    }

    private bool CanPieceMoveLeft(IPiece piece)
    {
        piece.MoveLeft();
        bool result = !board.IntersectPiece(piece);
        piece.MoveRight();
        return result;
    }
    
    private bool CanPieceMoveRight(IPiece piece)
    {
        piece.MoveRight();
        bool result = !board.IntersectPiece(piece);
        piece.MoveLeft();
        return result;
    }
    
    private bool CanPieceMoveUp(IPiece piece)
    {
        piece.Y++;
        bool result = !board.IntersectPiece(piece);
        piece.Y--;
        return result;
    }
}