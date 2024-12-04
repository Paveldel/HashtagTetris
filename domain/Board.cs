namespace domain;

public class Board
{
    private readonly int _width = 10;
    private readonly int _height = 20;

    private int[][] _matrix;

    public Board()
    {
        InitBoard();
    }

    private void InitBoard()
    {
        _matrix = new int[_width][];
        for (int i = 0; i < _width; i++)
        {
            _matrix[i] = new int[_height * 2];
        }
    }

    public bool IntersectPiece(Piece piece)
    {
        Block[] blocks = piece.GetBlocks();
        for (int i = 0; i < blocks.Length; i++)
        {
            if (IntersectBlock(blocks[i].X + piece.X, blocks[i].Y + piece.Y)) return true;
        }
        return false;
    }

    public bool IntersectBlock(int x, int y)
    {
        if (x < 0 || y < 0) return true;
        if (x > _width) return true;
        return _matrix[x][y] != 0;
    }
}