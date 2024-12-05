namespace domain;

public class Input(ActivePiece playerPiece)
{
    private long _leftTimer = long.MaxValue;
    private long _rightTimer = long.MaxValue;
    private long _softDropTimer = long.MaxValue;
    
    public void HandleKeyPress(string keyCode)
    {
        if (keyCode == Controls.MoveLeft)
        {
            playerPiece.MoveLeft();
            _leftTimer = getCurrentTime() + Controls.DAS;
            _rightTimer = long.MaxValue;
        }
        else if (keyCode == Controls.SoftDrop)
        {
            playerPiece.MoveDown();
            _softDropTimer = getCurrentTime() + Controls.SDF;
        }
        else if (keyCode == Controls.MoveRight)
        {
            playerPiece.MoveRight();
            _rightTimer = getCurrentTime() + Controls.DAS;
            _leftTimer = long.MaxValue;
        }
        else if (keyCode == Controls.Hold)
        {
            playerPiece.Hold();
        }
        else if (keyCode == Controls.HardDrop)
        {
            playerPiece.HardDrop();
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