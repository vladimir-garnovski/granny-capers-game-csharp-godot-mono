using Godot;
using System;

[Tool]
public partial class MovingPlatformPf : PathFollow3D
{
	[Export] private float _speed  = 2.0f;
	[Export] private CollisionShape3D _collisionShape;
	[Export] private MeshInstance3D _meshInstance3D;


	[Export] private Mesh _platformMesh;
	[Export] private Vector3 _shapePosition = Vector3.Zero;

	[Export] private Vector3 _platformMeshPositon = Vector3.Zero;
	[Export] private Shape3D _shape;

	int _direction = 1 ;


	private void UpdateComponents()
	{
		if (_shape == null || _platformMesh == null)
			return;
			
		_collisionShape.Shape = _shape;
		_meshInstance3D.Mesh = _platformMesh;
		_collisionShape.Position = _shapePosition;
		_meshInstance3D.Position = _platformMeshPositon;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateComponents();
	}


    public override void _Notification(int what)
    {
        if (what == NotificationEditorPostSave)
		{
			UpdateComponents();
		}
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(Engine.IsEditorHint())
			return;
		if (_direction == 1 && ProgressRatio > 0.99)
		{
			_direction = -1;
		}
		else if (_direction == -1 && ProgressRatio < 0.01)
		{
			_direction = 1;
		}
		Progress += (float)delta * _speed * _direction;
	}
}
