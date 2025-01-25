﻿namespace domain;

public class Hold
{
    private int _heldPieceIndex = 0;
    private bool _holdEnabled = true;
    private readonly IPieceData _pieceData;

    public Hold(IPieceData pieceData)
    {
        _pieceData = pieceData;
    }

    public Piece? HoldPiece(Piece p)
    {
        if (!_holdEnabled) return p;
        int temp = _heldPieceIndex;
        _heldPieceIndex = p.GetPieceIndex();
        _holdEnabled = false;
        return _pieceData.GetPieceByIndex(temp);
    }

    public bool IsEnabled()
    {
        return _holdEnabled;
    }

    public int GetHeldPieceType()
    {
        return _heldPieceIndex;
    }

    public void EnableHold()
    {
        _holdEnabled = true;
    }
}