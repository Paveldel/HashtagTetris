using domain.piecedata;

namespace domain.queues;

public class ClassicQueue : AbstractQueue
{
    private const int AmountOfStartingPieces = 50;
    private int _lastGeneratedPiece = 0;
    
    private readonly Random _random;
    
    public ClassicQueue(IPieceData pieceData, Random random) : base(pieceData)
    {
        _random = random;
        for (int i = 0; i < AmountOfStartingPieces; i++)
        {
            AddPiece();
        }
    }

    public override void AddPiece()
    {
        int randomPiece = _random.Next(0, PieceData.GetAmountOfPieces());
        if (randomPiece == 0 || randomPiece == _lastGeneratedPiece) randomPiece = SecondPass();
        Queue.Add(PieceData.GetPieceByIndex(randomPiece)!);
        _lastGeneratedPiece = randomPiece;
    }

    private int SecondPass()
    {
        return _random.Next(1, PieceData.GetAmountOfPieces());
    }
}