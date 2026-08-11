using Godot;
using System;

public partial class FireBall : Node3D
{
	
	private Vector3 _velocity = Vector3.Zero;
	private float _gravity = -10.0f;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (_velocity != Vector3.Zero) 
		{
			_velocity.Y += _gravity * (float)delta;
			GlobalTranslate(_velocity * (float)delta);
		}
	}
	public void Setup(float speed, Vector3 direction, float startSpeed = 3.0f) 
	{
		_velocity = direction.Normalized() * speed;
		_velocity.Y = startSpeed;
	}
}
