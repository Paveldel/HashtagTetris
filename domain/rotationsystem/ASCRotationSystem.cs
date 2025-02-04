using domain.data;

namespace domain.rotationsystem;

public class ASCRotationSystem: KickRotationSystem
{
    private static readonly Kick[] ClockWiseKicks =
    {
        new Kick(0, 0), new Kick(-1, 0), new Kick(0, -1), new Kick(-1, -1), new Kick(0, -2), new Kick(-1, -2), new Kick(-2, 0),
        new Kick(-2, -1), new Kick(-2, -2), new Kick(1, 0), new Kick(1, -1), new Kick(0, 1), new Kick(-1, 1), new Kick(-2, 1),
        new Kick(1, -2), new Kick(2, 0), new Kick(0, 2), new Kick(-1, 2), new Kick(-2, 2), new Kick(2, -1), new Kick(2, -2)
    };
    
    private static readonly Kick[] CounterClockWiseKicks =
    {
        new Kick(0, 0), new Kick(-1, 0), new Kick(0, -1), new Kick(-1, -1), new Kick(0, -2), new Kick(-1, -2), new Kick(-2, 0),
        new Kick(-2, -1), new Kick(-2, -2), new Kick(1, 0), new Kick(1, -1), new Kick(0, 1), new Kick(-1, 1), new Kick(-2, 1),
        new Kick(1, -2), new Kick(2, 0), new Kick(0, 2), new Kick(-1, 2), new Kick(-2, 2), new Kick(2, -1), new Kick(2, -2)
    };
    
    public override Piece RotatePiece(Piece piece, Board board, Rotation rotation)
    {
        if (rotation == Rotation.Reverse) return RotateReverse(piece, board);
        return RotateNormal(piece, board, rotation);
    }

    private Piece RotateNormal(Piece piece, Board board, Rotation rotation)
    {
        Piece rotatedPiece = piece.Rotate(rotation);
        TryKicks(rotatedPiece, board, GetKicks(rotation));
        if (KickIndex == FailedRotation) return piece;
        return rotatedPiece;
    }

    private Kick[] GetKicks(Rotation rotation)
    {
        if (rotation == Rotation.Clockwise) return ClockWiseKicks;
        return CounterClockWiseKicks;
    }

    private Piece RotateReverse(Piece piece, Board board)
    {
        Piece rotatedPiece = piece.Rotate(Rotation.Reverse);
        if (board.IntersectPiece(rotatedPiece)) return piece;
        return rotatedPiece;
    }
}