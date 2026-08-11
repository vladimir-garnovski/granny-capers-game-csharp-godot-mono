using Godot;
using System;

public partial class Main : Control
{
    private bool _canPress = false;

    public override void _Ready()
    {
        GetTree().Paused = false;
    }
    public void SetPressOn()
    {
        _canPress = true;
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if (_canPress && Input.IsActionJustPressed("shoot"))
        {
            GameManager.Instance.LoadNextLevel();
        }
    }
}
