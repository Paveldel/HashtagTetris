using domain.data;
using domain.Input;

namespace domain;

public class BoardRenderer
{
    public readonly int _amountOfPreviews;
    private readonly bool _showShadowPiece;
    private readonly Board _board;
    private readonly ActivePiece _piece;

    public int[][] Matrix { get; set; }
    public int[] Previews { get; set; }
    public int Held { get; set; }
    public BoardRenderer(Board board, ActivePiece piece, int amountOfPreviews, bool showShadowPiece)
    {
        _board = board;
        _piece = piece;
        _amountOfPreviews = amountOfPreviews;
        _showShadowPiece = showShadowPiece;
        UpdateBoardToRender();
    }

    public int[][] BoardToRender()
    {
        int[][] result = GetMatrixCopy(_board.GetBoard());
        if (!_piece.HasPlayerLost() && !_piece.InDelay())
        {
            ShowShadowPiece(result);
            ShowPiece(result);
        }
        return result;
    }

    private void ShowPiece(int[][] result)
    {
        if (!_showShadowPiece) return;
        IPiece piece = _piece.GetPiece();
        Block[] blocks = piece.GetBlocks();
        for (int i = 0; i < blocks.Length; i++)
        {
            SetSquareOnMatrix(result, blocks[i].X + piece.X, blocks[i].Y + piece.Y, piece.GetPieceIndex());
        }
    }
    
    private void ShowShadowPiece(int[][] result)
    {
        IPiece piece = _piece.DeepDrop();
        Block[] blocks = piece.GetBlocks();
        for (int i = 0; i < blocks.Length; i++)
        {
            SetSquareOnMatrix(result, blocks[i].X + piece.X, blocks[i].Y + piece.Y, (-piece.GetPieceIndex()));
        }
    }

    private void SetSquareOnMatrix(int[][] matrix, int x, int y, int pieceIndex)
    {
        if (x < 0 || x >= matrix.Length) return;
        if (y < 0 || y >= matrix[x].Length) return;
        matrix[x][y] = pieceIndex;
    }

    public int[][] GetMatrixCopy(int[][] matrix)
    {
        int[][] copy = new int[matrix.Length][];
        for (int i = 0; i < matrix.Length; i++)
        {
            copy[i] = new int[matrix[i].Length / 2];
            Array.Copy(matrix[i], copy[i], matrix[i].Length / 2);
        }
        return copy;
    }
    
    public void UpdateBoardToRender()
    {
        Matrix = BoardToRender();
        GetPreviews();
        UpdateHeld();
    }

    private void UpdateHeld()
    {
        Held = _piece.GetHeldType();
        if (!_piece.IsHoldEnabled()) Held = -Held;
    }

    private void GetPreviews()
    {
        Previews = new int[_amountOfPreviews];
        IPiece[] previewPieces = _piece.GetNextPieces(_amountOfPreviews);
        for (int i = 0; i < _amountOfPreviews; i++)
        {
            Previews[i] = previewPieces[i].GetPieceIndex();
        }
    }

    public int AmountOfGarbageQueued()
    {
        return _board.GetAmountOfGarbage();
    }
}