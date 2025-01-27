namespace domain;

public class GuideLineDamageCalculator : IDamageCalculator
{
    private const int NoCombo = -1;
    private const int NoBackToBack = 0;
    private const int BackToBack = 1;
    private const int PerfectClearDamage = 10;
    private static readonly int[] ComboDamage = new[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4, 4, 4, 5};
    private static readonly int[] LineClearDamage = new[] { 0, 0, 1, 2, 4};
    
    private int _combo = NoCombo;
    private int _backToBack = NoBackToBack;
    
    public int CalculateDamage(int linesCleared, SpinType spinType, bool isPerfectClear)
    {
        int amountOfDamage = GetLineClearDamage(linesCleared, spinType);
        UpdateCombo(linesCleared);
        amountOfDamage += ComboDamage[Math.Min(_combo, ComboDamage.Length - 1)];
        amountOfDamage += _backToBack;
        UpdateBackToBack(linesCleared, spinType);
        if (isPerfectClear) amountOfDamage += PerfectClearDamage;
        return amountOfDamage;
    }

    private int GetLineClearDamage(int linesCleared, SpinType spinType)
    {
        if (spinType == SpinType.FullSpin) return linesCleared * 2;
        return LineClearDamage[Math.Min(linesCleared, LineClearDamage.Length - 1)];
    }

    private void UpdateBackToBack(int linesCleared, SpinType spinType)
    {
        if (linesCleared == 0) return;
        if (linesCleared == 4 || spinType == SpinType.MiniSpin || spinType == SpinType.FullSpin)
            _backToBack = BackToBack;
        else _backToBack = NoBackToBack;
    }

    private void UpdateCombo(int linesCleared)
    {
        if (linesCleared == 0) _combo = NoCombo;
        else _combo++;
    }

    public int GetCombo()
    {
        return _combo;
    }

    public int GetBackToBack()
    {
        return _backToBack;
    }
}