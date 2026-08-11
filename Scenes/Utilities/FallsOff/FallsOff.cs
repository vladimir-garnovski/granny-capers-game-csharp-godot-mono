using Godot;
using System;

public partial class FallsOff : Node3D
{
	[Export] private float _yFloor = -50.0f;
	

	public override void _Process(double delta)
	{
		if (GlobalPosition.Y < _yFloor)
			FallOff();
	}
    private void FallOff()
    {
        GrannyUtils.PrintWithParent(this,"Falling off");
		GetParent().QueueFree();
    }
}
