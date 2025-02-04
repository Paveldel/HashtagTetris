using domain.data;

namespace domain.rotationsystem;

public abstract class KickRotationSystem(Board board) : IRotationSystem
{
    protected const int FailedRotation = -1;
    
    protected int KickIndex = 0;
    
    public abstract IPiece RotatePiece(IPiece piece, Rotation rotation);

    public int GetLastUsedKickIndex()
    {
        return KickIndex;
    }

    protected void TryKicks(IPiece piece, Kick[] kicksToTry)
    {
        for (KickIndex = 0; KickIndex < kicksToTry.Length; KickIndex++)
        {
            Kick nextKick = kicksToTry[KickIndex];
            ApplyKick(piece, nextKick);
            if (!board.IntersectPiece(piece)) return;
            RemoveKick(piece, nextKick);
        }

        KickIndex = FailedRotation;
    }

    private void ApplyKick(IPiece piece, Kick kick)
    {
        piece.X += kick.X;
        piece.Y += kick.Y;
    }

    private void RemoveKick(IPiece piece, Kick kick)
    {
        piece.X -= kick.X;
        piece.Y -= kick.Y;
    }
}