using Godot;
using System;


public partial class Vilagletrehozo : Control
{

	

	[Export] public Button Letrehoz { get; set; } = null!;
	
  



	public override void _Ready()
	{
	
		if (Letrehoz != null)
		{
		  
			Letrehoz.Pressed += _on_Letrehoz_pressed;
		}
		else
		{
			GD.PushError("Hiba: A 'Letrehoz' gomb nincs megfelelően beállítva a scriptben.");
		}
	}

	
	public override void _Process(double delta)
	{
	  
	}


	private void _on_Letrehoz_pressed()
	{
  
	
		
	  
		

	
			
	
		GetTree().ChangeSceneToFile("res://Scenes/világcsináló.tscn");
	
	}
}
