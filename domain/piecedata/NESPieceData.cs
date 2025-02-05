using domain.data;

namespace domain.piecedata;

public class NESPieceData : IPieceData
{
    private static readonly IPiece[] Pieces = [
        null!,
        new CustomPiece(
            new Block[][] {
                [new Block(0, 0), new Block(1, 0), new Block(2, 0), new Block(-1, 0)],
                [new Block(0, 0), new Block(0, 1), new Block(0, -1), new Block(0, -2)],
            }, PieceType.I),
        new CustomPiece(
            new Block[][] {
                [new Block(0, 1), new Block(0, 0), new Block(1, 0), new Block(-1, 0)],
                [new Block(0, 0), new Block(1, 0), new Block(0, -1), new Block(0, 1)],
                [new Block(0, 0), new Block(1, 0), new Block(0, -1), new Block(-1, 0)],
                [new Block(0, 0), new Block(-1, 0), new Block(0, -1), new Block(0, 1)],
            }, PieceType.T),
        new CustomPiece(
            new Block[][] {
                [new Block(0, 0), new Block(0, -1), new Block(1, -1), new Block(1, 0)],
            }, PieceType.O),
        new CustomPiece(
            new Block[][] {
                [new Block(1, 1), new Block(0, 0), new Block(1, 0), new Block(-1, 0)],
                [new Block(0, 0), new Block(1, -1), new Block(0, -1), new Block(0, 1)],
                [new Block(0, 0), new Block(1, 0), new Block(-1, -1), new Block(-1, 0)],
                [new Block(0, 0), new Block(-1, 1), new Block(0, -1), new Block(0, 1)],
            }, PieceType.L),
        new CustomPiece(
            new Block[][] {
                [new Block(-1, 1), new Block(0, 0), new Block(-1, 0), new Block(1, 0)],
                [new Block(0, 0), new Block(-1, -1), new Block(0, -1), new Block(0, 1)],
                [new Block(0, 0), new Block(-1, 0), new Block(1, -1), new Block(1, 0)],
                [new Block(0, 0), new Block(1, 1), new Block(0, -1), new Block(0, 1)],
            }, PieceType.J),
        new CustomPiece(
            new Block[][] {
                [new Block(0, -1), new Block(-1, -1), new Block(0, 0), new Block(1, 0)],
                [new Block(0, 0), new Block(1, 0), new Block(1, -1), new Block(0, 1)],
            }, PieceType.S),
        new CustomPiece(
            new Block[][] {
                [new Block(0, -1), new Block(1, -1), new Block(0, 0), new Block(-1, 0)],
                [new Block(1, 0), new Block(0, 0), new Block(0, -1), new Block(1, 1)],
            }, PieceType.Z),
    ];
    
    public IPiece? GetPieceByIndex(int index)
    {
        return Pieces[index]?.Clone() ?? null;
    }

    public int GetAmountOfPieces()
    {
        return Pieces.Length - 1;
    }
}