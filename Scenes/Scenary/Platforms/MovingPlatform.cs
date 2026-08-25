using Godot;
using Microsoft.VisualBasic;
using System;

[Tool]
public partial class MovingPlatform : AnimatableBody3D
{
	[Export] private Godot.Collections.Array<Vector3> _points = new();
	[Export] private float _speed = 5.0f;

	private Tween _tween; // we use one tween here to avoid creating multiple tweens and overlapping movements
	private int _index = 0;
	private bool _forward = true; // direction flag

	public override void _Ready()
	{
		if (_points.Count > 1)
		{
			_moveToNextPoint();
		}
	}
	public void _moveToNextPoint()
	{
		
		if (_points.Count < 2)
			return;
		if (_tween != null)
			_tween.Kill();

		int nextIndex;
		GD.Print("nextIndex..");
		if (_forward)		
			nextIndex = _index + 1;
		else
			nextIndex = _index - 1;	

		if (nextIndex >= _points.Count)
		{
			_forward = false;
			nextIndex = _index - 1;

		}	
		else if  (nextIndex < 0)
		{
			_forward = true;
			nextIndex = _index + 1;
		}
	
		_index = nextIndex;

		var nextPosition = _points[nextIndex];
		var distance = Position.DistanceTo(nextPosition);
		var moveTime = distance / _speed;

		_tween = CreateTween();
		_tween.TweenProperty(this,"global_position",nextPosition,moveTime);
		_tween.TweenCallback(Callable.From(_moveToNextPoint)).SetDelay(0.05f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
