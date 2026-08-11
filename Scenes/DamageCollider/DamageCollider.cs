using Godot;
using System;

[Tool]
public partial class DamageCollider : Collider
{
	[Signal]
	public delegate void DamageGivenEventHandler(int amount);

	[ExportCategory("Damage")]
	[Export] private int _damageAmount = 10;
	[Export] private bool _expolosedOnHit = true;
	[Export] private bool  _diesOnHit = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready(); // For the hooked signal :)

	}
	protected override void OnAreaEntered(Area3D area)
	{
		GrannyUtils.PrintWithParent(this, $"DamageCollider OnAreaEntered()");
		ApplyImpactEffects();
	}

	private void ApplyImpactEffects()
	{
		EmitDamageGiven(_damageAmount);
		if (_diesOnHit)
		{
			GrannyUtils.PrintWithParent(this, "DamageCollider DiesOnHit()");
			Die(); 
		}
		if (_expolosedOnHit)
		{
			GrannyUtils.PrintWithParent(this, "DamageCollider ExplodeOnHit()");

		}
			
	}
	public void EmitDamageGiven(int amount)
	{
		EmitSignal(SignalName.DamageGiven, amount);
	}
	public int GetDamage()
	{
		return _damageAmount;
	}
	
}
