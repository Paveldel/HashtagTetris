using domain;
using domain.factory;
using Microsoft.AspNetCore.Components;

namespace GUI.Components.Pages;

public partial class Tetris : ComponentBase
{
    
    public Game GetTetrisGame()
    {
        Config configuration = new Config();
        return new GameFactory(configuration).CreateSingleGame();
    }
}