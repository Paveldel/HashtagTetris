using domain.data;

namespace domain.spindetectors;

public class FourCorner(Board board) : ISpinDetector
{
    private const int ImportantCorners = 2;
    
    private readonly Block[][] _corners = new[]
    {
        null!,
        new []{new Block(1, 1, true), new Block(0, -1, true), new Block(-2, 0, true), new Block(3, 0, true)},
        new []{new Block(1, 1, false), new Block(-1, 1, false), new Block(1, -1, false), new Block(-1, -1, false)},
        [],
        new []{new Block(0, 1, false), new Block(-1, 1, false), new Block(1, -1, false), new Block(-1, -1, false)},
        new []{new Block(0, 1, false), new Block(1, 1, false), new Block(1, -1, false), new Block(-1, -1, false)},
        new []{new Block(-1, 1, false), new Block(1, 0, false), new Block(-2, 0, false), new Block(2, 1, false)},
        new []{new Block(1, 1, false), new Block(-1, 0, false), new Block(2, 0, false), new Block(-2, 1, false)}
    };

    private readonly Board _board = board;

    public SpinType DetectSpin(IPiece piece, int lastKick)
    {
        int corners = 0, importantCorners = 0;
        for (int i = 0; i < _corners[piece.GetPieceIndex()].Length; i++)
        {
            Block cornerToCheck = CenterCorner(piece, _corners[piece.GetPieceIndex()][i]);
            if (board.IntersectBlock(cornerToCheck.X, cornerToCheck.Y))
            {
                corners++;
                if (i < ImportantCorners) importantCorners++;
            }
        }

        return DecideSpinType(corners, importantCorners, lastKick);
    }

    private SpinType DecideSpinType(int corners, int importantCorners, int lastKick)
    {
        if (corners >= 3)
        {
            if (importantCorners > 1 || lastKick == 4) return SpinType.FullSpin;
            return SpinType.MiniSpin;
        }

        return SpinType.NoSpin;
    }

    private Block CenterCorner(IPiece piece, Block corner)
    {
        Block rotatedCorner = corner.Rotate((Rotation)piece.RotIndex);
        Block centeredCorner = new Block(rotatedCorner.X + piece.X, rotatedCorner.Y + piece.Y, corner.Centered);
        return centeredCorner;
    }
}