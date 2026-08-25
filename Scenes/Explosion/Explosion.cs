using Godot;
using System;
using System.Threading.Tasks;

public partial class Explosion : Node3D
{
	[Export] private AnimationPlayer _animationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationPlayer.AnimationFinished += OnAnimationFinished;
	}

    private void OnAnimationFinished(StringName animName)
    {
        QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _ExitTree()
	{
		_animationPlayer.AnimationFinished -= OnAnimationFinished;
	}
}
