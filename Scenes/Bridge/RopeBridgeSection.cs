using Godot;
using System;

// ! For this to work in the Solver you need Contact Monitor: ON , Max Contacts Report 1 (or more)
public partial class RopeBridgeSection : RigidBody3D
{
    [Export] private Vector3 _impulse = new(0,10,0); 
    [Export] private Timer _timer;
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }
        public override void _ExitTree()
    {
        BodyEntered -= OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Granny && _timer.IsStopped())
        {
            ApplyCentralImpulse(_impulse);
            _timer.Start();
        }
    }
}
