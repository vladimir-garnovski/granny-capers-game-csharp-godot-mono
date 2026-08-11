using Godot;
using System;

public partial class Pumpkin : CharacterBody3D
{
	[Export] private float _speed = 3.0f;
	[Export] private float	_gravity = -20.0f;
	[Export] private float _jumpSpeed = 10.0f;
	[Export] private float _jumpDistance = 10.0f; // distance of the player , so that the pumpkin will jump

	[Export] private Timer _jumpTimer;
	[Export] private Label3D _labelDebug;
	[Export] private LinkPlayer _linkPlayer;

	[Export] private AudioStreamPlayer3D _effect;



	private bool _canJump = true;
	private bool _wasOnFloor = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_jumpTimer.Timeout += OnJumpTimerTimeout;
	}

    private void OnJumpTimerTimeout()
    {
        _canJump = true;
    }

    public override void _ExitTree()
    {
        _jumpTimer.Timeout -= OnJumpTimerTimeout;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (_linkPlayer.Granny == null)
		{
			return;
		}
		var velocity = Velocity;
		velocity.Y += _gravity * (float)delta;
		Velocity = velocity;
		TryJump();
		MoveAndSlide();

		if (_wasOnFloor != IsOnFloor())
		{
			_effect.Play();
			_wasOnFloor = IsOnFloor();
		}
	}
	private void TryJump()
	{
		Vector3 direction = _linkPlayer.DirectionToGranny(this.GlobalPosition);
		Vector3 flatPosition = _linkPlayer.GrannyPosSetY(this.GlobalPosition.Y);
		float distanceToPlayer = GlobalPosition.DistanceTo(flatPosition);

		LookAt(flatPosition, Vector3.Up); // remember the pumpkin model is at the negative z

		if (_canJump && distanceToPlayer < _jumpDistance)
		{
			_canJump = false;
			Vector3 velocity = Velocity;
			velocity.Y = _jumpSpeed;
			velocity.X = direction.X * _speed;
			velocity.Z = direction.Z * _speed;
			Velocity = velocity;
			_jumpTimer.Start();
		}
		else if(IsOnFloor())
		{
			var velocity = Velocity;
			velocity.X = 0.0f;
			velocity.Z = 0.0f;
			Velocity = velocity;
		}
		_labelDebug.Text = $"Direction: {GrannyUtils.FormattedVec3(direction)}\n" +
						   $" Distance: {distanceToPlayer.ToString("F1")} \n"+
						   $"Timer: {_jumpTimer.TimeLeft.ToString("F1")}";

	}
}
