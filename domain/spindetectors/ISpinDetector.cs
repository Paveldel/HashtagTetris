using domain.data;

namespace domain.spindetectors;

public interface ISpinDetector
{
    public SpinType DetectSpin(Piece piece, Board board, int lastKick);
}