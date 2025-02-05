using domain.data;

namespace domain.piecedata;

public class PentominoData : IPieceData
{
    private const int AmountOfPieces = 18;

    private static readonly IPiece[] Pieces = [
        null!,
        new Piece([new Block(0, 0), new Block(1, 0), new Block(-1, 0), new Block(2, 0), new Block(-2, 0)], 0, 0, PieceType.I),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(-1, 0), new Block(0, 1), new Block(0, 2)], 0, 0, PieceType.T),
        new Piece([new Block(0, 0), new Block(0, 2), new Block(-1, 1), new Block(1, 0), new Block(0, 1)], 0, 0, PieceType.E),
        new Piece([new Block(0, 0), new Block(-1, 0), new Block(-2, 0), new Block(1, 0), new Block(1, 1)], 0, 0, PieceType.L),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(2, 0), new Block(-1, 0), new Block(-1, 1)], 0, 0, PieceType.J),
        new Piece([new Block(0, 0), new Block(0, 2), new Block(1, 2), new Block(-1, 0), new Block(0, 1)], 0, 0, PieceType.S),
        new Piece([new Block(0, 0), new Block(0, 2), new Block(-1, 2), new Block(1, 0), new Block(0, 1)], 0, 0, PieceType.Z),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(-1, 0), new Block(1, 1), new Block(1, 2)], 0, 0, PieceType.V),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(-1, 0), new Block(-1, 1), new Block(1, 1)], 0, 0, PieceType.U),
        new Piece([new Block(0, 0), new Block(0, 1), new Block(-1, 0), new Block(1, 1), new Block(1, 2)], 0, 0, PieceType.W),
        new Piece([new Block(0, 0), new Block(0, 1), new Block(-1, 0), new Block(0, -1), new Block(1, 0)], 0, 0, PieceType.X),
        new Piece([new Block(0, 0), new Block(-1, 0), new Block(-2, 0), new Block(0, 1), new Block(1, 1)], 0, 0, PieceType.H),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(2, 0), new Block(0, 1), new Block(-1, 1)], 0, 0, PieceType.N),
        new Piece([new Block(0, 0), new Block(-1, 0), new Block(-2, 0), new Block(1, 0), new Block(0, 1)], 0, 0, PieceType.Y),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(2, 0), new Block(-1, 0), new Block(0, 1)], 0, 0, PieceType.R),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(-1, 1), new Block(-1, 0), new Block(0, 1)], 0, 0, PieceType.P),
        new Piece([new Block(0, 0), new Block(1, 0), new Block(1, 1), new Block(-1, 0), new Block(0, 1)], 0, 0, PieceType.Q),
        new Piece([new Block(0, 0), new Block(0, 2), new Block(1, 1), new Block(-1, 0), new Block(0, 1)], 0, 0, PieceType.F),
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