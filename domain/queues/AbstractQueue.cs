using domain.data;
using domain.piecedata;

namespace domain.queues;

public abstract class AbstractQueue : IPieceQueue
{
    protected readonly IPieceData PieceData;
    protected readonly List<IPiece> Queue = new();

    protected AbstractQueue(IPieceData pieceData)
    {
        PieceData = pieceData;
    }

    public abstract void AddPiece();

    public IPiece GetNextPiece()
    {
        IPiece nextPiece = Queue.ElementAt(0);
        Queue.RemoveAt(0);
        return nextPiece;
    }

    public IPiece[] GetPiecePreviews(int amountOfPreviews)
    {
        IPiece[] previews = new IPiece[amountOfPreviews];
        for (int i = 0; i < amountOfPreviews; i++)
        {
            previews[i] = Queue.ElementAt(i);
        }
        return previews;
    }
}