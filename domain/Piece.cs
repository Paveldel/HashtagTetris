namespace domain;

public class Piece
{
    public int X { get; set; }
    public int Y { get; set; }

    public int RotIndex { get; private set; }

    private readonly PieceType _typeIndex;
    
    private readonly Block[] _blocks;

    public Piece(Block[] blocks, int x, int y, PieceType typeIndex)
    {
        _blocks = blocks;
        X = x;
        Y = y;
        _typeIndex = typeIndex;
        RotIndex = 0;
    }

    public Block[] GetBlocks()
    {
        return _blocks;
    }

    public int GetPieceIndex()
    {
        return (int)_typeIndex;
    }

    public Piece Clone()
    {
        Block[] copyBlocks = new Block[_blocks.Length];
        for (int i = 0; i < _blocks.Length; i++)
        {
            copyBlocks[i] = _blocks[i].Clone();
        }

        return new Piece(copyBlocks, X, Y, _typeIndex);
    }

    public Piece Rotate(Rotation rotation)
    {
        Piece rotatedPiece = Clone();
        for (int i = 0; i < _blocks.Length; i++)
        {
            rotatedPiece._blocks[i] = rotatedPiece._blocks[i].Rotate(rotation);
        }
        rotatedPiece.RotIndex += ((int)rotation % 4);
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
        movedPiece.Y--;
        return movedPiece;
    }

    protected bool Equals(Piece other)
    {
        return _typeIndex == other._typeIndex && _blocks.Equals(other._blocks) && X == other.X && Y == other.Y && RotIndex == other.RotIndex;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Piece)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)_typeIndex, _blocks, X, Y, RotIndex);
    }
}