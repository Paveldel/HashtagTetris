namespace domain;

public class BoardRenderer
{
    private readonly Board _board;
    private readonly ActivePiece _piece;

    public BoardRenderer(Board board, ActivePiece piece)
    {
        _board = board;
        _piece = piece;
    }

    public int[][] BoardToRender()
    {
        int[][] result = _board.GetMatrixCopy();
        Piece piece = _piece.GetPiece();
        Block[] blocks = piece.GetBlocks();
        for (int i = 0; i < blocks.Length; i++)
        {
            result[blocks[i].X + piece.X][blocks[i].Y + piece.Y] = piece.GetPieceIndex();
        }
        return result;
    }
}