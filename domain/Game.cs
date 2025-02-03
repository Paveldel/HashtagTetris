namespace domain;

public class Game
{
    public BoardRenderer Renderer { get; }
    
    private readonly Board _board;
    private readonly Gravity _gravity;
    private readonly ActivePiece _playerPiece;
    private readonly Input _input;
    private readonly GameLoop _gameLoop;

    public Game()
    {
        this._board = new Board();
        this._gravity = new Gravity();
        this._playerPiece = new ActivePiece(this._board, this._gravity, new SRSPieceData());
        this.Renderer = new BoardRenderer(this._board, this._playerPiece);
        this._input = new Input(this._playerPiece);
        this._gameLoop = new GameLoop();
        _gameLoop.RegisterUpdatable(this._input, this._gravity, _playerPiece);
        _playerPiece.Start(1000);
    }

    public void HandleKeyPress(string keyCode)
    {
        _input.HandleKeyPress(keyCode);
    }

    public void HandleKeyRelease(string keyCode)
    {
        _input.HandleKeyRelease(keyCode);
    }

    public void StartGame(IUpdatable screen)
    {
        _gameLoop.RegisterUpdatable(screen);
        _ = _gameLoop.StartLoop();
    }
}