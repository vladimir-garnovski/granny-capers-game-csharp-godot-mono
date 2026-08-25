using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class GrannyUtils //: Node
{
    // Stops current clip , starts a new one
    public static void PlayClipStop(AudioStreamPlayer3D player, AudioStream clip) // Stop what's your playing, and play this clip
    {
        player.Stop();
        player.Stream = clip;
        player.Play();
    }
    // Play a clip
    public static void PlayClip(AudioStreamPlayer3D player, AudioStream clip) // Only switch the clip if we're not already playing
    {
        if (!player.Playing)
        {
            player.Stream = clip;
            player.Play();
        }
    }
    // Play a clip
    public static void PlayClipPlain(AudioStreamPlayer player, AudioStream clip) // Only switch the clip if we're not already playing
    {
        if (!player.Playing)
        {
            player.Stream = clip;
            player.Play();
        }
    }
    // Just show a Vector3 nicely for debugging
    public static String FormattedVec3(Vector3 v) // Formats vectors for debugging
    {
        return $"({v.X.ToString("F1")},{v.Y.ToString("F1")},{v.Z.ToString("F1")})";
    }

    // Just return a nice DayTime string
    public static String FormattedDt()
    {
        Godot.Collections.Dictionary dt = Time.GetDatetimeDictFromSystem(); // Turns your datetime to a dictionary
        var shortStr =  $"{dt["day"]:D2}/{dt["month"]:D2} {dt["hour"]:D2}:{dt["minute"]:D2}";
        return shortStr;
    }
    // Printing the name of the parent of the node
    public static void PrintWithParent(Node node, String text) 
    {
        GD.Print(node.GetParent().Name," ::", text);
    }
}
