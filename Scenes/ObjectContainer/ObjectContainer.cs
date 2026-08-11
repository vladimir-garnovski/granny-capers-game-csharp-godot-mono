using Godot;
using System;

public partial class ObjectContainer : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnAddNewScene += OnAddNewScene;
	}
	private void AddWithPosition(Node3D ob, Vector3 pos)
	{
		
		AddChild(ob); // the _Ready of the object is in worked
		ob.GlobalPosition = pos;
		
	}
    private void OnAddNewScene(Node3D ob, Vector3 pos)
    {
        CallDeferred("AddWithPosition",ob,pos);
    }
	public override void _ExitTree()
	{
		SignalHub.Instance.OnAddNewScene -= OnAddNewScene;
	}

}
