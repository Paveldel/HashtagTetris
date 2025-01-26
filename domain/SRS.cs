namespace domain;

public class SRS : IRotationSystem
{
    private const int AmountOfKicks = 5;
    
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
    
    private int _rotationIndex = 0;
    
    public Piece RotatePiece(Piece piece, Board board, Rotation rotation)
    {
        if (rotation == Rotation.REVERSE) return RotateReverse(piece, board);
        else return RotateNormal(piece, board, rotation);
    }

    private Piece RotateNormal(Piece piece, Board board, Rotation rotation)
    {
        int kickIndex = CalculateKickIndex(piece.RotIndex, rotation);
        TryKicks(piece, board, kickIndex);
        return piece;
    }

    private void TryKicks(Piece piece, Board board, int kickIndex)
    {
        for (_rotationIndex = 0; _rotationIndex < AmountOfKicks; _rotationIndex++)
        {
            Kick nextKick = GetKick(kickIndex, _rotationIndex, piece.GetPieceIndex());
            ApplyKick(piece, nextKick);
            if (!board.IntersectPiece(piece)) return;
            RemoveKick(piece, nextKick);
        }

        _rotationIndex = 0;
    }

    private void ApplyKick(Piece piece, Kick kick)
    {
        piece.X += kick.X;
        piece.Y += kick.Y;
    }
    
    private void RemoveKick(Piece piece, Kick kick)
    {
        piece.X -= kick.X;
        piece.Y -= kick.Y;
    }

    private Kick GetKick(int kickIndex, int kick, int pieceType)
    {
        if (pieceType == (int)PieceType.I) return IKicks[kickIndex][kick];
        return NormalKicks[kickIndex][kick];
    }

    private Piece RotateReverse(Piece piece, Board board)
    {
        Piece rotatedPiece = piece.Rotate(Rotation.REVERSE);
        if (board.IntersectPiece(rotatedPiece)) return piece;
        return rotatedPiece;
    }

    private int CalculateKickIndex(int rotIndex, Rotation rotation)
    {
        int kickIndex = 2 * rotIndex;
        if (rotation == Rotation.ANTI_CLOCKWISE) kickIndex--;
        if (kickIndex < 0) kickIndex += 8;
        return kickIndex;
    }

    public int LastUsedRotationIndex()
    {
        return _rotationIndex;
    }
}