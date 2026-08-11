using Godot;
using System;

public partial class PickUpScores : GodotObject // we extend GodotObject in order to use it as a signal..
{
	public int CoinsTotal  {get; set;} = 0;
	public int JewelsTotal {get; set;} = 0;
	public int CoinsCount  {get; set;} = 0;
	public int JewelsCount {get; set;} = 0;


	public bool AllJewelsCollected {get {return JewelsCount == JewelsTotal; } }

    public override string ToString()
    {
        return $"PickUpScore: Coins: {CoinsCount}/{CoinsTotal} Jewels: {JewelsCount}/{JewelsTotal}";
    }

}
