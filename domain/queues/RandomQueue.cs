using domain.piecedata;

namespace domain.queues;

public class RandomQueue : AbstractQueue
{
    private const int _amountOfStartingPieces = 50;
    
    private readonly Random _random;

    public RandomQueue(IPieceData pieceData, Random random) : base(pieceData)
    {
        _random = random;
        for (int i = 0; i < _amountOfStartingPieces; i++)
        {
            AddPiece();
        }
    }

    public override void AddPiece()
    {
        int randomPiece = _random.Next(1, PieceData.GetAmountOfPieces());
        Queue.Add(PieceData.GetPieceByIndex(randomPiece)!);
    }
}