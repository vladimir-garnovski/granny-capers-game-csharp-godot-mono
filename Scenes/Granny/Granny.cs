using Godot;
using System;


public partial class Granny : CharacterBody3D
{
	public const string GROUP_NAME = "Granny";

	[Export] private Label3D _label;
	[Export] private float _gravity = -70.0f;
	[Export] private float _runSpeed = 6.0f;
	[Export] private float _rotationSpeed = 2.7f;
	[Export] private float _jumpVelocity = 40.0f;
	[Export] private float _doubleJumpVelocity = 25.0f;
	[Export] private float _airControlFactor = 0.7f;
	


	private const string GROUNDED = "parameters/Grounded/playback";
	[Export] private AnimationTree _animationTree;
	AnimationNodeStateMachinePlayback _treeSmGrounded;

	private PackedScene FIRE_BALL_SCN = GD.Load<PackedScene>("res://Scenes/FireBall/FireBall.tscn");
	[Export] private float _shootSpeed = 10.0f;
	[Export] private float _shootVerticalSpeed = 3.0f;
	[Export] private BoneAttachment3D _boneAttachment;

	private bool _throwing = false;
	public bool Throwing
	{
		get
		{
			return _throwing;
		}
	}
	private bool _canDoubleJump = false; 

	// For state machine
	private bool _isMoving = false;
	private bool _isOnFloor;
    public override void _EnterTree()
    {
        AddToGroup(GROUP_NAME);
    }
	private void HandleShoot()
	{
		if (Input.IsActionJustPressed("shoot") && !_throwing && IsOnFloor())
		{
			_throwing = true;
			_treeSmGrounded.Travel("Throw");
			Velocity = Vector3.Zero;
		}
	}
	public void CreateFireball()
	{
		FireBall fb = (FireBall)FIRE_BALL_SCN.Instantiate();
		fb.Setup(_shootSpeed, GlobalTransform.Basis.Z,_shootVerticalSpeed); //GlobalTransform.Basis.Z -> where the grannyt is facing
		SignalHub.Instance.EmitOnAddNewScene(fb, _boneAttachment.GlobalPosition);
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animationTree.AnimationFinished += OnAnimationFinished;
		_treeSmGrounded = (AnimationNodeStateMachinePlayback)_animationTree.Get(GROUNDED); // Gets hold of the state machine

	}
	public override void _ExitTree()
	{
		_animationTree.AnimationFinished -= OnAnimationFinished;
	}

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "Throw")
		{
			_throwing = false;
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		UpdateDebug();
		ApplyGravity((float)delta);
		HandleInput((float)delta);
		MoveAndSlide();

		// Godot Mono Workarounds
		_isOnFloor = IsOnFloor();
	}
	private void UpdateDebug()
	{
		string debugString = $"Floor:{IsOnFloor().ToString()} \n";
		debugString		  += $"Vel:{GrannyUtils.FormattedVec3(Velocity)} \n";
		debugString		  += $"Pos:{GrannyUtils.FormattedVec3(GlobalPosition)}";
							 
		_label.Text = debugString;
	}
	private void ApplyGravity(float delta)
	{
		Vector3 velocity = Velocity;
		velocity.Y += _gravity * delta;
		Velocity = velocity;
	}
	private void HandleInput(float delta)
	{
		if (_throwing) 
			return;
		bool rotated = HandleRotation(delta);
		bool moved =  HandleMovement();
		_isMoving = rotated || moved;

		HandleShoot();
		HandleJump();
		
	}
	private bool HandleMovement()
	{
		float input = Input.GetAxis("move_backward","move_forward");
		Vector3 velocity = Velocity;
		if (Mathf.IsEqualApprox(input,0.0))
		{
			velocity.X = 0.0f;
			velocity.Z = 0.0f;
			Velocity = velocity;
			return false;
		}
		Vector3 direction = Transform.Basis.Z * input; // Because the model is facing the positive Z | already normalised
		float speed = IsOnFloor() ? _runSpeed : _runSpeed * _airControlFactor;

		velocity.X = direction.X * speed;
		velocity.Z = direction.Z * speed;
		Velocity = velocity;
		return true;
		
	}
	private bool HandleRotation(float delta)
	{
		float input = Input.GetAxis("move_right","move_left");
		Vector3 rotation = Rotation;
		rotation.Y += input * _rotationSpeed * delta;
		Rotation = rotation;
		return !Mathf.IsEqualApprox(input,0.0);
	}
	private void HandleJump()
	{
		if (_throwing)
			return;
		if (Input.IsActionJustPressed("jump"))
		{
			Vector3 velocity = Velocity;

			if (IsOnFloor())
			{
				velocity.Y = _jumpVelocity;
				
				_canDoubleJump = true;
			} else if(_canDoubleJump && Velocity.Y > 0)
			{
				_canDoubleJump = false;
				velocity.Y += _doubleJumpVelocity;
			}
			Velocity = velocity;
		}
	}
}
