using domain;
using Microsoft.AspNetCore.Components;

namespace GUI.Components.shared;

public partial class OtherPlayer : ComponentBase
{
    [Parameter]
    public Game Game { get; set; }

    public void Update(long currentTime)
    {
        StateHasChanged();
    }
    
}