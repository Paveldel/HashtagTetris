using domain.data;
using domain.piecedata;

namespace domain.queues;

public class SevenBag : AbstractQueue
{
    private const int AmountOfStartingBags = 10;
    
    private readonly Random _random = new();

    public SevenBag(IPieceData pieceData) : base(pieceData)
    {
        for (int i = 0; i < AmountOfStartingBags; i++)
        {
            AddPiece();
        }
    }

    public override void AddPiece()
    {
        if (Queue.Count > 100) return;
        List<Piece> bag = GetPieceBag();
        while (bag.Count != 0)
        {
            PutRandomPieceInQueue(bag);
        }
    }

    private void PutRandomPieceInQueue(List<Piece> bag)
    {
        int randomIndex = _random.Next(0, bag.Count);
        Queue.Add(bag.ElementAt(randomIndex));
        bag.RemoveAt(randomIndex);
    }

    private List<Piece> GetPieceBag()
    {
        List<Piece> bag = new();
        for (int i = 0; i < PieceData.GetAmountOfPieces(); i++)
        {
            AddPieceToBag(bag, i);
        }
        return bag;
    }

    protected virtual void AddPieceToBag(List<Piece> bag, int i)
    {
        bag.Add(PieceData.GetPieceByIndex(i + 1)!);
    }
}