using domain.data;

namespace domain.piecedata;

public interface IPieceData
{
    public IPiece? GetPieceByIndex(int index);
    public int GetAmountOfPieces();
}