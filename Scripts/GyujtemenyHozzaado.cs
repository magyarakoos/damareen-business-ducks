using Godot;
using System;

public partial class GyujtemenyHozzaado : Control
{
	public override void _Ready()
	{
		var options = GetNode<OptionButton>("Panel/VBoxContainer/OptionButton");
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.vilagkartyak.Where(kartya => Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Find(kx => kx.nev == kartya.nev) == null))
		{
			options.AddItem(kartya.nev);
		}
	}

	private void OnHozzaadButtonPressed()
	{
		var options = GetNode<OptionButton>("Panel/VBoxContainer/OptionButton");

		if (options.Selected >= 0)
		{
			string name = options.GetItemText(options.Selected);
			Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Add(Global.Instance!.aktivVilag.vilagkartyak.Find(kartya => kartya.nev == name)!);
		}
		
		OnMegsemButtonPressed();
	}

	private void OnMegsemButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/vilag_szerkeszto.tscn");
	}
}
