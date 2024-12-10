using domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GUI.Components.Pages;

public partial class Tetris : ComponentBase, IScreen
{
    public Game game { get; set; } = new Game();

    protected async Task ButtonReleased(KeyboardEventArgs e)
    {
        game.Input.HandleKeyRelease(e.Code);
        game.Renderer.UpdateBoardToRender();
    }

    protected async Task ButtonPressed(KeyboardEventArgs e)
    {
        if (e.Repeat) return;
        game.Input.HandleKeyPress(e.Code);
        game.Renderer.UpdateBoardToRender();
    }
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            game.GameLoop.StartLoop(this);
        }
    }

    public void Rerender()
    {
        game.Renderer.UpdateBoardToRender();
        StateHasChanged();
    }
}