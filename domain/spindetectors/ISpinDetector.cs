using domain.data;

namespace domain.spindetectors;

public interface ISpinDetector
{
    public SpinType DetectSpin(IPiece piece, Board board, int lastKick);
}