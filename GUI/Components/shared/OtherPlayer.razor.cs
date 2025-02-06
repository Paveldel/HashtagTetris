using domain;
using domain.timer;
using Microsoft.AspNetCore.Components;
using ITimer = domain.timer.ITimer;

namespace GUI.Components.shared;

public partial class OtherPlayer : ComponentBase, IUpdatable
{
    [Parameter]
    public Game Game { get; set; }
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Game.StartGame(this);
        }
    }

    public void Update(long currentTime)
    {
        Game.Renderer.UpdateBoardToRender();
        StateHasChanged();
    }

    public void SetTimer(ITimer timer) { }
    
}