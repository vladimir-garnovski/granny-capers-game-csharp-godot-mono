using Godot;
using System;

public partial class VampireModel : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export] private AnimationPlayer _animationPlayer;

	public void PlayWalk()
	{
		_animationPlayer.Play("walk");
	}
}
