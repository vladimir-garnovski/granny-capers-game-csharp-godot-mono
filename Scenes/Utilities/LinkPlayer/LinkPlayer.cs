using Godot;
using System;
/*

public Granny Granny => _grannyRef;

public float GrannyX => _grannyRef?.GlobalPosition.X ?? 0.0f;
public float GrannyY => _grannyRef?.GlobalPosition.Y ?? 0.0f;
public float GrannyZ => _grannyRef?.GlobalPosition.Z ?? 0.0f;

public Vector3 GrannyPos => _grannyRef?.GlobalPosition ?? Vector3.Zero;
*/
public partial class LinkPlayer : Node
{
	private Granny _grannyRef;
	public Granny Granny => _grannyRef; // _grannyRef getter

	public float GrannyX => _grannyRef?.GlobalPosition.X ?? 0.0f;
	public float GrannyY => _grannyRef?.GlobalPosition.Y ?? 0.0f;
	public float GrannyZ => _grannyRef?.GlobalPosition.Z ?? 0.0f;
	public Vector3 GrannyPos => _grannyRef?.GlobalPosition ?? Vector3.Zero;


	public Vector3 GrannyPosSetY(float y)
	{
		return new Vector3(_grannyRef.GlobalPosition.X,y, _grannyRef.GlobalPosition.Z);
	}
	public Vector3 DirectionToGranny(Vector3 ourPos)
	{
		//return (_grannyRef.GlobalPosition - ourPos).Normalized();
		return ourPos.DirectionTo(_grannyRef.GlobalPosition); // diretion to returns a normolized vector
	}
	public bool GrannyTooClose (Vector3 OurPos)
	{
		return OurPos.DistanceTo(_grannyRef.GlobalPosition) < 0.2;
	}
	public bool GrannyTooCloseIgnoreY (Vector3 OurPos)
	{
		Vector3 ourPostIgnoreY = OurPos * new Vector3 (1,0,1);
		Vector3 grannyRefGlobalPositionIgnoreY =  _grannyRef.GlobalPosition * new Vector3 (1,0,1);

		return ourPostIgnoreY.DistanceTo(grannyRefGlobalPositionIgnoreY) < 0.2;
	}
    public override void _Ready()
    {
        FindRef();
    }
    public override void _EnterTree()
    {
        SignalHub.Instance.OnPlayerDied += OnPlayerDied;
    }
	private void OnPlayerDied()
	{
		_grannyRef = null;
	}
	public void FindRef()
	{
		_grannyRef = (Granny)GetTree().GetFirstNodeInGroup(Granny.GROUP_NAME);
	}
    public override void _ExitTree()
    {
        SignalHub.Instance.OnPlayerDied -= OnPlayerDied;
    }
}
