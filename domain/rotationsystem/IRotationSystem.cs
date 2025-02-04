using domain.data;

namespace domain.rotationsystem;

public interface IRotationSystem
{
    public Piece RotatePiece(Piece piece, Board board, Rotation rotation);
    public int GetLastUsedKickIndex();
}