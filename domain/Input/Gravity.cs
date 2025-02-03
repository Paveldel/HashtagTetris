using domain.timer;
using ITimer = domain.timer.ITimer;

namespace domain.Input;

public class Gravity : IUpdatable
{
    private const int MaxAmountOfResets = 10;
    private const long StepDelay = 500;
    private const long LockDelay = 1000;
    
    private int _resetsLeft = MaxAmountOfResets;
    private bool _onGround;
    private long _lockTimer = long.MaxValue;
    private long _nextStep = long.MaxValue;
    private ITimer? _timer;

    private ActivePiece? _activePiece;

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

    public void Reset(bool isPieceOnGround)
    {
        UpdateLock(isPieceOnGround);
        _resetsLeft = MaxAmountOfResets;
        _onGround = isPieceOnGround;
        _nextStep = GetCurrentTime();
        Step();
    }

    public void Update(long currentTime)
    {
        if ((bool)_activePiece?.InDelay()) return;
        if (_onGround && currentTime > _lockTimer) _activePiece?.LockPiece();
        while (currentTime > _nextStep) Step();
    }

    private void Step()
    {
        _nextStep += StepDelay;
        if (!_activePiece!.MoveDown())
        {
            _nextStep = GetCurrentTime() + StepDelay;
        }
    }

    public void SetActivePiece(ActivePiece newActivePiece)
    {
        _activePiece = newActivePiece;
    }

    private long GetCurrentTime()
    {
        return _timer?.GetCurrentTime() ?? 0;
    }

    public void SetTimer(ITimer timer)
    {
        _timer = timer;
        _nextStep = GetCurrentTime() + StepDelay;
    }
}