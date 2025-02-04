using domain.data;

namespace domain.rotationsystem;

public class NoKicks(Board board) : IRotationSystem
{
    public IPiece RotatePiece(IPiece piece, Rotation rotation)
    {
        IPiece rotatedPiece = piece.Rotate(rotation);
        if (!board.IntersectPiece(rotatedPiece)) return rotatedPiece;
        return piece;
    }

    public int GetLastUsedKickIndex()
    {
        return 0;
    }
}