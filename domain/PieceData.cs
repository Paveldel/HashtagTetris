namespace domain;

public class PieceData
{
    public static readonly int AmountOfPieces = 7;
    
    public static readonly Piece[] Pieces = [
        null!,
        new Piece([new Block(0, 0, true), new Block(1, 0, true), new Block(-1, 0, true), new Block(2, 0, true)], 0, 0, PieceType.I),
        new Piece([new Block(0, 0, false), new Block(1, 0, false), new Block(-1, 0, false), new Block(0, 1, false)], 0, 0, PieceType.T),
        new Piece([new Block(0, 0, true), new Block(1, 0, true), new Block(0, -1, true), new Block(1, -1, true)], 0, 0, PieceType.O),
        new Piece([new Block(0, 0, false), new Block(1, 0, false), new Block(-1, 0, false), new Block(1, 1, false)], 0, 0, PieceType.L),
        new Piece([new Block(0, 0, false), new Block(1, 0, false), new Block(-1, 0, false), new Block(-1, 1, false)], 0, 0, PieceType.J),
        new Piece([new Block(0, 0, false), new Block(0, 1, false), new Block(-1, 0, false), new Block(1, 1, false)], 0, 0, PieceType.S),
        new Piece([new Block(0, 0, false), new Block(1, 0, false), new Block(0, 1, false), new Block(-1, 1, false)], 0, 0, PieceType.Z),
    ];
}