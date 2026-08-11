using Godot;
using System;

public partial class PickUpTracker : Node
{
	private PickUpScores _pickUpScores = new();

    public override void _EnterTree()
    {
        SignalHub.Instance.OnPickUpCollected += OnPickUpCollected;
    }

 

    public override void _Ready()
	{
		var children = GetTree().GetNodesInGroup (PickUp.GROUP_NAME);
		foreach (var child in children) 
		{
			if (child is PickUp) // Safety in case group was assigned wrong
			{
				var childPickup = (child as PickUp).GetPickUpType();
				switch(childPickup)
				{
					case PickUp.PickUpType.Coin:
						_pickUpScores.CoinsTotal++;
					break;
					case PickUp.PickUpType.Jewel:
						_pickUpScores.JewelsTotal++;

					break;					
					case PickUp.PickUpType.Key:
						SignalHub.Instance.EmitOnKeyCollected();
					break;
				}
			}	
		}
		GD.Print(_pickUpScores);
		SignalHub.Instance.EmitOnPickUpScoresUpdated(_pickUpScores);
	}

	private void OnPickUpCollected(PickUp pickUp)
    {
		switch(pickUp.GetPickUpType())
		{
			case PickUp.PickUpType.Coin:
				_pickUpScores.CoinsCount++;
			break;
			case PickUp.PickUpType.Jewel:
				_pickUpScores.JewelsCount++;
				if (_pickUpScores.JewelsCount == _pickUpScores.JewelsTotal)
				{
					SignalHub.Instance.EmitOnJewelsCollected();
				}
			break;					
			case PickUp.PickUpType.Key:
			break;
		}
        GD.Print(_pickUpScores);
		SignalHub.Instance.EmitOnPickUpScoresUpdated(_pickUpScores);



    }

}
