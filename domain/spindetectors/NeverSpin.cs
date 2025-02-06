using domain.data;

namespace domain.spindetectors;

public class NeverSpin : ISpinDetector
{
    public SpinType DetectSpin(IPiece piece, int lastKick)
    {
        return SpinType.NoSpin;
    }
}