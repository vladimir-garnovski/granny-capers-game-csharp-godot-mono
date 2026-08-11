using Godot;
using System;

public partial class Turret : Node3D
{
	[Export] private Area3D _playerDetect;
	[Export] private Timer _shootTimer;
	[Export] private Node3D _pivot;
	[Export] private LinkPlayer _linkPlayer;
	[Export] private AudioStreamPlayer3D _effects;
	[Export] private Marker3D _shootPoint;
	private readonly PackedScene TURRET_BULLET = GD.Load<PackedScene>("res://Scenes/Turret/TurretBullet.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_shootTimer.Timeout += Shoot;
		_playerDetect.BodyEntered += OnBodyEntered;
		_playerDetect.BodyExited += OnBodyExited;
	}

    private void OnBodyExited(Node3D body)
    {
        if (body is Granny)
		{
			_shootTimer.Stop();
		}
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body is Granny)
		{
			_shootTimer.Start();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		RotateIfTimerRunning();
	}
	private void Shoot()
	{
		TurretBullet newScene = TURRET_BULLET.Instantiate<TurretBullet>();
		SignalHub.Instance.EmitOnAddNewScene(newScene, _shootPoint.GlobalPosition);
		newScene.SetTargetDeferred(_linkPlayer.GrannyPos);
		_effects.Play();
	}
	private void RotateIfTimerRunning()
	{
		if (!_shootTimer.IsStopped() && _linkPlayer.Granny != null) // Timer is running
		{
			Vector3 flatPosition = _linkPlayer.GrannyPosSetY(this.GlobalPosition.Y);
			_pivot.LookAt(flatPosition, Vector3.Up);

		}
	}
}
