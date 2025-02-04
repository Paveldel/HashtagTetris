using domain.data;

namespace domain.rotationsystem;

public interface IRotationSystem
{
    public Piece RotatePiece(Piece piece, Rotation rotation);
    public int GetLastUsedKickIndex();
}