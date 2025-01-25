namespace domain;

public interface IPieceData
{
    public Piece GetPieceByIndex(int index);
    public int GetAmountOfPieces();
}