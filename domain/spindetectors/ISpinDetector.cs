using domain.data;

namespace domain.spindetectors;

public interface ISpinDetector
{
    public SpinType DetectSpin(IPiece piece, int lastKick);
}