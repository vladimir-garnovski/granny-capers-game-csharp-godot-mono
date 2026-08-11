using Godot;
using System;
using System.Threading.Tasks;

public partial class Key : PickUp
{
	AudioStream KEY_APPEARS = GD.Load<AudioStream>("res://Assets/Audio/Effects/coin_pickup.wav");
    AudioStream KEY_COLLECT = GD.Load<AudioStream>("res://Assets/Audio/Effects/Jewel_Collect.wav");

    public override void _EnterTree()
    {
        SignalHub.Instance.OnJewelsCollected += Enable;
    }

    public override void _Ready()
    {        
        base._Ready();
        Disable();
    }
    private void Enable() 
    {
        SetDeferred("monitoring",true);
        Show();
        GrannyUtils.PlayClip(_effects, KEY_APPEARS);
        GD.Print("Key enabled!");
    }
    protected override void Kill()
    {
        GD.Print("Key Kill()");
        _effects.Stream = KEY_COLLECT;
        SignalHub.Instance.EmitOnKeyCollected();
        base.Kill();
        GD.Print("Key killed!");
    }
   

    public override void _ExitTree()
    {
        SignalHub.Instance.OnJewelsCollected -= Enable;
    }

}
