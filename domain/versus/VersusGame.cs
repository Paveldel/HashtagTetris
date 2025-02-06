using domain.factory;
using domain.timer;

namespace domain.versus;

public class VersusGame
{
    private IParticipant? _playerOne;
    private IParticipant? _playerTwo;

    private Game? _gameOne;
    private Game? _gameTwo;
    
    public void RegisterForGame(IParticipant participant)
    {
        if (_playerOne == null) _playerOne = participant;
        else if (_playerTwo == null)
        {
            _playerTwo = participant;
            StartGame();
        }
    }

    private void StartGame()
    {
        MasterTimer timer = new MasterTimer();
        GameFactory factory = new GameFactory(GetVersusConfig());
        _gameOne = factory.CreateMultiPlayerGame(timer);
        _gameTwo = factory.CreateMultiPlayerGame(timer);
        _playerOne!.GameStart(_gameOne, 1);
        _playerTwo!.GameStart(_gameTwo, 2);
    }

    private static Config GetVersusConfig()
    {
        return new Config();
    }
}