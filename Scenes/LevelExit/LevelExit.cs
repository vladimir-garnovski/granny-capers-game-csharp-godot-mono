using Godot;
using System;

public partial class LevelExit : Node3D
{
	[Export] private Node3D _labelHolder;
	[Export] private Label3D _labelKey;

	[Export] private Area3D _exitArea;

	[Export] private AudioStreamPlayer3D _effects;

	private bool _keyCollected = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SignalHub.Instance.OnKeyCollected += OnKeyCollected;
		_labelHolder.Hide();
		_exitArea.BodyEntered += OnBodyEntered;
		_exitArea.BodyExited  += OnBodyExited;

		AnimateLabel();
	}

    private void OnBodyExited(Node3D body)
    {
        _labelHolder.Hide();
    }

    private void OnBodyEntered(Node3D body)
    {
        if (_keyCollected)
		{
			SignalHub.Instance.EmitOnLevelCompleted();
		}else
		{
			_labelHolder.Show();
			_effects.Play();
		}
    }

    private void OnKeyCollected()
    {
        _keyCollected = true;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
    public override void _ExitTree()
    {
        SignalHub.Instance.OnKeyCollected -= OnKeyCollected;
		_exitArea.BodyEntered -= OnBodyEntered;
		_exitArea.BodyExited  -= OnBodyExited;
    }
	private void AnimateLabel()
	{
		Tween tw = CreateTween();
		tw.SetLoops(0);
		tw.TweenProperty(_labelKey,"visible",true,0.6);
		tw.TweenProperty(_labelKey,"visible",false,0.1);

	}
}
