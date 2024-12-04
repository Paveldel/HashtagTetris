namespace domain;

public class Block
{
    private readonly bool _centered;
    public int X { get; set; }
    public int Y { get; set; }
    
    public Block(int x, int y ,bool centered)
    {
        this.X = x;
        this.Y = y;
        _centered = centered;
    }
    
    public Block Clone()
    {
        return new Block(X, Y, _centered);
    }

    public void Rotate(bool rotateAroundBlock, Rotation rotation)
    {
        int amountOfRotations = GetAmountOfRotations(rotation);
        for (int i = 0; i < amountOfRotations; i++)
        {
            RotateClockWise();
        }
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
}