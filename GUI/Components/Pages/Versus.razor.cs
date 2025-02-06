using domain;
using domain.timer;
using domain.versus;
using Microsoft.AspNetCore.Components;
using ITimer = domain.timer.ITimer;

namespace GUI.Components.Pages;

public partial class Versus : ComponentBase, IParticipant, IUpdatable
{
    private readonly VersusGame _versusGame;
    
    private Game? _localPlayer;
    private Game? _otherPlayer;
    private ITimer _timer;

    public Versus()
    {
        _versusGame = GlobalVersusQueue.VersusQueue.GetGameToJoin();
        GlobalVersusQueue.VersusQueue.RegisterInGame(this);
        UpdatePage();
    }
    
    private void UpdatePage()
    {
        _timer = new GameLoop();
        _timer.RegisterUpdatable(this);
        _timer.StartLoop();
    }

    public void Update(long currentTime)
    {
        if (_versusGame.HasGameStarted()) _timer.EndTimer();
        StateHasChanged();
    }

    public void SetTimer(ITimer timer) { }
    
    public void GameStart(Game participantGame, int playerId)
    {
        _localPlayer = participantGame;
        _otherPlayer = _versusGame.GetOpponent(playerId);
    }
}