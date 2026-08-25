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
    [Export] private AudioStreamPlayer  _InGameMusic;

    [Export] private Label _healthLabel;
    [Export] private Label  _levelCompleteLabel;

    [Export] private Label _scoreLabel;
    [Export] private Label _levelLabel;

    private AudioStream DARKLING      = GD.Load<AudioStream>("res://Assets/Audio/Music/Darkling.mp3");
    private AudioStream PARADISE_FOUND =  GD.Load<AudioStream>("res://Assets/Audio/Music/Paradise_Found.mp3");

    bool _canContinue = false;
    public override void _UnhandledInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("exit"))
        {
            GameManager.Instance.ChangeToMain();
        } 
        else if (Input.IsActionJustPressed("shoot") && _canContinue)
        {
            GD.Print("Shoot Clicked.. next lvl will load");
            GameManager.Instance.LoadNextLevel();
        }
    }
    public override void _Ready()
    {
        SignalHub.Instance.OnPickUpScoresUpdated += OnPickUpScoresUpdated;
        SignalHub.Instance.OnJewelsCollected +=  KeyShow;
        SignalHub.Instance.OnKeyCollected += OnKeyCollected;
        SignalHub.Instance.OnLevelCompleted += OnLevelCompleted;
        SignalHub.Instance.OnPlayerHealthChange += OnPlayerHealthChange;
        SignalHub.Instance.OnPlayerDied += OnPlayerDied;
        SignalHub.Instance.OnScoreChanged += OnScoreChanged;
        _levelCompleteRect.Hide();


        GetTree().Paused = false;
        OnScoreChanged(ScoreManager.Instance.CurrentScore);

        _levelLabel.Text = "LV:"+ GameManager.Instance.CurrentLevel;
    }

    private void OnPlayerDied()
    {
        ShowGameOver(true);
    }

    private void OnPlayerHealthChange(int health)
    {
        _healthLabel.Text = health.ToString();
    }

    private void OnLevelCompleted()
    {
        ShowGameOver(false);
    }
    private void ShowGameOver(bool isDead)
    {
        _InGameMusic.Stop();
        GetTree().Paused = true;
        if (isDead)
        {
            _levelCompleteLabel.Text = "Game Over";
        }

        _levelCompleteRect.Show();
        if(!isDead)
        {
            _canContinue = true;
          _continueLabel.Show();
          GrannyUtils.PlayClipPlain(_music, PARADISE_FOUND); 
        } else
        {
            GrannyUtils.PlayClipPlain(_music, DARKLING);
        }
  
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
        SignalHub.Instance.OnLevelCompleted -= OnLevelCompleted;
        SignalHub.Instance.OnPlayerHealthChange -= OnPlayerHealthChange;
        SignalHub.Instance.OnPlayerDied -= OnPlayerDied;
        SignalHub.Instance.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _scoreLabel.Text = score.ToString("D3");
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
