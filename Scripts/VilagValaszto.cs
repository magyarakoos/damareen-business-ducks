using Godot;
using System;

public partial class VilagValaszto : Control
{
	public override void _Ready()
	{
		var vilaglista = GetNode<OptionButton>("Panel/VBoxContainer/Vilagok");
		foreach (Vilag vilag in Global.Instance!.vilagok)
		{
			vilaglista.AddItem(vilag.nev);
		}
	}

	private void OnVilagKivalasztPressed()
	{
		var vilaglista = GetNode<OptionButton>("Panel/VBoxContainer/Vilagok");
		string selected = vilaglista.GetItemText(vilaglista.Selected);

		Global.Instance!.aktivVilag = Global.Instance!.vilagok.Find((vilag) => vilag.nev == selected)!;
		GetTree().ChangeSceneToFile("res://Scenes/jatekos.tscn");
	}
	
	private void OnVisszaButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
}
