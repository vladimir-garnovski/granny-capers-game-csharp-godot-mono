using Godot;
using System;

[Tool]
public partial class Collider : Area3D
{
	[Export] private CollisionShape3D _collisionShape3D; // Root -> CollisionShape ref
	[ExportCategory("Shape | Root ")]
	[Export] public Shape3D _Shape // Root scene Shape
	{
		get
		{
			return _shape;
		}
		set
		{
			_shape = value;
			if (Engine.IsEditorHint())
			{

				UpdateComponent();
			}
		}
	}
	private Shape3D _shape;


	private void UpdateComponent()
	{
		if (_shape != null)
		{
			_collisionShape3D.Shape = _shape;
		}

		
	}
	public override void _Ready()
	{
		UpdateComponent();

		AreaEntered += OnAreaEntered;
		BodyEntered += OnBodyEntered;
	}

    protected virtual void OnBodyEntered(Node3D body)
    {
        throw new NotImplementedException();
    }

    protected virtual void OnAreaEntered(Area3D area)
    {
        throw new NotImplementedException();
    }

    public void Enable()
	{
		SetDeferred("monitorable", true);
		SetDeferred("monitoring", true);
		GrannyUtils.PrintWithParent(this,"DamageCollider Enable()");
	}
	public void Disable()
	{
		SetDeferred("monitorable", false);
		SetDeferred("monitoring", false);
		GrannyUtils.PrintWithParent(this,"DamageCollider Disable()");
	}
	public virtual void Die()
	{
		GrannyUtils.PrintWithParent(this,"Collider Die()");
		GetParent().QueueFree();
	}

}
