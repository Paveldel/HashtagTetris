using domain;
using domain.timer;
using Microsoft.AspNetCore.Components;
using ITimer = domain.timer.ITimer;

namespace GUI.Components.shared;

public partial class OtherPlayer : ComponentBase, IUpdatable
{
    [Parameter]
    public Game Game { get; set; }

    public void Update(long currentTime)
    {
        StateHasChanged();
    }
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            UpdateBoard();
        }
    }

    private void UpdateBoard()
    {
        ITimer timer = new GameLoop();
        timer.RegisterUpdatable(this);
        timer.StartLoop();
    }

    public void SetTimer(ITimer timer) { }
}