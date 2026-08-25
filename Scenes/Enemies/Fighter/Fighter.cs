using Godot;
using System;
using System.Collections.Generic;

public partial class Fighter : CharacterBody3D
{
	[Export] private float _gravity = 70.0f;
	[Export] private LinkPlayer _linkPlayer;
	// Called when the node enters the scene tree for the first time.
	[Export] private float _chaseSpeed = 2.0f;
	bool _chasePlayer = true;
	bool _closeEnough = false;
	bool _justJumped = false;
	// For animation tree
	bool _isWalking = false;

	[Export] Timer _flipTimer;

	bool _timePassedFromJump = false; //
	[Export] Timer _tinyAfterJumpTimer;
	public override void _Ready()
	{
		_flipTimer.Timeout += OnFlipTimerEnd;
		_tinyAfterJumpTimer.Timeout += EnableLandingHandler;
	}

    private void EnableLandingHandler()
    {
        _timePassedFromJump = true;
    }

    public override void _ExitTree()
	{
		_flipTimer.Timeout -= OnFlipTimerEnd;
	}

    private void OnFlipTimerEnd()
    {
		GD.Print("Jump!");
        Jump();


    }
	private void Jump ()
	{
		if (!IsOnFloor())
			return;

		var velocity = Velocity;
		
		Vector3 originalDirectionXZ = new Vector3(velocity.X/1.1f,0,velocity.Z/1.1f);

		if (Random.Shared.Next(2) == 0)
			velocity = new Vector3(-velocity.Z, velocity.Y, velocity.X) + originalDirectionXZ;
		else
			velocity = new Vector3(velocity.Z, velocity.Y, -velocity.X) + originalDirectionXZ;
		
		velocity.Y = 1000.0f * (float)GetPhysicsProcessDeltaTime();
		Velocity = velocity;
		_chasePlayer = false;
		_justJumped = true;
		_tinyAfterJumpTimer.Start();
	}
	private void LandingAfterJumpHandler()
	{
		if (_justJumped && IsOnFloor() && _timePassedFromJump)
		{

			GD.Print("Landing Handler!!");
			Velocity = new Vector3(0,Velocity.Y,0);
			_justJumped = false;
			_chasePlayer = true;
			_timePassedFromJump = false;
		}
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		HandleGravity((float)delta);
		ChasePlayer();
		LookAtPlayer();
		LandingAfterJumpHandler();
		MoveAndSlide();

		

		HandleAnimationTreeParams();
	}
	private void HandleGravity(float delta)
	{
		var velocity = Velocity;
		velocity.Y -= _gravity * delta;
		Velocity = velocity;
	}
	private void ChasePlayer()
	{
		if (!_chasePlayer) 
			return;
		var chaseDirection = _linkPlayer.DirectionToGranny(GlobalPosition);
		Vector3 velocity = Velocity;
		velocity.X = chaseDirection.X * _chaseSpeed;
		velocity.Z = chaseDirection.Z * _chaseSpeed;
		Velocity = velocity;	
	}
	private void LookAtPlayer()
	{
		if (!_linkPlayer.GrannyTooCloseIgnoreY(GlobalPosition))
			LookAt(_linkPlayer.GrannyPosSetY(GlobalPosition.Y), Vector3.Up); // remember the pumpkin model is at the negative z

	}
	private void HandleAnimationTreeParams()
	{
		
		if (Mathf.IsZeroApprox(Velocity.X) && Mathf.IsZeroApprox(Velocity.Z) ) 
			_isWalking = false;
		else
			_isWalking = true;	
	}
}
