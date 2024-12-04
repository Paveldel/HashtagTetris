﻿namespace domain;

public class Piece
{
    public int X { get; set; }
    public int Y { get; set; }

    private Block[] _blocks;

    public Piece(Block[] blocks, int x, int y)
    {
        _blocks = blocks;
        this.X = x;
        this.Y = y;
    }

    public Piece Clone()
    {
        Block[] copyBlocks = new Block[_blocks.Length];
        for (int i = 0; i < _blocks.Length; i++)
        {
            copyBlocks[i] = _blocks[i].Clone();
        }

        return new Piece(copyBlocks, X, Y);
    }

    public Piece Rotate(Rotation rotation)
    {
        Piece rotatedPiece = Clone();
        for (int i = 0; i < _blocks.Length; i++)
        {
            rotatedPiece._blocks[i].Rotate(rotation);
        }

        return rotatedPiece;
    }

    public Piece MoveLeft()
    {
        Piece movedPiece = Clone();
        movedPiece.X--;
        return movedPiece;
    }
    
    public Piece MoveRight()
    {
        Piece movedPiece = Clone();
        movedPiece.X++;
        return movedPiece;
    }
    
    public Piece MoveDown()
    {
        Piece movedPiece = Clone();
        movedPiece.Y++;
        return movedPiece;
    }
}