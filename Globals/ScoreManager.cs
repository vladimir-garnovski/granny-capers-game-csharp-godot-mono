using Godot;
using System;

public partial class ScoreManager : Node
{
    public static ScoreManager Instance {get;private set;}

    const string SCORES_PATH = "user://high_scores.tres";
    public HighScores _highScores = new();


    private int _currentScore = 0;
    public int CurrentScore {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;
            if (_currentScore < 0)
                _currentScore = 0;
            SignalHub.Instance.EmitOnScoreChanged(_currentScore);    
        }
    }



    public override void _EnterTree()
    {
        Instance = this;
    }
    public override void _Ready()
    {
       LoadHighScores();
       SignalHub.Instance.OnPlayerDied += OnPlayerDied;
    }
    public void ResetScore()
    {
        _currentScore = 0;
    }
    public override void _ExitTree()
    {
        SignalHub.Instance.OnPlayerDied -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _highScores.AddNewScore(_currentScore);
        SaveHighScores();
    }
    private void SaveGameScore()
    {
        _highScores.AddNewScore(_currentScore);
        SaveHighScores();
    }
    private void LoadHighScores()
    {
        if (ResourceLoader.Exists(SCORES_PATH))
        {
            _highScores = GD.Load<HighScores>(SCORES_PATH);  
        }
		
    }
    private void SaveHighScores()
    {
        ResourceSaver.Save(_highScores, SCORES_PATH);
    }
}

