using Godot;
using System;

public partial class BounceButton : Node3D
{
    [Export] AudioStreamPlayer3D _Effect;
    [Export] AnimationPlayer _animationPlayer;
    [Export] float _bounceSpeed = 2.0f;
    [Export] Area3D _detectionArea;

    public override void _Ready()
    {
        _detectionArea.BodyEntered += OnBodyEntered;
    }
    public override void _ExitTree()
    {
        _detectionArea.BodyEntered -= OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if(body is not Granny)
            return;
            
        _Effect.Play();
        _animationPlayer.Play("toggle");
        SignalHub.Instance.EmitOnPlayerBounce(_bounceSpeed);
    }
}
