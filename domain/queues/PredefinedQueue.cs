using domain.data;
using domain.piecedata;

namespace domain.queues;

public class PredefinedQueue : AbstractQueue
{
    private const int AmountOfStartingPieces = 50;

    private static readonly Dictionary<char, int> CharacterConversion = new Dictionary<char, int> {
        {'I', (int)PieceType.I},
        {'T', (int)PieceType.T},
        {'O', (int)PieceType.O},
        {'L', (int)PieceType.L},
        {'J', (int)PieceType.J},
        {'S', (int)PieceType.S},
        {'Z', (int)PieceType.Z},
    };

    private char[] _order;
    
    public PredefinedQueue(SrsPieceData pieceData, string order) : base(pieceData)
    {
        _order = order.ToCharArray();
        ValidateCharacters();
        while (Queue.Count < AmountOfStartingPieces)
        {
            AddPiece();
        }
    }

    private void ValidateCharacters()
    {
        foreach (var pieceCharacter in _order)
        {
            if (!CharacterConversion.ContainsKey(pieceCharacter))
                throw new Exception("could not decipher character to piece");
        }
    }

    public override void AddPiece()
    {
        if (Queue.Count > AmountOfStartingPieces) return;
        foreach (var pieceCharacter in _order)
        {
            Queue.Add(ConvertCharacterToPiece(pieceCharacter));
        }
    }

    private IPiece ConvertCharacterToPiece(char pieceCharacter)
    {
        return PieceData.GetPieceByIndex(CharacterConversion[pieceCharacter])!;
    }
}