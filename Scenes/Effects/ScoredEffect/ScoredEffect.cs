using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


public partial class ScoredEffect : Node3D
{
    [Export] private Label3D _scoreLabel;
    [Export] private AnimationPlayer _animationPlayer;

    private int _points = 0;
    public override async void _Ready()
    {
        _scoreLabel.Text = "+" + _points;
        _animationPlayer.Play("score");
        await ToSignal(_animationPlayer, "animation_finished");
        QueueFree();
    }
    public void Setup(int points)
    {
        _points = points;
    }
}
