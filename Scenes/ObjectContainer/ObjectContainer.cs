using Godot;
using System;

public partial class ObjectContainer : Node
{
	 PackedScene EXPLOSION = GD.Load<PackedScene>("res://Scenes/Explosion/Explosion.tscn");
	 PackedScene SCORED_EFFECT = GD.Load<PackedScene>("res://Scenes/Effects/ScoredEffect/ScoredEffect.tscn");	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnAddNewScene += OnAddNewScene;
		SignalHub.Instance.OnAddNewExplosion += OnAddNewExplosion;
		SignalHub.Instance.OnPickUpCollected += OnPickupCollected;
	}

    private void OnAddNewExplosion(Vector3 pos)
    {
        Explosion ns = (Explosion)EXPLOSION.Instantiate();
		OnAddNewScene(ns, pos);
		

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
		SignalHub.Instance.OnAddNewExplosion -= OnAddNewExplosion;
		SignalHub.Instance.OnPickUpCollected -= OnPickupCollected;
	}

    private void OnPickupCollected(PickUp pickUp)
    {
        ScoredEffect ns = (ScoredEffect)SCORED_EFFECT.Instantiate();
		ns.Setup(pickUp.GetScore());
		OnAddNewScene(ns, pickUp.GlobalPosition);
    }
}
