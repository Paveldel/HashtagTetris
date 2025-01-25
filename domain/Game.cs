namespace domain;

public class Game
{
    public Board Board { get; set; }
    public Gravity Gravity { get; set; }
    public ActivePiece PlayerPiece { get; set; }
    public BoardRenderer Renderer { get; set; }
    public Input Input { get; set; }
    public GameLoop GameLoop { get; set; }

    public Game()
    {
        this.Board = new Board();
        this.Gravity = new Gravity();
        this.PlayerPiece = new ActivePiece(this.Board, this.Gravity);
        this.Gravity.SetActivePiece(this.PlayerPiece);
        this.Renderer = new BoardRenderer(this.Board, this.PlayerPiece);
        this.Input = new Input(this.PlayerPiece);
        this.GameLoop = new GameLoop(this.Input, this.Gravity);
    }
}