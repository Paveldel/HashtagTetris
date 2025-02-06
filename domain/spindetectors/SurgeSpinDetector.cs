using domain.data;

namespace domain.spindetectors;

public class SurgeSpinDetector(Board board) : ISpinDetector
{
    
    private readonly ISpinDetector _TspinDetector = new FourCorner(board);
    private readonly ISpinDetector _otherspinDetector = new Immobile(board);
    
    public SpinType DetectSpin(IPiece piece, int lastKick)
    {
        if (piece.GetPieceIndex() == (int)PieceType.T) return _TspinDetector.DetectSpin(piece, lastKick);
        return (_otherspinDetector.DetectSpin(piece, lastKick) == SpinType.FullSpin)
            ? SpinType.MiniSpin
            : SpinType.NoSpin;
    }
}