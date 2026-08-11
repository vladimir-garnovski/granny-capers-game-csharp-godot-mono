using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class GrannyUtils //: Node
{
    public static void PlayClipStop(AudioStreamPlayer3D player, AudioStream clip) // Stop what's your playing, and play this clip
    {
        player.Stop();
        player.Stream = clip;
        player.Play();
    }
    public static void PlayClip(AudioStreamPlayer3D player, AudioStream clip) // Only switch the clip if we're not already playing
    {
        if (!player.Playing)
        {
            player.Stream = clip;
            player.Play();
        }
    }
    public static void PlayClipPlain(AudioStreamPlayer player, AudioStream clip) // Only switch the clip if we're not already playing
    {
        if (!player.Playing)
        {
            player.Stream = clip;
            player.Play();
        }
    }
    public static String FormattedVec3(Vector3 v) // Formats vectors for debugging
    {
        return $"({v.X.ToString("F1")},{v.Y.ToString("F1")},{v.Z.ToString("F1")})";
    }
    public static String FormattedDt() //
    {
        Godot.Collections.Dictionary dt = Time.GetDatetimeDictFromSystem(); // Turns your datetime to a dictionary
        var shortStr =  $"{dt["day"]:D2}/{dt["month"]:D2} {dt["hour"]:D2}:{dt["minute"]:D2}";
        return shortStr;
    }
    public static void PrintWithParent(Node node, String text) // Printing the name of the parent of the node
    {
        GD.Print(node.GetParent().Name," ::", text);
    }
}
/*
class_name GrannyUtils


static func play_clip_stop(player: AudioStreamPlayer3D, clip: AudioStream):
	player.stop()
	player.stream = clip
	player.play()


static func play_clip(player: AudioStreamPlayer3D, clip: AudioStream):
	if !player.playing:
		player.stream = clip
		player.play()


static func play_clip_plain(player: AudioStreamPlayer, clip: AudioStream):
	if !player.playing:
		player.stream = clip
		player.play()


static func formatted_vec3(v: Vector3) -> String:
	return "(%.1f,%.1f,%.1f)" % [
		v.x,v.y,v.z
	]


static func formatted_dt() -> String:
	var dt = Time.get_datetime_dict_from_system()
	var short_str = "%02d/%02d %02d:%02d" % [dt.day, dt.month, dt.hour, dt.minute]
	return short_str


static func print_with_parent(node: Node, text: String) -> void:
	print(node.get_parent().name, " :: ", text)

*/
