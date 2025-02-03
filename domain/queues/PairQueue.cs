using domain.data;
using domain.piecedata;

namespace domain.queues;

public class PairQueue : AbstractQueue
{
    private const int AmountOfBatchesToStart = 10;
    private const int AmountOfPairsPerBatch = 4;
    
    private readonly Random _random = new();

    public PairQueue(IPieceData pieceData) : base(pieceData)
    {
        for (int i = 0; i < AmountOfBatchesToStart; i++)
        {
            AddPiece();
        }
    }

    public override void AddPiece()
    {
        if (Queue.Count > 100) return;
        List<Piece> pairs = GeneratePairs();
        while (pairs.Count != 0)
        {
            PutRandomPieceInQueue(pairs);
        }
    }

    private List<Piece> GeneratePairs()
    {
        List<Piece> pair = new();
        int firstPiece = _random.Next(1, PieceData.GetAmountOfPieces());
        int secondPiece = _random.Next(1, PieceData.GetAmountOfPieces() - 1);
        if (secondPiece >= firstPiece) secondPiece++;
        AddPieceToList(pair, firstPiece);
        AddPieceToList(pair, secondPiece);
        return pair;
    }

    private void AddPieceToList(List<Piece> list, int secondPiece)
    {
        for (int i = 0; i < AmountOfPairsPerBatch; i++)
        {
            list.Add(PieceData.GetPieceByIndex(secondPiece)!);
        }
    }

    private void PutRandomPieceInQueue(List<Piece> bag)
    {
        int randomIndex = _random.Next(0, bag.Count);
        Queue.Add(bag.ElementAt(randomIndex));
        bag.RemoveAt(randomIndex);
    }
}