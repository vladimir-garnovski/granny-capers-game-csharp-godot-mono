using Godot;
using System;
using System.Transactions;

[Tool]
public partial class HurtBox : Collider
{
	[Signal]
	public delegate void DiedEventHandler();
	[Signal]
	public delegate void DamageTakenEventHandler(int dmg);

	[ExportCategory("Hurt Receiver")]
	[Export] private bool _killsParentOnDeath = true;
	[Export] private bool _diesOnSingleImpact = false;
	[Export] private int _maxHealth = 100;
		// Called when the node enters the scene tree for the first time.
	private int _currentHealth;
	public int CurrentHealth
	{
		get
		{
			return _currentHealth;
		}
		set
		{
			_currentHealth = value > 0? value : 0 ;
		}
	}
		
		
		

	public override void _Ready()
	{
		base._Ready();
		CurrentHealth = _maxHealth;
	}

	public void TakeHit(int amount)
	{
		
		if (amount <= 0)
		{
			return;
		}
		else
		{
			
			_currentHealth -= amount;
			
			EmitSignal(SignalName.DamageTaken, amount);
			GrannyUtils.PrintWithParent(this, $"HurtBox Take Damage, current health: {_currentHealth}");

			if (_currentHealth <=0)
			{
				Die();
			}
		}
	}
	public void TakeDamage(int amount)
	{
		GrannyUtils.PrintWithParent(this,"HurtBox TakeHit()");
		if (_diesOnSingleImpact)
		{
			GrannyUtils.PrintWithParent(this,"HurtBox dies on signle impact()");
			TakeDamage(_currentHealth + 1 );

		}
		else
		{
			TakeDamage(amount);
		}
	}
	public override void  Die()
	{
		EmitSignal(SignalName.Died);
		if (_killsParentOnDeath)
		{
			base.Die();
		}
	}
	protected override void OnAreaEntered(Area3D area)
	{
		if (area is DamageCollider)
		{
			GrannyUtils.PrintWithParent(this,"HurtBox OnAreaEntered()");
			TakeHit((area as DamageCollider).GetDamage());
		}
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
