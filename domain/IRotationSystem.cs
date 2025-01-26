namespace domain;

public interface IRotationSystem
{
    public Piece RotatePiece(Piece piece, Board board, Rotation rotation);
    public int LastUsedRotationIndex();
}