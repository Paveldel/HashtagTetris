namespace domain.data;

public class CustomPiece(Block[][] blocks, PieceType pieceType, int rotIndex = 2, int x = 0, int y = 0) : IPiece
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int RotIndex { get; private set; } = rotIndex % blocks.Length;
    private PieceType _pieceType = pieceType;

    private Block[][] _blocks = blocks;

    public Block[] GetBlocks()
    {
        return _blocks[RotIndex];
    }

    public int GetPieceIndex()
    {
        return (int)_pieceType;
    }

    public IPiece Clone()
    {
        return new CustomPiece(_blocks, _pieceType, RotIndex, X, Y);
    }

    public IPiece Rotate(Rotation rotation)
    {
        CustomPiece rotatePiece = (CustomPiece)Clone();
        rotatePiece.RotIndex += (int)rotation;
        rotatePiece.RotIndex = rotatePiece.RotIndex % _blocks.Length;
        return rotatePiece;
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
}