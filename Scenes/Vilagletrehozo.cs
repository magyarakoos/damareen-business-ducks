using Godot;
using System;


public partial class Vilagletrehozo : Control
{

	

	[Export] public Button Letrehoz { get; set; } = null!;
	
  
	[Export] public LineEdit Nev { get; set; } = null!;


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
  
		if (Nev == null)
		{
			GD.PushError("Hiba: A 'Nev' LineEdit nincs megfelelően beállítva a scriptben.");
			return;
		}
		
	  
		if (string.IsNullOrEmpty(Nev.Text))
		{
		  
			GD.PushError("A név nem lehet üres!");
		
		
		}

	 Nev.Clear();
		GD.Print($"Világ létrehozása ezzel a névvel: {Nev.Text}");
	}
}
