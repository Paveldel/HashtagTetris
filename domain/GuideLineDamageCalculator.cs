namespace domain;

public class GuideLineDamageCalculator : IDamageCalculator
{
    private const int NoCombo = -1;
    private const int NoBackToBack = 0;
    private const int BackToBack = 1;
    private const int PerfectClearDamage = 10;
    private static readonly int[] ComboDamage = { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4, 4, 4, 5};
    private static readonly int[] LineClearDamage = { 0, 0, 1, 2, 4};
    
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
        int resultDamage = GetLineClearDamage(linesCleared, spinType) 
                           + GetComboDamage()
                           + GetBackToBackDamage(linesCleared, spinType);
        return resultDamage;
    }

    private int GetLineClearDamage(int linesCleared, SpinType spinType)
    {
        if (spinType == SpinType.FullSpin) return linesCleared * 2;
        return LineClearDamage[Math.Min(linesCleared, LineClearDamage.Length - 1)];
    }
    
    private int GetComboDamage()
    {
        return ComboDamage[Math.Min(_combo, ComboDamage.Length - 1)];
    }

    private int GetBackToBackDamage(int linesCleared, SpinType spinType)
    {
        if (IsSpecialClear(linesCleared, spinType))
        {
            int hadBackToBack = _backToBack;
            _backToBack = BackToBack;
            return hadBackToBack;
        }
        _backToBack = NoBackToBack;
        return NoBackToBack;
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