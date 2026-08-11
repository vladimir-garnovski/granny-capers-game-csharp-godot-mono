using Godot;
using System;
using System.Threading.Tasks;

public partial class Hud : Control
{
    [Export] private Label _coinsLabel;
    [Export] private Label _jewelsLabel;
    [Export] private TextureRect _key;
    [Export] private Label _exitLabel;
    [Export] private Label _continueLabel;
    [Export] private ColorRect _levelCompleteRect;
    [Export] private AudioStreamPlayer  _music;


    private AudioStream DARKLING      = GD.Load<AudioStream>("res://Assets/Audio/Music/Darkling.mp3");
    private AudioStream PARADISE_FOUND =  GD.Load<AudioStream>("res://Assets/Audio/Music/Darkling.mp3");

    public override void _Ready()
    {
        SignalHub.Instance.OnPickUpScoresUpdated += OnPickUpScoresUpdated;
        SignalHub.Instance.OnJewelsCollected +=  KeyShow;
        SignalHub.Instance.OnKeyCollected += OnKeyCollected;
        SignalHub.Instance.OnLevelCompleted += OnLevelCompleted;
        _levelCompleteRect.Hide();
    }

    private void OnLevelCompleted()
    {
        ShowGameOver();
    }
    private void ShowGameOver()
    {
        GetTree().Paused = true;
        _levelCompleteRect.Show();

        bool timerFinished = false;
        GetTree().CreateTimer(1.0).Timeout+= ()=> {timerFinished = true;};
        //while(!timerFinished) {GD.Print("Timer running");}
        GD.Print("Timer finished");
        _continueLabel.Show();
        GrannyUtils.PlayClipPlain(_music, PARADISE_FOUND);
    }

    private void OnKeyCollected()
    {
        _key.Hide();
        _exitLabel.Show();
    }

    private void OnPickUpScoresUpdated(PickUpScores pickUpScores)
    {
        _jewelsLabel.Text = $"{pickUpScores.JewelsCount} / {pickUpScores.JewelsTotal}";
        _coinsLabel.Text = $"{pickUpScores.CoinsCount} / {pickUpScores.CoinsTotal}";
    }
    public override void _ExitTree()
    {
        SignalHub.Instance.OnPickUpScoresUpdated -= OnPickUpScoresUpdated;
        SignalHub.Instance.OnJewelsCollected -=  KeyShow;
        SignalHub.Instance.OnKeyCollected -= OnKeyCollected;
    }
    private void KeyShow()
    {
        _key.Show();
        Tween tween = CreateTween();
        tween.SetLoops(0);

        tween.TweenProperty(
            _key,                //object
            "modulate",          //property
            new Color(1,1,1,0),  // Final value
            1                  // Duration
        );
        tween.TweenProperty(_key, "modulate",new Color(1,1,1,1), 1);

    }
}
