using domain.data;

namespace domain.piecedata;

public interface IPieceData
{
    public Piece? GetPieceByIndex(int index);
    public int GetAmountOfPieces();
}