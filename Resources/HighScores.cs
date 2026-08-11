using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class HighScores : Resource
{
    private const int MAX_SCORES = 10; // 10 scores max

    [Export] Godot.Collections.Array<HighScore> _highScores = new Godot.Collections.Array<HighScore>(); // An Array(list) of the HighScore class

    public HighScores()
    {
        SortScores();
    }
    private void SortScores()
    {
        // LINQ approach - sorts and creates new array.
        var sorted = _highScores.OrderByDescending(x => x.Score).ToList();
        _highScores.Clear();
        foreach (var item in sorted)
        {
            _highScores.Add(item);
        }
    }
    public Godot.Collections.Array<HighScore>  GetScoreList() // Getter for getting a score list
    {
        return _highScores;
    }
    public void AddNewScore(int score)
    {
        var newHighScore = new HighScore(score, GrannyUtils.FormattedDt());
        _highScores.Add(newHighScore);
        SortScores();
        
        while (_highScores.Count > MAX_SCORES) // Keeping the score array not bigger than the max
        {
            _highScores.RemoveAt(_highScores.Count - 1);
        }
    }
}
