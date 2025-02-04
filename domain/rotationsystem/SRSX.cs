using domain.data;

namespace domain.rotationsystem;

public class SRSX : SRS
{
    
    private static readonly Kick[][] NormalReverseKicks = new[]
    {
        new[] { new Kick(0, 0), new Kick(1, 0), new Kick(2, 0), new Kick(1, 1), new Kick(2, 1), new Kick(-1, 0), new Kick(-2, 0), new Kick(-1, 1), new Kick(-2, 1), new Kick(0, -1), new Kick(3, 0), new Kick(-3, 0) },
        new[] { new Kick(0, 0), new Kick(0, 1), new Kick(0, 2), new Kick(-1, 1), new Kick(-1, 2), new Kick(0, -1), new Kick(0, -2), new Kick(-1, -1), new Kick(-1, -2), new Kick(1, 0), new Kick(0, 3), new Kick(0, -3) },
        new[] { new Kick(0, 0), new Kick(-1, 0), new Kick(-2, 0), new Kick(-1, -1), new Kick(-2, -1), new Kick(1, 0), new Kick(2, 0), new Kick(1, -1), new Kick(2, -1), new Kick(0, 1), new Kick(-3, 0), new Kick(3, 0) },
        new[] { new Kick(0, 0), new Kick(0, 1), new Kick(0, 2), new Kick(1, 1), new Kick(1, 2), new Kick(0, -1), new Kick(0, -2), new Kick(1, -1), new Kick(1, -2), new Kick(-1, 0), new Kick(0, 3), new Kick(0, -3) }
    };
    
    private static readonly Kick[][] IReverseKicks = new[]
    {
        new[] { new Kick(0, 0), new Kick(-1, 0), new Kick(-2, 0), new Kick(1, 0), new Kick(2, 0), new Kick(0, 1) },
        new[] { new Kick(0, 0), new Kick(0, 1), new Kick(0, 2), new Kick(0, -1), new Kick(0, -2), new Kick(-1, 0) },
        new[] { new Kick(0, 0), new Kick(1, 0), new Kick(2, 0), new Kick(-1, 0), new Kick(-2, 0), new Kick(0, -1) },
        new[] { new Kick(0, 0), new Kick(0, 1), new Kick(0, 2), new Kick(0, -1), new Kick(0, -2), new Kick(1, 0) },
    };
    
    protected override Piece RotateReverse(Piece piece, Board board)
    {
        Piece rotatedPiece = piece.Rotate(Rotation.Reverse);
        TryKicks(rotatedPiece, board, GetReverseKicks(piece));
        return KickIndex == FailedRotation ? piece : rotatedPiece;
    }

    private Kick[] GetReverseKicks(Piece piece)
    {
        if (piece.GetPieceIndex() == (int)PieceType.I) return IReverseKicks[piece.RotIndex];
        return NormalReverseKicks[piece.RotIndex];
    }
}