namespace domain.factory;

public class Config
{
    public int Seed { get; set; } = Guid.NewGuid().GetHashCode();
    public int BoardWidth { get; set; } = 10;
    public int BoardHeight { get; set; } = 20;
    public long LineClearDelay { get; set; } = 0;
    public int DamageCalculator { get; set; } = 0;
    public double DamageSentMultiplier { get; set; } = 1.0;
    public int DamageQueue { get; set; } = 0;
    public long DamageDelay { get; set; } = 1000;
    public bool AllowCanceling { get; set; } = true;
    public bool AllowBlocking { get; set; } = true;
    public double DamageTakenMultiplier { get; set; } = 1;
    public int HoldType { get; set; } = 0;
    public long AppearanceDelay { get; set; } = 0;
    public int PieceQueue { get; set; } = 0;
    public string? PiecesInQueue { get; set; }
    public int RotationSystem { get; set; } = 0;
    public int PieceData { get; set; } = 0;
    public int SpinDetector { get; set; } = 0;
    public long StartingDelay { get; set; } = 2000;
    public long Gravity { get; set; } = 500;
    public int Resets { get; set; } = 10;
    public int AmountOfPreviews { get; set; } = 5;
    public bool ShowShadowPiece { get; set; } = true;
}