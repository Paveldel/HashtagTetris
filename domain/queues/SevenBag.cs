using domain.data;
using domain.piecedata;

namespace domain.queues;

public class SevenBag : IPieceQueue
{
    private const int AmountOfStarting = 10;

    private readonly IPieceData _pieceData;
    
    private readonly List<Piece> _queue = new();
    private readonly Random _random = new();

    public SevenBag(IPieceData pieceData)
    {
        _pieceData = pieceData;
        for (int i = 0; i < AmountOfStarting; i++)
        {
            AddPiece();
        }
    }

    public void AddPiece()
    {
        if (_queue.Count > 100) return;
        List<Piece> bag = GetPieceBag();
        while (bag.Count != 0)
        {
            PutRandomPieceInQueue(bag);
        }
    }

    private void PutRandomPieceInQueue(List<Piece> bag)
    {
        int randomIndex = _random.Next(0, bag.Count);
        _queue.Add(bag.ElementAt(randomIndex));
        bag.RemoveAt(randomIndex);
    }

    private List<Piece> GetPieceBag()
    {
        List<Piece> bag = new();
        for (int i = 0; i < _pieceData.GetAmountOfPieces(); i++)
        {
            bag.Add(_pieceData.GetPieceByIndex(i + 1));
        }
        return bag;
    }

    public Piece GetNextPiece()
    {
        Piece nextPiece = _queue.ElementAt(0);
        _queue.RemoveAt(0);
        return nextPiece;
    }

    public Piece[] GetPiecePreviews(int amountOfPreviews)
    {
        Piece[] previews = new Piece[amountOfPreviews];
        for (int i = 0; i < amountOfPreviews; i++)
        {
            previews[i] = _queue.ElementAt(i);
        }
        return previews;
    }
}