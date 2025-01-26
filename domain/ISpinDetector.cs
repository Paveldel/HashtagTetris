namespace domain;

public interface ISpinDetector
{
    public SpinType DetectSpin(Piece piece, Board board, int lastKick);
}