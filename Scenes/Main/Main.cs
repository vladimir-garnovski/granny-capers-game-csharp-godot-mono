using Godot;
using System;

public partial class Main : Control
{
    private PackedScene HIGHSCORE_DISPLAY_SCN = GD.Load<PackedScene>("res://Scenes/HighScoreDisplay/HighScoreDisplay.tscn");
    [Export] private GridContainer _gridContainer;

    private bool _canPress = false;

    public override void _Ready()
    {
        GetTree().Paused = false;
        
    }
    public void SetPressOn()
    {
        _canPress = true;
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if (_canPress && Input.IsActionJustPressed("shoot"))
        {
            GameManager.Instance.LoadNextLevel();
        }
    }
    public void AddScores()
    {
        foreach (var hs in ScoreManager.Instance._highScores.GetScoreList())
        {
             HighScoreDisplay highScoreDisplay = (HighScoreDisplay)HIGHSCORE_DISPLAY_SCN.Instantiate();
             highScoreDisplay.Setup(hs);
             _gridContainer.AddChild(highScoreDisplay);
        }
       
    }
    
}
/*





*/