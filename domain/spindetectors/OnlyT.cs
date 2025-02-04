using domain.data;

namespace domain.spindetectors;

public class OnlyT : ISpinDetector
{
    private readonly ISpinDetector _spinDetector = new FourCorner();
    
    public SpinType DetectSpin(IPiece piece, Board board, int lastKick)
    {
        if (piece.GetPieceIndex() != (int)PieceType.T) return SpinType.NoSpin;
        return _spinDetector.DetectSpin(piece, board, lastKick);
    }
}