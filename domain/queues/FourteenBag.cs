using domain.data;
using domain.piecedata;

namespace domain.queues;

public class FourteenBag(IPieceData pieceData, Random random) : SevenBag(pieceData, random)
{
    protected override void AddPieceToBag(List<IPiece> bag, int i)
    {
        base.AddPieceToBag(bag, i);
        base.AddPieceToBag(bag, i);
    }
}