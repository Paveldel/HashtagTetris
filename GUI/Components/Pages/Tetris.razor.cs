﻿using domain;
using domain.factory;
using domain.timer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ITimer = domain.timer.ITimer;

namespace GUI.Components.Pages;

public partial class Tetris : ComponentBase, IUpdatable
{
    public Tetris()
    {
        Config configuration = new Config();
        Game = new GameFactory(configuration).CreateGame();
    }

    private Game Game { get; set; }

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