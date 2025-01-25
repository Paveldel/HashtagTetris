namespace domain;

public class Gravity
{
    private const int MaxAmountOfResets = 10;
    private const long StepDelay = 500;
    private const long LockDelay = 1000;
    
    private int _resetsLeft = MaxAmountOfResets;
    private bool _onGround = false;
    private long _lockTimer = long.MaxValue;
    private long _nextStep = GetCurrentTime();

    private ActivePiece? _activePiece = null;

    public void PieceMoved(bool onGround)
    {
        if (_onGround && !onGround)
        {
            _nextStep = GetCurrentTime() + StepDelay;
        }
        _onGround = onGround;
        if (_resetsLeft <= 0) return;
        UpdateLock(onGround);
    }

    private void UpdateLock(bool onGround)
    {
        if (onGround)
        {
            _resetsLeft--;
            _lockTimer = GetCurrentTime() + LockDelay;
        }
        else
        {
            _lockTimer = long.MaxValue;
        }
    }

    public void Reset()
    {
        _resetsLeft = MaxAmountOfResets;
        _onGround = false;
        _lockTimer = long.MaxValue;
        _nextStep = GetCurrentTime();
        Step();
    }

    public void Update()
    {
        long currentTime = GetCurrentTime();
        if (_onGround && currentTime > _lockTimer) _activePiece?.LockPiece();
        while (currentTime > _nextStep) Step();
    }

    private void Step()
    {
        _nextStep += StepDelay;
        if (!_activePiece!.MoveDown())
        {
            _nextStep = GetCurrentTime();
        }
    }

    public void SetActivePiece(ActivePiece newActivePiece)
    {
        _activePiece = newActivePiece;
    }

    private static long GetCurrentTime()
    {
        return DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}