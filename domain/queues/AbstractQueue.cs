using domain.data;
using domain.piecedata;

namespace domain.queues;

public abstract class AbstractQueue : IPieceQueue
{
    protected readonly IPieceData PieceData;
    protected readonly List<Piece> Queue = new();

    protected AbstractQueue(IPieceData pieceData)
    {
        PieceData = pieceData;
    }

    public abstract void AddPiece();

    public Piece GetNextPiece()
    {
        Piece nextPiece = Queue.ElementAt(0);
        Queue.RemoveAt(0);
        return nextPiece;
    }

    public Piece[] GetPiecePreviews(int amountOfPreviews)
    {
        Piece[] previews = new Piece[amountOfPreviews];
        for (int i = 0; i < amountOfPreviews; i++)
        {
            previews[i] = Queue.ElementAt(i);
        }
        return previews;
    }
}