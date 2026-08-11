using Godot;
using System;

public partial class SignalHub : Node
{
    public static SignalHub Instance {get;private set;}

    [Signal]
    public delegate void OnPickUpCollectedEventHandler(PickUp pickUp);

    [Signal]
    public delegate void OnPickUpScoresUpdatedEventHandler(PickUpScores pickUpScores);

    [Signal]
    public delegate void OnJewelsCollectedEventHandler();

    [Signal]
    public delegate void OnKeyCollectedEventHandler();

    [Signal]
    public delegate void OnLevelCompletedEventHandler();

    [Signal]
    public delegate void OnPlayerDiedEventHandler();

    [Signal]
    public delegate void OnAddNewSceneEventHandler(Node3D ob, Vector3 pos);


    public override void _EnterTree()
    {
        Instance = this;
    }

    // Emit Signal functions
    public void EmitOnLevelCompleted()
    {
        EmitSignal(SignalName.OnLevelCompleted);
    }
    public void EmitOnPickUpCollected(PickUp pickUp)
    {
        EmitSignal(SignalName.OnPickUpCollected, pickUp );
    }
    public void EmitOnPickUpScoresUpdated(PickUpScores pickUpScores)
    {
        EmitSignal(SignalName.OnPickUpScoresUpdated, pickUpScores);
    }
    public void EmitOnJewelsCollected()
    {
        GD.Print("SignalHub: EmitOnJewelsCollected()");
        EmitSignal(SignalName.OnJewelsCollected);
    }
    public void EmitOnKeyCollected()
    {
        EmitSignal(SignalName.OnKeyCollected);
    }
    public void EmitPlayerDied()
    {
        EmitSignal(SignalName.OnPlayerDied);
    }
    public void EmitOnAddNewScene(Node3D ob, Vector3 pos)
    {
        EmitSignal(SignalName.OnAddNewScene,ob,pos);
    }
}
