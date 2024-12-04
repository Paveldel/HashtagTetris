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

    public int[][] GetMatrixCopy()
    {
        int[][] copy = new int[_width][];
        for (int i = 0; i < _width; i++)
        {
            copy[i] = new int[_height * 2];
            Array.Copy(_matrix[i], copy[i], _height * 2);
        }
        return copy;
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

    public void Lock(Piece piece)
    {
        Block[] blocks = piece.GetBlocks();
        for (int i = 0; i < blocks.Length; i++)
        {
            _matrix[blocks[i].X + piece.X][blocks[i].Y + piece.Y] = piece.GetPieceIndex();
        }
        ClearLines();
    }

    private void ClearLines()
    {
        for (int i = _height; i >= 0; i--)
        {
            if (IsLineFilled(i)) ClearLine(i);
        }
    }

    private bool IsLineFilled(int row)
    {
        for (int i = 0; i < _width; i++)
        {
            if (_matrix[i][row] == 0) return false;
        }
        return true;
    }
    
    private void ClearLine(int rowToRemove)
    {
        for (int i = rowToRemove; i < _height - 2; i++)
        {
            MoveLineDown(i);
        }
    }

    private void MoveLineDown(int row)
    {
        for (int i = 0; i < _width; i++)
        {
            _matrix[i][row] = _matrix[i][row + 1];
        }
    }

    public int getXCenter()
    {
        return (_width / 2) - 1;
    }

    public int getTop()
    {
        return _height - 1;
    }
}