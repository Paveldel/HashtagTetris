namespace domain.damage.queue;

public class GuidelineDamageQueue(Board board, bool allowCanceling = true, bool allowBlocking = true)
    : AbstractGarbageQueue(board, allowCanceling, allowBlocking)
{
    private const int LinesBeforeSwitch = 8;
    
    private int _linesUntilSwitch = LinesBeforeSwitch;
    
    protected override void AfterTakeLine()
    {
        _linesUntilSwitch--;
        if (_linesUntilSwitch == 0)
        {
            RandomiseWell();
            _linesUntilSwitch = LinesBeforeSwitch;
        }
    }

    protected override void AfterTakeBatch()
    {
        return;
    }
}