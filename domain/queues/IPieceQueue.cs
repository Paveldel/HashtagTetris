using domain.data;

namespace domain.queues;

public interface IPieceQueue
{
    void AddPiece();
    Piece GetNextPiece();
    Piece[] GetPiecePreviews(int amountOfPreviews);
}