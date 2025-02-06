namespace domain.versus;

public class VersusQueue
{
    private VersusGame _unfilledGame = new();

    public VersusGame RegisterInGame(IParticipant participant)
    {
        VersusGame versusGame = _unfilledGame;
        versusGame.RegisterForGame(participant);
        ReplaceFilledGame();
        return versusGame;
    }

    private void ReplaceFilledGame()
    {
        if (!_unfilledGame.HasGameStarted()) return;
        _unfilledGame = new VersusGame();
    }
}