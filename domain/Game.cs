namespace domain;

public class Game
{
    public Board Board { get; set; }
    public ActivePiece PlayerPiece { get; set; }
    public BoardRenderer Renderer { get; set; }
    public Input Input { get; set; }

    public Game()
    {
        this.Board = new Board();
        this.PlayerPiece = new ActivePiece(this.Board);
        this.Renderer = new BoardRenderer(this.Board, this.PlayerPiece);
        this.Input = new Input(this.PlayerPiece);
    }
}