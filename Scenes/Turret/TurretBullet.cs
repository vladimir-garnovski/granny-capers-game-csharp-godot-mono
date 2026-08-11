using Godot;
using System;

public partial class TurretBullet : Node3D
{
	private float _speed = 3.0f;
	private Vector3 _direction = Vector3.Zero;
	private float _yOffset = 1.0f;

	public Vector3 TargetPosition {get;private set;} = Vector3.Zero;
	private bool _targetSet = false;



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (_targetSet)
			Position += _direction * _speed * (float)delta;
	}
	public void SetTargetDeferred(Vector3 targetPosition)
	{
		CallDeferred("SetTarget",targetPosition);
	}
	public void SetTarget(Vector3 targetPosition)
	{
		LookAt(new Vector3(targetPosition.X,targetPosition.Y + _yOffset, targetPosition.Z));
		_direction = -Transform.Basis.Z.Normalized();
		_targetSet = true;
	}
}
