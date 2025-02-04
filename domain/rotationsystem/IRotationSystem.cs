using domain.data;

namespace domain.rotationsystem;

public interface IRotationSystem
{
    public IPiece RotatePiece(IPiece piece, Rotation rotation);
    public int GetLastUsedKickIndex();
}