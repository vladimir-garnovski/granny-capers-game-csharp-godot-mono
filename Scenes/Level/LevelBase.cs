using Godot;
using System;

public partial class LevelBase : Node3D
{
    [Export] private AudioStreamPlayer _music;
    public override void _EnterTree()
    {
        SignalHub.Instance.OnLevelCompleted += OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
        _music.Stop();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("exit"))
        {
            GameManager.Instance.ChangeToMain();
        }
    }
}
