using Godot;
using System;

public partial class VampireCoffin : Node3D
{
	private readonly Vector3 OFFSET_Y = new Vector3(0,1,0);

	[Export] private AnimationPlayer _animationPlayer;
	[Export] private RayCast3D _playerDetect;
	[Export] private LinkPlayer _linkPlayer;
	
	private readonly PackedScene VAMPIRE_CHARACTER_SCN = GD.Load<PackedScene>("res://Scenes/Enemies/Vampire/VampireCharacter.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (_linkPlayer.Granny != null)
		{
			_playerDetect.LookAt(_linkPlayer.Granny.GlobalPosition+OFFSET_Y,Vector3.Up);
			
			OpenCoffin();
		}
	}
	private void OpenCoffin()
	{
		if(_playerDetect.IsColliding() && _playerDetect.GetCollider() == _linkPlayer.Granny)
		{
			_animationPlayer.Play("appear");
			SetPhysicsProcess(false);
		}
	}
	public void CreateCharacter()
	{
		VampireCharacter newScene = VAMPIRE_CHARACTER_SCN.Instantiate<VampireCharacter>();
		newScene.RotationDegrees = new Vector3 (0,180,0);
		SignalHub.Instance.EmitOnAddNewScene(newScene,this.GlobalPosition);
	}
}
