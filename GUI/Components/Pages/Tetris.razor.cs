using domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GUI.Components.Pages;

public partial class Tetris : ComponentBase
{
    public Game game { get; set; } = new Game();

    protected async Task KeywordEnterPressed(KeyboardEventArgs e)
    {
        if (e.Code == "Numpad1")
        {
            game.PlayerPiece.MoveLeft();
        }
        else if (e.Code == "Numpad2")
        {
            game.PlayerPiece.MoveDown();
        }
        else if (e.Code == "Numpad3")
        {
            game.PlayerPiece.MoveRight();
        }
        else if (e.Code == "Space")
        {
            game.PlayerPiece.HardDrop();
        }
        else if (e.Code == "KeyW")
        {
            game.PlayerPiece.Rotate(Rotation.ANTI_CLOCKWISE);
        }
        else if (e.Code == "KeyE")
        {
            game.PlayerPiece.Rotate(Rotation.REVERSE);
        }
        else if (e.Code == "KeyR")
        {
            game.PlayerPiece.Rotate(Rotation.CLOCKWISE);
        }

        game.Renderer.UpdateBoardToRender();
    }
}