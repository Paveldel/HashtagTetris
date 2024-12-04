namespace domain;

public interface IPieceQueue
{
    void AddPiece();
    Piece GetNextPiece();
    Piece[] GetPiecePreviews(int amountOfPreviews);
}