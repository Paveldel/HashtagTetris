namespace domain.damage.queue;

public class DamageQueue(Board board, Random random, bool allowCanceling = true, bool allowBlocking = true)
    : AbstractDamageQueue(board, random, allowCanceling,
        allowBlocking)
{
    private const double DefaultMessinessOnChange = 1;
    private const double DefaultMessinessInBatch = 0;
    
    private double _messinessOnChange = DefaultMessinessOnChange;
    private double _messinessInBatch = DefaultMessinessInBatch;

    public void SetMessinessOnChange(double messiness)
    {
        _messinessOnChange = messiness;
    }
    
    public void SetMessinessInBatch(double messiness)
    {
        _messinessInBatch = messiness;
    }

    protected override void AfterTakeLine()
    {
        if (_messinessInBatch >= Random.NextDouble()) RandomiseWell();
    }
    
    protected override void AfterTakeBatch()
    {
        if (_messinessOnChange >= Random.NextDouble())  RandomiseWell();
    }
}