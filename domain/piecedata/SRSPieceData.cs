using domain.data;

namespace domain.piecedata;

public class SrsPieceData : IPieceData
{
    private const int AmountOfPieces = 7;

    private static readonly IPiece[] Pieces = [
        null!,
        new Piece([new Block(0, 0, true), new Block(1, 0, true), new Block(-1, 0, true), new Block(2, 0, true)], 0, 0, PieceType.I),
        new Piece([new Block(0, 0, false), new Block(1, 0, false), new Block(-1, 0, false), new Block(0, 1, false)], 0, 0, PieceType.T),
        new Piece([new Block(0, 0, true), new Block(1, 0, true), new Block(0, -1, true), new Block(1, -1, true)], 0, 1, PieceType.O),
        new Piece([new Block(0, 0, false), new Block(1, 0, false), new Block(-1, 0, false), new Block(1, 1, false)], 0, 0, PieceType.L),
        new Piece([new Block(0, 0, false), new Block(1, 0, false), new Block(-1, 0, false), new Block(-1, 1, false)], 0, 0, PieceType.J),
        new Piece([new Block(0, 0, false), new Block(0, 1, false), new Block(-1, 0, false), new Block(1, 1, false)], 0, 0, PieceType.S),
        new Piece([new Block(0, 0, false), new Block(1, 0, false), new Block(0, 1, false), new Block(-1, 1, false)], 0, 0, PieceType.Z),
    ];

    public int GetAmountOfPieces()
    {
        return AmountOfPieces;
    }

    public IPiece? GetPieceByIndex(int index)
    {
        return Pieces[index]?.Clone() ?? null;
    }
}