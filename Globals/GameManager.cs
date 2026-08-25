using Godot;
using System;
using System.Collections.Generic;

// Job: Switching and navigating scenes, dealing with scores
public partial class GameManager : Node
{
    public static GameManager Instance {get;private set;} // Godot mono magic

    private static readonly PackedScene MainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn"); // Main scene

    private static readonly Dictionary<int, PackedScene> Levels = new() // This is populated with all the levels
    {
        { 1, GD.Load<PackedScene>("res://Scenes/Level/Level1.tscn") },
        { 2, GD.Load<PackedScene>("res://Scenes/Level/Level2.tscn") }
    };
    public int CurrentLevel {get;set;} = 0;

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
    }

    public override void _Ready()
    {
        Instance = this; // Godot Mono magic
        ProcessMode = ProcessModeEnum.Always; // Ignoring tree paused
    }

    public override void _ExitTree()
    {
        // Empty, or remove if not needed
    }

    public void ChangeToMain()
    {
        CurrentLevel = 0;
        ScoreManager.Instance.ResetScore();
        GetTree().ChangeSceneToPacked(MainScene);
    }

    public void LoadNextLevel()
    {
        CurrentLevel++;
        if (CurrentLevel > Levels.Count)
            CurrentLevel = 1;
            
        GetTree().ChangeSceneToPacked(Levels[CurrentLevel]);
    } 
}