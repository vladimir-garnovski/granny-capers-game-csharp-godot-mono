using Godot;
using System;

public partial class SignalHub : Node
{
    public static SignalHub Instance {get;private set;}

    [Signal] // OnPickUpCollected - Any time a pickup is collected
    public delegate void OnPickUpCollectedEventHandler(PickUp pickUp);

    [Signal] // OnPickUpScoresUpdated - Any to,e a score is updated
    public delegate void OnPickUpScoresUpdatedEventHandler(PickUpScores pickUpScores);

    [Signal] // OnJewelsCollected - any time a jewel is collected
    public delegate void OnJewelsCollectedEventHandler();

    [Signal] // OnKeyCollected - any time a key is collected
    public delegate void OnKeyCollectedEventHandler();

    [Signal] // OnLevelCompleted - Once level is complete
    public delegate void OnLevelCompletedEventHandler();

    [Signal] // OnPlayerDied - Once player died
    public delegate void OnPlayerDiedEventHandler();

    [Signal] // OnAddNewScene - Once a Scene is added
    public delegate void OnAddNewSceneEventHandler(Node3D ob, Vector3 pos);

    [Signal]
    public delegate void OnAddNewExplosionEventHandler(Vector3 pos);

    [Signal]
    public delegate void OnPlayerHealthChangeEventHandler(int health);

    [Signal]
    public delegate void OnPlayerBounceEventHandler(float speed);

    [Signal]
    public delegate void OnScoreChangedEventHandler(int score);

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
    public void EmitOnAddNewExplosion(Vector3 pos)
    {
        EmitSignal(SignalName.OnAddNewExplosion, pos);
    }
    public void EmitOnPlayerHealthChange(int health)
    {
        EmitSignal(SignalName.OnPlayerHealthChange, health);
    }
    public void EmitOnPlayerBounce(float speed)
    {
        EmitSignal(SignalName.OnPlayerBounce, speed);
    }
    public void EmitOnScoreChanged(int score)
    {
        EmitSignal(SignalName.OnScoreChanged, score);
    }
}
