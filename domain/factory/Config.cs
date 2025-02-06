namespace domain.factory;

public class Config
{
    public int DamageCalculator { get; set; } = 0;
    public double DamageSentMultiplier { get; set; } = 1;
    public int DamageQueue { get; set; } = 0;
    public double DamageTakenMultiplier { get; set; } = 1;
    public int HoldType { get; set; } = 0;
    public int AmountOfPreviews { get; set; } = 5;
    public long AppearanceDelay { get; set; } = 0;
    public long LineClearDelay { get; set; } = 0;
    public int PieceQueue { get; set; } = 0;
    public int RotationSystem { get; set; } = 0;
    public int PieceData { get; set; } = 0;
    public int SpinDetector { get; set; } = 0;
    public int Gravity { get; set; } = 500;
    public int Resets { get; set; } = 10;
    public int BoardWidth { get; set; } = 10;
    public int PieceHeight { get; set; } = 20;
    public bool ShowShadowPiece { get; set; } = true;
}