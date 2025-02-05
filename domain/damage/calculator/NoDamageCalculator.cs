namespace domain.damage.calculator;

public class NoDamageCalculator : IDamageCalculator
{
    public int CalculateDamage(int linesCleared, SpinType spinType, bool isPerfectClear)
    {
        return 0;
    }

    public int GetCombo()
    {
        return -1;
    }

    public int GetBackToBack()
    {
        return 0;
    }
}