﻿namespace domain;

public class Input(ActivePiece playerPiece)
{
    private long _leftTimer = long.MaxValue;
    private long _rightTimer = long.MaxValue;
    private long _softDropTimer = long.MaxValue;
    
    public void HandleKeyPress(string keyCode)
    {
        if (keyCode == Controls.MoveLeft)
        {
            HandleLeftInput();
        }
        else if (keyCode == Controls.SoftDrop)
        {
            HandleDownInput();
        }
        else if (keyCode == Controls.MoveRight)
        {
            HandleRightInput();
        }
        else if (keyCode == Controls.Hold)
        {
            playerPiece.Hold();
        }
        else if (keyCode == Controls.HardDrop)
        {
            HandleHardDrop();
        }
        else if (keyCode == Controls.AntiClockwise)
        {
            playerPiece.Rotate(Rotation.ANTI_CLOCKWISE);
        }
        else if (keyCode == Controls.Reverse)
        {
            playerPiece.Rotate(Rotation.REVERSE);
        }
        else if (keyCode == Controls.Clockwise)
        {
            playerPiece.Rotate(Rotation.CLOCKWISE);
        }
    }

    private void HandleHardDrop()
    {
        playerPiece.HardDrop();
        if (_rightTimer != long.MaxValue)
        {
            _rightTimer = getCurrentTime() + Controls.DCD;
        }
        if (_leftTimer != long.MaxValue)
        {
            _leftTimer = getCurrentTime() + Controls.DCD;
        }
    }

    private void HandleRightInput()
    {
        playerPiece.MoveRight();
        _rightTimer = getCurrentTime() + Controls.DAS;
        _leftTimer = long.MaxValue;
    }

    private void HandleDownInput()
    {
        playerPiece.MoveDown();
        _softDropTimer = getCurrentTime() + Controls.SDF;
    }

    private void HandleLeftInput()
    {
        playerPiece.MoveLeft();
        _leftTimer = getCurrentTime() + Controls.DAS;
        _rightTimer = long.MaxValue;
    }

    public void HandleKeyRelease(string keyCode)
    {
        if (keyCode == Controls.MoveLeft)
        {
            _leftTimer = long.MaxValue;
        }
        else if (keyCode == Controls.SoftDrop)
        {
            _softDropTimer = long.MaxValue;
        }
        else if (keyCode == Controls.MoveRight)
        {
            _rightTimer = long.MaxValue;
        }
    }

    public void update()
    {
        
    }

    private long getCurrentTime()
    {
        return DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}