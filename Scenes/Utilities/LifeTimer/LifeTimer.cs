using Godot;
using System;
using System.Threading.Tasks;

public partial class LifeTimer : Node
{
	[Export] private float _lifeTimer = 5.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Create a Timer, wait till it times our, remove parent (+Check if instance is valid)
		GetTree().CreateTimer(_lifeTimer, false).Timeout += () => 
		{ 
			if (GodotObject.IsInstanceValid(this)) // If the instance hasn't been deleted yet
            {
				   GrannyUtils.PrintWithParent(this,"life time over");
                GetParent()?.QueueFree();
            }
		};
		
	}

}
