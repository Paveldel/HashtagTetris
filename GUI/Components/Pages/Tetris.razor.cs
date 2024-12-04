using domain;
using Microsoft.AspNetCore.Components;

namespace GUI.Components.Pages;

public partial class Tetris : ComponentBase
{
    public Game game { get; set; }
}