using domain.data;

namespace domain.rotationsystem;

public class SRS(Board board) : KickRotationSystem(board)
{
    private static readonly Kick[][] NormalKicks = new[]
    {
        new[] { new Kick(0, 0), new Kick(-1, 0), new Kick(-1, 1), new Kick(0, -2), new Kick(-1, -2) },
        new[] { new Kick(0, 0), new Kick(1, 0), new Kick(1, -1), new Kick(0, 2), new Kick(1, 2) },
        new[] { new Kick(0, 0), new Kick(1, 0), new Kick(1, -1), new Kick(0, 2), new Kick(1, 2) },
        new[] { new Kick(0, 0), new Kick(-1, 0), new Kick(-1, 1), new Kick(0, -2), new Kick(-1, -2) },
        new[] { new Kick(0, 0), new Kick(1, 0), new Kick(1, 1), new Kick(0, -2), new Kick(1, -2) },
        new[] { new Kick(0, 0), new Kick(-1, 0), new Kick(-1, -1), new Kick(0, 2), new Kick(-1, 2) },
        new[] { new Kick(0, 0), new Kick(-1, 0), new Kick(-1, -1), new Kick(0, 2), new Kick(-1, 2) },
        new[] { new Kick(0, 0), new Kick(1, 0), new Kick(1, 1), new Kick(0, -2), new Kick(1, -2) },
    };
    
    private static readonly Kick[][] IKicks = new[]
    {
        new[] { new Kick(0, 0), new Kick(-2, 0), new Kick(1, 0), new Kick(-2, -1), new Kick(1, 2) },
        new[] { new Kick(0, 0), new Kick(2, 0), new Kick(-1, 0), new Kick(2, 1), new Kick(-1, -2) },
        new[] { new Kick(0, 0), new Kick(2, 0), new Kick(-1, 0), new Kick(2, 1), new Kick(-1, -2) },
        new[] { new Kick(0, 0), new Kick(-2, 0), new Kick(1, 0), new Kick(-2, -1), new Kick(1, 2) },
        new[] { new Kick(0, 0), new Kick(2, 0), new Kick(-1, 0), new Kick(2, -1), new Kick(-1, 2) },
        new[] { new Kick(0, 0), new Kick(-2, 0), new Kick(1, 0), new Kick(-2, 1), new Kick(1, -2) },
        new[] { new Kick(0, 0), new Kick(-2, 0), new Kick(1, 0), new Kick(-2, 1), new Kick(1, -2) },
        new[] { new Kick(0, 0), new Kick(2, 0), new Kick(-1, 0), new Kick(2, -1), new Kick(-1, 2) },
    };
    
    public override Piece RotatePiece(Piece piece, Rotation rotation)
    {
        if (rotation == Rotation.Reverse) return RotateReverse(piece);
        return RotateNormal(piece, rotation);
    }

    private Piece RotateNormal(Piece piece, Rotation rotation)
    {
        Piece rotatedPiece = piece.Rotate(rotation);
        int kickIndex = CalculateKickIndex(piece.RotIndex, rotation);
        TryKicks(rotatedPiece, GetKicks(kickIndex, piece.GetPieceIndex()));
        if (KickIndex == FailedRotation) return piece;
        return rotatedPiece;
    }

    private Kick[] GetKicks(int kickIndex, int pieceType)
    {
        if (pieceType == (int)PieceType.I) return IKicks[kickIndex];
        return NormalKicks[kickIndex];
    }

    protected virtual Piece RotateReverse(Piece piece)
    {
        Piece rotatedPiece = piece.Rotate(Rotation.Reverse);
        if (board.IntersectPiece(rotatedPiece)) return piece;
        return rotatedPiece;
    }

    private int CalculateKickIndex(int rotIndex, Rotation rotation)
    {
        int kickIndex = 2 * rotIndex;
        if (rotation == Rotation.AntiClockwise) kickIndex--;
        if (kickIndex < 0) kickIndex += 8;
        return kickIndex;
    }
}