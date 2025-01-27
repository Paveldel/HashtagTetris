namespace domain;

public class Block
{
    public bool Centered { get; }
    public int X { get; private set; }
    public int Y { get; private set; }
    
    public Block(int x, int y, bool centered)
    {
        this.X = x;
        this.Y = y;
        Centered = centered;
    }
    
    public Block Clone()
    {
        return new Block(X, Y, Centered);
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
        if (rotation == Rotation.AntiClockwise)
            return 3;
        if (rotation == Rotation.Reverse)
            return 2;
        if (rotation == Rotation.Clockwise) return 1;
        return 0;
    }

    private void RotateClockWise()
    {
        if (!Centered) RotateAroundBlock();
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
        return Centered == other.Centered && X == other.X && Y == other.Y;
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
        return HashCode.Combine(Centered, X, Y);
    }
}