using Godot;
using System;

public partial class TestBunny : Node3D
{
	[Export] private float _fallSpeed = 10.0f;
	[Export] private LinkPlayer _linkPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Position -= new Vector3(0, _fallSpeed * (float)delta, 0);
		var flatPosition = _linkPlayer.GrannyPosSetY(GlobalPosition.Y);
		LookAt(flatPosition,Vector3.Up); // The LookAt is using Godot's foward in the Z axis
		//GD.Print($"Flat Position:{flatPosition}");
	}
    public override void _ExitTree()
    {
        GD.Print(_linkPlayer.DirectionToGranny(GlobalPosition));
		GD.Print("Test Bunny says bye bye");
    }

}
