using domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ITimer = domain.ITimer;

namespace GUI.Components.Pages;

public partial class Tetris : ComponentBase, IUpdatable
{
    private Game Game { get; set; } = new Game();

    protected async void ButtonReleased(KeyboardEventArgs e)
    {
        Game.HandleKeyRelease(e.Code);
        Game.Renderer.UpdateBoardToRender();
    }

    protected async void ButtonPressed(KeyboardEventArgs e)
    {
        if (e.Repeat) return;
        Game.HandleKeyPress(e.Code);
        Game.Renderer.UpdateBoardToRender();
    }
    
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