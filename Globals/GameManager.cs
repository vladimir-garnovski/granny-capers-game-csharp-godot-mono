using Godot;
using System;
using System.Collections.Generic;

// Job: Switching and navigating scenes, dealing with scores
public partial class GameManager : Node
{
    public static GameManager Instance {get;private set;}

    private static readonly PackedScene MainScene = GD.Load<PackedScene>("res://Scenes/Main/Main.tscn");

    private static readonly Dictionary<int, PackedScene> Levels = new() // This is populated with all the levels
    {
        { 1, GD.Load<PackedScene>("res://Scenes/Level/LevelBase.tscn") }
    };

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("quit"))
        {
            GetTree().Quit();
        }
    }

    public override void _Ready()
    {
        Instance = this;
        ProcessMode = ProcessModeEnum.Always; // Ignoring tree paused
    }

    public override void _ExitTree()
    {
        // Empty, or remove if not needed
    }

    public void ChangeToMain()
    {
        GetTree().ChangeSceneToPacked(MainScene);
    }

    public void LoadNextLevel()
    {
        GetTree().ChangeSceneToPacked(Levels[1]);
    } 
}