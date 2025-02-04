using domain.data;

namespace domain.rotationsystem;

public class NoKicks(Board board) : IRotationSystem
{
    public Piece RotatePiece(Piece piece, Rotation rotation)
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