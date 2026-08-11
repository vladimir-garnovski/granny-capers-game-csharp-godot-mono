using Godot;
using System;

public partial class VampireCharacter : CharacterBody3D
{
	[Export] private Timer _runningTimer;
	[Export] private AudioStreamPlayer3D _runningEffect;
	[Export] private float _gravity = 20.0f;
	[Export] private LinkPlayer _linkPlayer;
	[Export] private VampireModel _vampireModel;
	[Export] private float _runningSpeed = 3f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_vampireModel.PlayWalk();
		_runningTimer.Timeout += OnRunningTimerTimeout;
	}

    private void OnRunningTimerTimeout()
    {
        _runningEffect.Stop();
		_runningEffect.Play();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		HandleGravity((float)delta);
		ChasePlayer((float)delta);
		MoveAndSlide();
	}
	private void HandleGravity(float delta)
	{
		var velocity = Velocity;
		velocity.Y -= _gravity * delta;
		Velocity = velocity;
		//Velocity = new Vector3(Velocity.X, velocity.Y, Velocity.Z);
	}
	private void ChasePlayer(float delta)
	{
		if (_linkPlayer.Granny != null && !_linkPlayer.GrannyTooCloseIgnoreY(GlobalPosition))
		{
			Vector3 flatPosition = _linkPlayer.GrannyPosSetY(this.GlobalPosition.Y);
			LookAt(flatPosition, Vector3.Up);

			var velocity = Velocity;
			
			velocity.X = _linkPlayer.DirectionToGranny(GlobalPosition).X  * _runningSpeed;
			velocity.Z = _linkPlayer.DirectionToGranny(GlobalPosition).Z  * _runningSpeed;

			Velocity = velocity;
			//Velocity = new Vector3(velocity.X, Velocity.Y, velocity.Z);
			
		}
		
	}
}
