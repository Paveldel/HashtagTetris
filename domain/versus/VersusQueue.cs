namespace domain.versus;

public class VersusQueue
{
    private VersusGame _unfilledGame = new();

    public VersusGame GetGameToJoin()
    {
        return _unfilledGame;
    }

    public void RegisterInGame(IParticipant participant)
    {
        VersusGame versusGame = _unfilledGame;
        versusGame.RegisterForGame(participant);
        ReplaceFilledGame();
    }

    private void ReplaceFilledGame()
    {
        if (!_unfilledGame.HasGameStarted()) return;
        _unfilledGame = new VersusGame();
    }
}