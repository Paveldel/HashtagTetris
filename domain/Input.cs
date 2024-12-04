namespace domain;

public class Input(ActivePiece playerPiece)
{
    public void HandleKeyPress(string keyCode)
    {
        if (keyCode == Controls.MoveLeft)
        {
            playerPiece.MoveLeft();
        }
        else if (keyCode == Controls.SoftDrop)
        {
            playerPiece.MoveDown();
        }
        else if (keyCode == Controls.MoveRight)
        {
            playerPiece.MoveRight();
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
        
    }
}