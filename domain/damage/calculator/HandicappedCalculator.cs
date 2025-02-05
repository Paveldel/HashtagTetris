namespace domain.damage.calculator;

public class HandicappedCalculator(IDamageCalculator original, double multiplier) : IDamageCalculator
{
    private double _leftOvers;

    public int CalculateDamage(int linesCleared, SpinType spinType, bool isPerfectClear)
    {
        int damage = original.CalculateDamage(linesCleared, spinType, isPerfectClear);
        double multipliedDamage = damage * multiplier;
        multipliedDamage += _leftOvers;
        _leftOvers = multipliedDamage % 1;
        return (int)Math.Floor(multipliedDamage);
    }

    public int GetCombo()
    {
        return original.GetCombo();
    }

    public int GetBackToBack()
    {
        return original.GetBackToBack();
    }
}