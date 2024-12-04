namespace domain;

public class SevenBag : IPieceQueue
{
    private readonly List<Piece> _queue = new();
    private Random _random = new();
    
    public void AddPiece()
    {
        List<Piece> bag = getPieceBag();
        while (bag.Count != 0)
        {
            PutRandomPieceInQueue(bag);
        }
    }

    private void PutRandomPieceInQueue(List<Piece> bag)
    {
        int randomIndex = _random.Next(0, PieceData.AmountOfPieces);
        _queue.Add(bag.ElementAt(randomIndex));
        bag.RemoveAt(randomIndex);
    }

    private List<Piece> getPieceBag()
    {
        List<Piece> bag = new();
        for (int i = 0; i < PieceData.AmountOfPieces; i++)
        {
            bag.Add(PieceData.Pieces[i + 1]);
        }
        return bag;
    }

    public Piece GetNextPiece()
    {
        throw new NotImplementedException();
    }

    public Piece[] GetPiecePreviews(int amountOfPreviews)
    {
        throw new NotImplementedException();
    }
}