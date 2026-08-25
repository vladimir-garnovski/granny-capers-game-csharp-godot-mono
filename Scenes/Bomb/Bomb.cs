using Godot;
using System;
using System.Threading.Tasks;

public partial class Bomb : Node3D
{
	[Export] private Area3D _trigger;
	[Export] private AnimationPlayer _animationPlayer;
	[Export] private AudioStreamPlayer3D _explodeSound;
	[Export] private DamageCollider _damageCollider;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_trigger.BodyEntered += OnTriggerBodyEntered;
		_animationPlayer.AnimationFinished += OnAnimationFinishedAsync;
		_explodeSound.Finished += OnExplodeSoundFinished;

		_damageCollider.Disable();
	}
    public override void _ExitTree()
    {
		_trigger.BodyEntered -= OnTriggerBodyEntered;
		_animationPlayer.AnimationFinished -= OnAnimationFinishedAsync;
		_explodeSound.Finished -= OnExplodeSoundFinished;
    }

    private void OnExplodeSoundFinished()
    {
        QueueFree();
    }

	
    private async void OnAnimationFinishedAsync(StringName animName)
    {
        if (animName == "bang")
		{
			_damageCollider.Enable();
			
			await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
			await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
			_damageCollider.Disable();

		
		}
    }

    private void OnTriggerBodyEntered(Node3D body)
    {
        _trigger.BodyEntered -= OnTriggerBodyEntered;
		_animationPlayer.Play("bang");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
	private void Explode ()
	{
		_explodeSound.Play();
		SignalHub.Instance.EmitOnAddNewExplosion(GlobalPosition);
		//CreateExplosion();
	}
	private void CreateExplosion()
	{
		
	}
}
