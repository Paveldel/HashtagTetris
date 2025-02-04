using domain.data;

namespace domain.rotationsystem;

public class NoKicks : IRotationSystem
{
    public Piece RotatePiece(Piece piece, Board board, Rotation rotation)
    {
        Piece rotatedPiece = piece.Rotate(rotation);
        if (!board.IntersectPiece(rotatedPiece)) return rotatedPiece;
        return piece;
    }

    public int GetLastUsedKickIndex()
    {
        return 0;
    }
}