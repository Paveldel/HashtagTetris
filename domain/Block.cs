namespace domain;

public class Block
{
    private readonly bool _centered;
    public int X { get; private set; }
    public int Y { get; private set; }
    
    public Block(int x, int y, bool centered)
    {
        this.X = x;
        this.Y = y;
        _centered = centered;
    }
    
    public Block Clone()
    {
        return new Block(X, Y, _centered);
    }

    public Block Rotate(Rotation rotation)
    {
        Block result = Clone();
        int amountOfRotations = GetAmountOfRotations(rotation);
        for (int i = 0; i < amountOfRotations; i++)
        {
            result.RotateClockWise();
        }
        return result;
    }

    private int GetAmountOfRotations(Rotation rotation)
    {
        if (rotation == Rotation.ANTI_CLOCKWISE)
            return 3;
        if (rotation == Rotation.REVERSE)
            return 2;
        if (rotation == Rotation.CLOCKWISE) return 1;
        return 0;
    }

    private void RotateClockWise()
    {
        if (!_centered) RotateAroundBlock();
        else RotateAroundCenter();
    }

    private void RotateAroundBlock()
    {
        int temp = X;
        X = Y;
        Y = -temp;
    }

    private void RotateAroundCenter()
    {
        int temp = X;
        X = Y + 1;
        Y = -temp;
    }

    protected bool Equals(Block other)
    {
        return _centered == other._centered && X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Block)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_centered, X, Y);
    }
}