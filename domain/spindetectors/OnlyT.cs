using domain.data;

namespace domain.spindetectors;

public class OnlyT : ISpinDetector
{
    private ISpinDetector _spinDetector = new FourCorner();
    
    public SpinType DetectSpin(Piece piece, Board board, int lastKick)
    {
        if (piece.GetPieceIndex() != (int)PieceType.T) return SpinType.NoSpin;
        return _spinDetector.DetectSpin(piece, board, lastKick);
    }
}