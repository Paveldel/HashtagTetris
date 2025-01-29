﻿namespace domain;

public class Board
{
    private readonly int _width = 10;
    private readonly int _height = 20;

    private readonly IDamageCalculator _damageCalculator;
    
    private int[][] _matrix;

    public Board()
    {
        InitBoard();
        _damageCalculator = new GuideLineDamageCalculator();
        for (int i = 0; i < _width; i++)
        {
            TakeGarbageLine(i);
        }
    }

    public int[][] GetBoard()
    {
        return _matrix;
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
        return blocks.Any(block => IntersectBlock(block.X + piece.X, block.Y + piece.Y));
    }

    public bool IntersectBlock(int x, int y)
    {
        if (x < 0 || y < 0) return true;
        if (x >= _width) return true;
        return _matrix[x][y] != 0;
    }

    public void Lock(Piece piece, SpinType spinType)
    {
        PlaceBlocksForPiece(piece);
        int amountOfLinesCleared = ClearLines();
        _damageCalculator.CalculateDamage(amountOfLinesCleared, spinType, false);
    }

    private void PlaceBlocksForPiece(Piece piece)
    {
        Block[] blocks = piece.GetBlocks();
        foreach (var block in blocks)
        {
            _matrix[block.X + piece.X][block.Y + piece.Y] = piece.GetPieceIndex();
        }
    }

    private int ClearLines()
    {
        int amountOfLinesCleared = 0;
        for (int i = (_height * 2) - 1; i >= 0; i--)
        {
            if (IsLineFilled(i))
            {
                ClearLine(i);
                amountOfLinesCleared++;
            }
        }
        return amountOfLinesCleared;
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
        for (int i = rowToRemove; i < (_height * 2) - 1; i++)
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

    public void TakeGarbageLine(int column)
    {
        MoveBoardUp();
        FillGarbageLine(column);
    }

    private void FillGarbageLine(int column)
    {
        for (int i = 0; i < _width; i++)
        {
            if (i != column) _matrix[i][0] = (int)PieceType.Garbage;
            else _matrix[i][0] = (int)PieceType.Empty;
        }
    }

    private void MoveBoardUp()
    {
        for (int i = (_height * 2) - 2; i > 0; i--)
        {
            MoveLineUp(i);
        }
    }

    private void MoveLineUp(int row)
    {
        for (int i = 0; i < _width; i++)
        {
            _matrix[i][row] = _matrix[i][row - 1];
        }
    }

    public int GetXCenter()
    {
        return (_width / 2) - 1;
    }

    public int GetTop()
    {
        return _height - 1;
    }
}