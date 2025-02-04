using domain.data;

namespace domain.rotationsystem;

public class ARS : KickRotationSystem
{
    private static readonly Kick[] CollisionChecks =
    {
        new Kick(-1, 1), new Kick(0, 1), new Kick(1, 1),
        new Kick(-1, 0), new Kick(0, 0), new Kick(1, 0),
        new Kick(-1, -1), new Kick(0, -1), new Kick(1, -1)
    };

    private static readonly Kick[] Kicks = { new Kick(0, 0), new Kick(0, 1), new Kick(0, -1) };
    
    public override Piece RotatePiece(Piece piece, Board board, Rotation rotation)
    {
        if (!KickCondition(piece, board)) return piece;
        Piece rotatedPiece = piece.Rotate(Rotation.Reverse);
        TryKicks(rotatedPiece, board, Kicks);
        return KickIndex == FailedRotation ? piece : rotatedPiece;
    }

    private bool KickCondition(Piece piece, Board board)
    {
        if (piece.GetPieceIndex() == (int)PieceType.I) return false;
        if (piece.GetPieceIndex() == (int)PieceType.T
            || piece.GetPieceIndex() == (int)PieceType.L
            || piece.GetPieceIndex() == (int)PieceType.J) return CheckCenterColumn(piece, board);
        return true;
    }

    private bool CheckCenterColumn(Piece piece, Board board)
    {
        int firstCollision = GetFirstCollision(piece, board);
        return firstCollision != 2 && firstCollision != 5 && firstCollision != 8;
    }

    private int GetFirstCollision(Piece piece, Board board)
    {
        for (int i = 0; i < CollisionChecks.Length; i++)
        {
            Kick check = CollisionChecks[i];
            if (board.IntersectBlock(check.X + piece.X, check.Y + piece.Y)) return i;
        }

        return CollisionChecks.Length;
    }
}