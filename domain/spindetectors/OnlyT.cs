using domain.data;

namespace domain.spindetectors;

public class OnlyT(Board board) : ISpinDetector
{
    private readonly ISpinDetector _spinDetector = new FourCorner(board);
    
    public SpinType DetectSpin(IPiece piece, int lastKick)
    {
        if (piece.GetPieceIndex() != (int)PieceType.T) return SpinType.NoSpin;
        return _spinDetector.DetectSpin(piece, lastKick);
    }
}