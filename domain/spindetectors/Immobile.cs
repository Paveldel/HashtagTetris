using domain.data;

namespace domain.spindetectors;

public class Immobile : ISpinDetector
{
    public SpinType DetectSpin(Piece piece, Board board, int lastKick)
    {
        if (CanPieceMoveUp(piece, board)) return SpinType.NoSpin;
        if (CanPieceMoveLeft(piece, board)) return SpinType.NoSpin;
        return CanPieceMoveRight(piece, board) ? SpinType.NoSpin : SpinType.FullSpin;
    }

    private bool CanPieceMoveLeft(Piece piece, Board board)
    {
        piece.MoveLeft();
        bool result = !board.IntersectPiece(piece);
        piece.MoveRight();
        return result;
    }
    
    private bool CanPieceMoveRight(Piece piece, Board board)
    {
        piece.MoveRight();
        bool result = !board.IntersectPiece(piece);
        piece.MoveLeft();
        return result;
    }
    
    private bool CanPieceMoveUp(Piece piece, Board board)
    {
        piece.Y++;
        bool result = !board.IntersectPiece(piece);
        piece.Y--;
        return result;
    }
}