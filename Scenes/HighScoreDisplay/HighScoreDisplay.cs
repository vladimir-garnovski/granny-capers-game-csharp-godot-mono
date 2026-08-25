using Godot;
using System;

public partial class HighScoreDisplay : HBoxContainer
{
    [Export] private Label _labelScore;
    [Export] private Label _labelTime;

    private HighScore _highScore = null;

    public override void _Ready()
    {
        if (_highScore != null)
        {
            _labelScore.Text = _highScore.Score.ToString("D4");
            _labelTime.Text = _highScore.DateScored;

        }
    }
    public void Setup(HighScore _highScore)
    {
        this._highScore = _highScore;
    }
}   



