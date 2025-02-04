using domain.data;

namespace domain.queues;

public interface IPieceQueue
{
    void AddPiece();
    IPiece GetNextPiece();
    IPiece[] GetPiecePreviews(int amountOfPreviews);
}