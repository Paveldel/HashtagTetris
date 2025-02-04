namespace domain.data;

public class Piece(Block[] blocks, int x, int y, PieceType typeIndex, int rotIndex = 0) : IPiece
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public int RotIndex { get; private set; } = rotIndex;

    private readonly Block[] _blocks = blocks;

    public Block[] GetBlocks()
    {
        return _blocks;
    }

    public int GetPieceIndex()
    {
        return (int)typeIndex;
    }

    public IPiece Clone()
    {
        Block[] copyBlocks = new Block[_blocks.Length];
        for (int i = 0; i < _blocks.Length; i++)
        {
            copyBlocks[i] = _blocks[i].Clone();
        }

        return new Piece(copyBlocks, X, Y, typeIndex, RotIndex);
    }

    public IPiece Rotate(Rotation rotation)
    {
        Piece rotatedPiece = (Piece)Clone();
        for (int i = 0; i < _blocks.Length; i++)
        {
            rotatedPiece._blocks[i] = rotatedPiece._blocks[i].Rotate(rotation);
        }
        rotatedPiece.RotIndex = (RotIndex + (int)rotation) % 4;
        return rotatedPiece;
    }

    public void MoveLeft()
    {
        X--;
    }
    
    public void MoveRight()
    {
        X++;
    }
    
    public void MoveDown()
    {
        Y--;
    }

    protected bool Equals(IPiece other)
    {
        return _blocks.Equals(other.GetBlocks()) && X == other.X && Y == other.Y && RotIndex == other.RotIndex;
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
        return HashCode.Combine(_blocks, X, Y, RotIndex);
    }
}