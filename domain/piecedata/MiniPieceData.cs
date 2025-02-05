using domain.data;

namespace domain.piecedata;

public class MiniPieceData : IPieceData
{
    private static readonly IPiece[] Pieces = [
        null!,
        new Piece([new Block(0, 0)], 0, 0, PieceType.I),
        new Piece([new Block(0, 0), new Block(1, 0)], 0, 0, PieceType.T),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(-1, 0)], 0, 0, PieceType.O),
        new Piece([new Block(0, 0, true), new Block(1, -1, true), new Block(0, -1, true)], 0, 0, PieceType.L),
    ];

    public int GetAmountOfPieces()
    {
        return Pieces.Length - 1;
    }

    public IPiece? GetPieceByIndex(int index)
    {
        return Pieces[index]?.Clone() ?? null;
    }
}