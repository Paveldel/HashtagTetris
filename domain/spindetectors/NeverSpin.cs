using domain.data;

namespace domain.spindetectors;

public class NeverSpin : ISpinDetector
{
    public SpinType DetectSpin(Piece piece, Board board, int lastKick)
    {
        return SpinType.NoSpin;
    }
}