using domain;
using domain.versus;
using Microsoft.AspNetCore.Components;

namespace GUI.Components.Pages;

public partial class Versus : ComponentBase, IParticipant
{
    private readonly VersusGame _versusGame;
    
    private Game? _localPlayer;
    private Game? _otherPlayer;

    public Versus()
    {
        _versusGame = GlobalVersusQueue.VersusQueue.GetGameToJoin();
        GlobalVersusQueue.VersusQueue.RegisterInGame(this);
    }
    
    public void GameStart(Game participantGame, int playerId)
    {
        _localPlayer = participantGame;
        _otherPlayer = _versusGame.GetOpponent(playerId);
    }
}