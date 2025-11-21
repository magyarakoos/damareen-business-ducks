using Godot;
using System;

public partial class Vilagcsinalo : Control
{
	[Export] public Label vilag { get; set; } = null!;
	

	public override void _Ready()
	{
		
		vilag.Text==vilagnev;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
