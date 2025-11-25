using Godot;
using System;

public partial class VilagSzerkeszto : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		var name = GetNode<LineEdit>("Panel/VBoxContainer/CenterContainer2/Name/NameInput");
        name.Text = Global.Instance!.aktivVilag!.nev;
    }

	private void OnElvetesButtonPressed()
	{
		Global.Instance!.aktivVilag = null;
		GetTree().ChangeSceneToFile("res://Scenes/jatekmester.tscn");
	}

	private void OnMentesButtonPressed()
	{
		VilagExport.Export(Global.Instance!.aktivVilag!);
		OnElvetesButtonPressed();
	}
}
