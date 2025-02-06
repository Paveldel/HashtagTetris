using domain.data;

namespace domain.spindetectors;

public class AlwaysSpin : ISpinDetector
{
    public SpinType DetectSpin(IPiece piece, int lastKick)
    {
        return SpinType.FullSpin;
    }
}