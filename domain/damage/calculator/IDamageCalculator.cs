namespace domain.damage.calculator;

public interface IDamageCalculator
{
    public int CalculateDamage(int linesCleared, SpinType spinType, bool isPerfectClear);
    public int GetCombo();
    public int GetBackToBack();
}