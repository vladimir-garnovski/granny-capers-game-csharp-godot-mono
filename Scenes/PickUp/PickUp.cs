using Godot;
using System;
using System.Threading.Tasks;

public partial class PickUp : Area3D
{
	public const string GROUP_NAME = "PickUp";
	public enum PickUpType {Jewel,Key,Coin};

	[Export] private PickUpType _pickUpType = PickUpType.Jewel;
	[Export] protected AudioStreamPlayer3D _effects;

	public PickUpType GetPickUpType()
	{
		return _pickUpType;
		
	}
	
	public override void _EnterTree()
	{
		AddToGroup(GROUP_NAME);
	}

	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}


    protected void OnBodyEntered(Node3D body)
	{
	
		if (body is Granny)
		{
			Disable();
			Kill();
			
		}

	}
	protected void Disable()
	{
		Hide();
		SetDeferred("monitoring",false); //Monitoring = false gives an error
	}
	protected virtual void Kill()
	{
		GD.Print("Base Kill()");
		SignalHub.Instance.EmitOnPickUpCollected(this);
		
		_effects.Finished += () => { QueueFree();};
		_effects.Play();
		
		
	}


    public override void _ExitTree()
    {
        BodyEntered -= OnBodyEntered;
    }
 
	
	


}
