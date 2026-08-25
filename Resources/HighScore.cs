using Godot;
using System;

public partial class HighScore : Resource
{
    [Export] public int Score {get;set;}                             // The score
    [Export] public string DateScored = GrannyUtils.FormattedDt(); // Score Date , granny utils formats to a string

    public HighScore(int pScore = 0, string pDateScored = null) //
    {
        Score = pScore;
        DateScored = pDateScored ?? GrannyUtils.FormattedDt(); // if it's null it'll be =GrannyUtils.FormattedDt()
    }
    public HighScore()
    {
        GD.Print("Empty HighScore Constructor..");
    }
}
