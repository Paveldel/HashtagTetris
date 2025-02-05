namespace domain.damage.calculator;

public class MultiplierDamageCalculator : IDamageCalculator
{
    private const int NoCombo = -1;
    private const int NoBackToBack = 0;
    private const int PerfectClearDamage = 10;
    private static readonly double[] LineClearDamage = { 0, 0, 1, 2, 4};
    private static readonly double[] ComboMultiplier = { 0, 0, 0.25, 0.5, 1};
    
    private int _combo = NoCombo;
    private int _backToBack = NoBackToBack;
    
    public int CalculateDamage(int linesCleared, SpinType spinType, bool isPerfectClear)
    {
        int resultDamage = 0;
        if (linesCleared == 0) HandleNoLineClear();
        else resultDamage = HandleLineClear(linesCleared, spinType);
        if (isPerfectClear) resultDamage += PerfectClearDamage;
        return resultDamage;
    }
    
    private void HandleNoLineClear()
    {
        _combo = NoCombo;
    }

    private int HandleLineClear(int linesCleared, SpinType spinType)
    {
        _combo++;
        double resultDamage = GetLineClearDamage(linesCleared, spinType) 
                           + GetComboDamage(linesCleared, spinType)
                           * GetBackToBackMultiplier(linesCleared, spinType);
        return (int)resultDamage;
    }

    private double GetLineClearDamage(int linesCleared, SpinType spinType)
    {
        if (spinType == SpinType.FullSpin) return linesCleared * 2;
        return LineClearDamage[Math.Min(linesCleared, LineClearDamage.Length - 1)];
    }
    
    private double GetComboDamage(int linesCleared, SpinType spinType)
    {
        if (linesCleared == 1) return Math.Log(1 + 1.25 * _combo, Math.E);
        if (spinType == SpinType.FullSpin) return (int)spinType * 0.5 * _combo;
        return ComboMultiplier[Math.Min(linesCleared, ComboMultiplier.Length - 1)] * _combo;
    }

    private double GetBackToBackMultiplier(int linesCleared, SpinType spinType)
    {
        if (IsSpecialClear(linesCleared, spinType))
        {
            _backToBack++;
            return Math.Log(_backToBack * 2.2, Math.E);
        }
        _backToBack = NoBackToBack;
        return 1;
    }

    private bool IsSpecialClear(int linesCleared, SpinType spinType)
    {
        return (linesCleared > 3) || spinType != SpinType.NoSpin;
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