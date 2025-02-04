namespace domain.data;

public interface IPiece
{
    public int X { get; set; }
    public int Y { get; set; }
    public int RotIndex { get; }
    public Block[] GetBlocks();
    public int GetPieceIndex();
    public IPiece Clone();
    public IPiece Rotate(Rotation rotation);
    public void MoveLeft();
    public void MoveRight();
    public void MoveDown();
}