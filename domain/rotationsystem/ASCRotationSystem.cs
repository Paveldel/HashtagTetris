﻿using domain.data;

namespace domain.rotationsystem;

public class ASCRotationSystem(Board board) : KickRotationSystem(board)
{
    private static readonly Kick[] ClockWiseKicks =
    {
        new Kick(0, 0), new Kick(-1, 0), new Kick(0, -1), new Kick(-1, -1), new Kick(0, -2), new Kick(-1, -2), new Kick(-2, 0),
        new Kick(-2, -1), new Kick(-2, -2), new Kick(1, 0), new Kick(1, -1), new Kick(0, 1), new Kick(-1, 1), new Kick(-2, 1),
        new Kick(1, -2), new Kick(2, 0), new Kick(0, 2), new Kick(-1, 2), new Kick(-2, 2), new Kick(2, -1), new Kick(2, -2)
    };
    
    private static readonly Kick[] CounterClockWiseKicks =
    {
        new Kick(0, 0), new Kick(-1, 0), new Kick(0, -1), new Kick(-1, -1), new Kick(0, -2), new Kick(-1, -2), new Kick(-2, 0),
        new Kick(-2, -1), new Kick(-2, -2), new Kick(1, 0), new Kick(1, -1), new Kick(0, 1), new Kick(-1, 1), new Kick(-2, 1),
        new Kick(1, -2), new Kick(2, 0), new Kick(0, 2), new Kick(-1, 2), new Kick(-2, 2), new Kick(2, -1), new Kick(2, -2)
    };
    
    public override IPiece RotatePiece(IPiece piece, Rotation rotation)
    {
        if (rotation == Rotation.Reverse) return RotateReverse(piece);
        return RotateNormal(piece, rotation);
    }

    private IPiece RotateNormal(IPiece piece, Rotation rotation)
    {
        IPiece rotatedPiece = piece.Rotate(rotation);
        TryKicks(rotatedPiece, GetKicks(rotation));
        if (KickIndex == FailedRotation) return piece;
        return rotatedPiece;
    }

    private Kick[] GetKicks(Rotation rotation)
    {
        if (rotation == Rotation.Clockwise) return ClockWiseKicks;
        return CounterClockWiseKicks;
    }

    private IPiece RotateReverse(IPiece piece)
    {
        IPiece rotatedPiece = piece.Rotate(Rotation.Reverse);
        if (board.IntersectPiece(rotatedPiece)) return piece;
        return rotatedPiece;
    }
}