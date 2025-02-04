using domain.data;

namespace domain.rotationsystem;

public class SRSPlus(Board board) : SRS(board)
{
    
    private static readonly Kick[][] ReverseKicks = new[]
    {
        new[] { new Kick(0, 0), new Kick(0, 1), new Kick(1, 1), new Kick(-1, 1), new Kick(1, 0), new Kick(-1, 0) },
        new[] { new Kick(0, 0), new Kick(1, 0), new Kick(1, 2), new Kick(1, 1), new Kick(0, 2), new Kick(0, 1) },
        new[] { new Kick(0, 0), new Kick(0, -1), new Kick(-1, -1), new Kick(1, -1), new Kick(-1, 0), new Kick(1, 0) },
        new[] { new Kick(0, 0), new Kick(-1, 0), new Kick(-1, 2), new Kick(-1, 1), new Kick(0, 2), new Kick(0, 1) }
    };
    
    protected override Piece RotateReverse(Piece piece)
    {
        Piece rotatedPiece = piece.Rotate(Rotation.Reverse);
        TryKicks(rotatedPiece, ReverseKicks[piece.RotIndex]);
        return KickIndex == FailedRotation ? piece : rotatedPiece;
    }
}