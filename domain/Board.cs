using domain.damage;
using domain.damage.calculator;
using domain.damage.queue;
using domain.data;
using domain.timer;
using ITimer = domain.timer.ITimer;

namespace domain;

public class Board : IDamageReceiver, IUpdatable
{
    private readonly long _lineClearDelay;
    
    private readonly int _width;
    private readonly int _height;

    private IDamageCalculator _damageCalculator;
    private IDamageQueue _damageQueue;
    
    private int[][] _matrix;
    
    private ITimer _timer;
    private bool _inAnimation = false;
    private long _lineClearTimer = 0;
    public long CurrentDelay { get; private set; }

    public Board(int width, int height, long lineClearDelay)
    {
        _width = width;
        _height = height;
        _lineClearDelay = lineClearDelay;
        InitBoard();
    }
    
    private void InitBoard()
    {
        _matrix = new int[_width][];
        for (int i = 0; i < _width; i++)
        {
            _matrix[i] = new int[_height * 2];
        }
    }

    public int[][] GetBoard()
    {
        return _matrix;
    }

    public void SetDamageCalculator(IDamageCalculator calculator)
    {
        _damageCalculator = calculator;
    }
    
    public void SetDamageQueue(IDamageQueue queue)
    {
        _damageQueue = queue;
    }

    public void SetTarget(IDamageReceiver target)
    {
        _damageQueue.RegisterAsReceiver(target);
    }

    public bool IntersectPiece(IPiece piece)
    {
        Block[] blocks = piece.GetBlocks();
        for (int i = 0; i < blocks.Length; i++)
        {
            if (IntersectBlock(blocks[i].X + piece.X, blocks[i].Y + piece.Y)) return true;
        }
        return false;
    }

    public bool IntersectBlock(int x, int y)
    {
        if (x < 0 || y < 0) return true;
        if (x >= _width) return true;
        return _matrix[x][y] != 0;
    }

    private readonly List<int> _filledLines = new();
    public void Lock(IPiece piece, SpinType spinType)
    {
        PlaceBlocksForPiece(piece);
        GetFilledLines();
        HandleDamage(spinType);
        AnimateClearedLines();
        Update(_timer.GetCurrentTime());
    }

    private void ClearFilledLines()
    {
        while (_filledLines.Any())
        {
            ClearLine(_filledLines[0]);
            _filledLines.RemoveAt(0);
        }
    }

    private void AnimateClearedLines()
    {
        CurrentDelay = 0;
        _inAnimation = true;
        if (_lineClearDelay == 0 || _filledLines.Count == 0) return;
        CurrentDelay = _lineClearDelay;
        _lineClearTimer = _timer.GetCurrentTime() + CurrentDelay;

        MakeClearedLinesWhite();
    }

    public long GetCurrentDelay()
    {
        return CurrentDelay;
    }

    private void MakeClearedLinesWhite()
    {
        foreach (var line in _filledLines)
        {
            for (int i = 0; i < _width; i++)
            {
                _matrix[i][line] = (int)PieceType.ClearingLine;
            }
        }
    }

    private void HandleDamage(SpinType spinType)
    {
        int amountOfLinesCleared = _filledLines.Count;
        int damageToSend = _damageCalculator.CalculateDamage(amountOfLinesCleared, spinType, IsPerfectClear());
        _damageQueue.PiecePlaced(damageToSend, amountOfLinesCleared > 0);
    }

    private void PlaceBlocksForPiece(IPiece piece)
    {
        Block[] blocks = piece.GetBlocks();
        foreach (var block in blocks)
        {
            _matrix[block.X + piece.X][block.Y + piece.Y] = piece.GetPieceIndex();
        }
    }
    
    private void GetFilledLines()
    {
        for (int i = (_height * 2) - 1; i >= 0; i--)
        {
            if (IsLineFilled(i))
            {
                _filledLines.Add(i);
            }
        }
    }

    private bool IsLineFilled(int row)
    {
        for (int i = 0; i < _width; i++)
        {
            if (_matrix[i][row] == 0) return false;
        }
        return true;
    }
    
    private void ClearLine(int rowToRemove)
    {
        for (int i = rowToRemove; i < (_height * 2) - 1; i++)
        {
            MoveLineDown(i);
        }
    }

    private void MoveLineDown(int row)
    {
        for (int i = 0; i < _width; i++)
        {
            _matrix[i][row] = _matrix[i][row + 1];
        }
    }

    public void TakeGarbageLine(int column)
    {
        MoveBoardUp();
        FillGarbageLine(column);
    }

    private void FillGarbageLine(int column)
    {
        for (int i = 0; i < _width; i++)
        {
            if (i != column) _matrix[i][0] = (int)PieceType.Garbage;
            else _matrix[i][0] = (int)PieceType.Empty;
        }
    }

    private void MoveBoardUp()
    {
        for (int i = (_height * 2) - 2; i > 0; i--)
        {
            MoveLineUp(i);
        }
    }

    private void MoveLineUp(int row)
    {
        for (int i = 0; i < _width; i++)
        {
            _matrix[i][row] = _matrix[i][row - 1];
        }
    }

    public void ReceiveDamage(int amountOfDamage)
    {
        _damageQueue.AddGarbageToQueue(amountOfDamage);
    }

    public int GetXCenter()
    {
        return (_width / 2) - 1;
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetTop()
    {
        return _height - 1;
    }

    public void GrayOutBoard()
    {
        for (int i = 0; i < _height * 2 - 1; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                if (_matrix[j][i] != (int)PieceType.Empty) _matrix[j][i] = (int)PieceType.Garbage;
            }
        }
    }

    private bool IsPerfectClear()
    {
        for (int i = 0; i < _width; i++)
        {
            if (_matrix[i][0] != (int)PieceType.Empty) return false;
        }

        return true;
    }

    public int GetAmountOfGarbage()
    {
        return _damageQueue.GetQueue().Sum();
    }

    public void Update(long currentTime)
    {
        if (_inAnimation && _lineClearTimer <= currentTime)
        {
            ClearFilledLines();
        }
    }

    public void SetTimer(ITimer timer)
    {
        _timer = timer;
    }
}