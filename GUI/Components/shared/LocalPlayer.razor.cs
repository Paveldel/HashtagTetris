﻿using domain;
using domain.timer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ITimer = domain.timer.ITimer;

namespace GUI.Components.shared;

public partial class LocalPlayer : ComponentBase, IUpdatable
{
    [Parameter]
    public Game Game { get; set; }
    
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
        InvokeAsync(StateHasChanged);
    }

    public void SetTimer(ITimer timer) { }
    
}