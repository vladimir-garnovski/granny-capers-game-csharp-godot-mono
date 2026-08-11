using Godot;
using System;

public partial class Ghost : Node3D
{
	[Export] private VisibleOnScreenNotifier3D _screenNotifier;
	[Export] private AudioStreamPlayer3D _effects;
	[Export] private LinkPlayer _linkPlayer;
	[Export] private float _chaseDistance = 10.0f;
	[Export] private float _speed = 3.0f;

	[Export] private float _frozenLimit = 3.0f;
	[Export] private float _teleportRadius = 10.0f;

	[Export] private Label3D _labelDebug ;


	private AudioStream EXIT      = GD.Load<AudioStream>("res://Assets/Audio/Enemies/exit.wav");
    private AudioStream GHOST 	  = GD.Load<AudioStream>("res://Assets/Audio/Enemies/ghost.wav");

	private bool _onScreen = false;

	bool _closeEnough = false;
	bool _shouldChase = false;

	float _frozenTimer =  0.0f;

	public override void _Ready()
	{
		_screenNotifier.ScreenEntered += OnScreenNotifierEntered;
		_screenNotifier.ScreenExited += OnScreenNotifierExited;

	}

    private void OnScreenNotifierExited()
    {
        _onScreen = false;
    }

    private void OnScreenNotifierEntered()
    {
		_onScreen = true;
		GrannyUtils.PlayClip(_effects, GHOST);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		if (_linkPlayer.Granny == null)
		{
			return;
		}
		
		Vector3 direction = _linkPlayer.DirectionToGranny(this.GlobalPosition);
		Vector3 flatPosition = _linkPlayer.GrannyPosSetY(this.GlobalPosition.Y);
		float distanceToPlayer = GlobalPosition.DistanceTo(flatPosition);
		
		if (!_linkPlayer.GrannyTooCloseIgnoreY(GlobalPosition))
			LookAt(_linkPlayer.GrannyPosSetY(GlobalPosition.Y), Vector3.Up); // remember the pumpkin model is at the negative z

		IsCloseEnough(distanceToPlayer);
		ShouldChase();

		if (_shouldChase)
		{
			_frozenTimer = 0.0f;
			Position += new Vector3(
									direction.X * _speed * (float)delta,
									0,
									direction.Z * _speed * (float)delta
			);
		} else // if we're not able to chase, we are frozen
		{
			_frozenTimer += (float)delta;
			if (_frozenTimer >= _frozenLimit)
			{
				Teleport();
			}

		}
		_labelDebug.Text = $"Timer: {_frozenTimer.ToString("F1")}\n" +
						   $"Distance:{distanceToPlayer.ToString("F1")}"+
						   $"OnScreen:{_onScreen}\n" +
						   $"ShouldChase:{_shouldChase}";
		
	}
	private void Teleport()
	{
		Vector3 randomOffset = new Vector3(

			(float)GD.RandRange((float)-_teleportRadius,(float)_teleportRadius),
			0,
			(float)GD.RandRange((float)-_teleportRadius,(float)_teleportRadius)
		);
		GlobalPosition += randomOffset;
		_frozenTimer = 0.0f;
		GrannyUtils.PlayClip(_effects, EXIT);

	}
	private bool IsCloseEnough(float distanceToPlayer)
	{
		if (distanceToPlayer < _chaseDistance)
		{
			_closeEnough = true;

		}else
		{
			_closeEnough = false;
		}
		return _closeEnough;
		
	}
	private bool ShouldChase() // not onscreen OR close enough
	{
		if (!_onScreen || _closeEnough)
		{
			_shouldChase = true;
		} else
		{
			_shouldChase = false;
		}
		return _shouldChase;
	}
	public override void _ExitTree()
	{
		_screenNotifier.ScreenEntered -= OnScreenNotifierEntered;
		_screenNotifier.ScreenExited -= OnScreenNotifierExited;

	}
}
