using domain.damage.calculator;
using domain.damage.queue;
using domain.hold;
using domain.Input;
using domain.piecedata;
using domain.queues;
using domain.rotationsystem;
using domain.spindetectors;
using domain.timer;
using ITimer = domain.timer.ITimer;

namespace domain.factory;

public class GameFactory(Config config)
{
    private Board _board;
    private Gravity _gravity;
    private ActivePiece _playerPiece;
    private IPieceData _pieceData;
    private BoardRenderer _renderer;
    private Input.Input _input;
    private ITimer _gameLoop;

    public Game CreateSingleGame()
    {
        _gameLoop = new GameLoop();
        return CreateGame();
    }
    
    public Game CreateMultiPlayerGame(MasterTimer masterTimer)
    {
        _gameLoop = masterTimer.GetChildTimer();
        return CreateGame();
    }

    private Game CreateGame()
    {
        CreateBoard();
        CreateActivePiece();
        _renderer = new BoardRenderer(_board, _playerPiece, config.AmountOfPreviews, config.ShowShadowPiece);
        _input = new Input.Input(_playerPiece);
        _gameLoop.RegisterUpdatable(_input, _gravity, _playerPiece, _board);
        _playerPiece.Start(config.StartingDelay);
        return new Game(_renderer, _input, _gameLoop);
    }

    private void CreateActivePiece()
    {
        _gravity = new Gravity(config.Gravity, config.Resets);
        GetPieceData();
        _playerPiece = new ActivePiece(_board, _gravity,
            GetHold(), GetPieceQueue(),
            GetRotationSystem(), GetSpinDetector(), config.AppearanceDelay);
    }

    private ISpinDetector GetSpinDetector()
    {
        return config.SpinDetector switch
        {
            1 => new FourCorner(_board),
            2 => new Immobile(_board),
            3 => new SurgeSpinDetector(_board),
            4 => new NeverSpin(),
            5 => new AlwaysSpin(),
            _ => new OnlyT(_board)
        };
    }

    private IRotationSystem GetRotationSystem()
    {
        return config.RotationSystem switch
        {
            1 => new SRS(_board),
            2 => new SRSX(_board),
            3 => new NoKicks(_board),
            4 => new ARS(_board),
            5 => new ASCRotationSystem(_board),
            _ => new SRSPlus(_board)
        };
    }

    private IPieceQueue GetPieceQueue()
    {
        return config.PieceQueue switch
        {
            1 => new RandomQueue(_pieceData, new Random(config.Seed)),
            2 => new ClassicQueue(_pieceData, new Random(config.Seed)),
            3 => new FourteenBag(_pieceData, new Random(config.Seed)),
            4 => new PredefinedQueue(_pieceData, config.PiecesInQueue!),
            _ => new SevenBag(_pieceData, new Random(config.Seed))
        };
    }

    private IHold GetHold()
    {
        return config.HoldType switch
        {
            1 => new NeverHold(),
            2 => new AlwaysHold(_pieceData),
            _ => new Hold(_pieceData)
        };
    }

    private void GetPieceData()
    {
        _pieceData = config.PieceData switch
        {
            1 => new MiniPieceData(),
            2 => new PentominoData(),
            3 => new ARSPieceData(),
            4 => new NESPieceData(),
            5 => new ASCPieceData(),
            _ => new SRSPieceData()
        };
    }

    private void CreateBoard()
    {
        _board = new Board(config.BoardWidth, config.BoardHeight, config.LineClearDelay);
        _board.SetDamageCalculator(GetDamageCalculator());
        _board.SetDamageQueue(GetDamageQueue());
    }

    private IDamageQueue GetDamageQueue()
    {
        IDamageQueue queue;
        switch (config.DamageQueue)
        {
            case 1:
                queue = new DelayedDamageQueue(_board, new Random(config.Seed), config.DamageDelay, config.AllowCanceling, config.AllowBlocking);
                _gameLoop.RegisterUpdatable((DelayedDamageQueue)queue);
                break;
            case 2:
                queue = new GuidelineDamageQueue(_board, new Random(config.Seed), config.AllowCanceling, config.AllowBlocking);
                break;
            case 3:
                queue = new FakeDamageQueue();
                break;
            default:
                queue = new DamageQueue(_board, new Random(config.Seed), config.AllowCanceling, config.AllowBlocking);
                break;
        }

        if (Math.Abs(config.DamageSentMultiplier - 1.0) > 0.01)
            queue = new HandicappedDamageQueue(queue, config.DamageSentMultiplier);
        return queue;
    }

    private IDamageCalculator GetDamageCalculator()
    {
        IDamageCalculator calculator = config.DamageCalculator switch
        {
            1 => new MultiplierDamageCalculator(),
            2 => new NoDamageCalculator(),
            _ => new GuideLineDamageCalculator()
        };

        if (Math.Abs(config.DamageTakenMultiplier - 1.0) > 0.01)
            calculator = new HandicappedCalculator(calculator, config.DamageTakenMultiplier);
        return calculator;
    }
}