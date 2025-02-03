using domain.timer;
using ITimer = domain.timer.ITimer;

namespace domain.Input;

public class Input(ActivePiece playerPiece) : IUpdatable
{
    private long _leftTimer = long.MaxValue;
    private long _rightTimer = long.MaxValue;
    private long _softDropTimer = long.MaxValue;
    private ITimer _timer;

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
            playerPiece.Rotate(Rotation.AntiClockwise);
        }
        else if (keyCode == Controls.Reverse)
        {
            playerPiece.Rotate(Rotation.Reverse);
        }
        else if (keyCode == Controls.Clockwise)
        {
            playerPiece.Rotate(Rotation.Clockwise);
        }
    }

    private void HandleHardDrop()
    {
        playerPiece.HardDrop();
        if (_rightTimer != long.MaxValue)
        {
            _rightTimer = GetCurrentTime() + Controls.DCD;
        }
        if (_leftTimer != long.MaxValue)
        {
            _leftTimer = GetCurrentTime() + Controls.DCD;
        }
    }

    private void HandleRightInput()
    {
        playerPiece.MoveRight();
        _rightTimer = GetCurrentTime() + Controls.DAS;
        _leftTimer = long.MaxValue;
    }

    private void HandleDownInput()
    {
        playerPiece.MoveDown();
        _softDropTimer = GetCurrentTime() + Controls.SDF;
    }

    private void HandleLeftInput()
    {
        playerPiece.MoveLeft();
        _leftTimer = GetCurrentTime() + Controls.DAS;
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

    public void Update(long currentTime)
    {
        UpdateLeftInput(currentTime);
        UpdateRightInput(currentTime);
        UpdateSoftDropInput(currentTime);
    }

    private void UpdateLeftInput(long currentTime)
    {
        while (_leftTimer < currentTime)
        {
            if (playerPiece.MoveLeft())
            {
                _leftTimer += Controls.ARR;
            }
            else
            {
                _leftTimer = currentTime;
            }
        }
    }
    
    private void UpdateRightInput(long currentTime)
    {
        while (_rightTimer < currentTime)
        {
            if (playerPiece.MoveRight())
            {
                _rightTimer += Controls.ARR;
            }
            else
            {
                _rightTimer = currentTime;
            }
        }
    }
    
    private void UpdateSoftDropInput(long currentTime)
    {
        while (_softDropTimer < currentTime)
        {
            if (playerPiece.MoveDown())
            {
                _softDropTimer += Controls.SDF;
            }
            else
            {
                _softDropTimer = currentTime;
            }
        }
    }

    private long GetCurrentTime()
    {
        return _timer.GetCurrentTime();
    }

    public void SetTimer(ITimer timer)
    {
        _timer = timer;
    }
}