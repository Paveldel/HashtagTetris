using domain.data;

namespace domain.spindetectors;

public class AlwaysSpin : ISpinDetector
{
    public SpinType DetectSpin(IPiece piece, Board board, int lastKick)
    {
        return SpinType.FullSpin;
    }
}