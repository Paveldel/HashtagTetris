using domain.data;

namespace domain.spindetectors;

public class SurgeSpinDetector : ISpinDetector
{
    
    private readonly ISpinDetector _TspinDetector = new FourCorner();
    private readonly ISpinDetector _otherspinDetector = new Immobile();
    
    public SpinType DetectSpin(Piece piece, Board board, int lastKick)
    {
        if (piece.GetPieceIndex() == (int)PieceType.T) return _TspinDetector.DetectSpin(piece, board, lastKick);
        return (_otherspinDetector.DetectSpin(piece, board, lastKick) == SpinType.FullSpin)
            ? SpinType.MiniSpin
            : SpinType.NoSpin;
    }
}