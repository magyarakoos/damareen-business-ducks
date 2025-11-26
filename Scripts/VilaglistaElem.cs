using Godot;
using System;

public partial class VilaglistaElem : Control
{
	[Signal] public delegate void RerenderVilaglistaEventHandler();
	public static VilaglistaElem CreateElem(Vilag vilag)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/vilaglista_elem.tscn");
		var elem = scene.Instantiate<VilaglistaElem>();

		elem.GetNode<Label>("Panel/HBoxContainer/Vilagnev").Text = vilag.nev;

		elem.GetNode<Button>("Panel/HBoxContainer/Szerkesztes").Pressed += () =>
		{
			var copy = new Vilag(vilag);
			Global.Instance!.aktivVilag = copy;
			elem.GetTree().ChangeSceneToFile("res://Scenes/vilag_szerkeszto.tscn");
		};

		elem.GetNode<Button>("Panel/HBoxContainer/Torles").Pressed += () =>
		{
			VilagExport.Delete(vilag.nev);
			elem.EmitSignal(SignalName.RerenderVilaglista);
		};

		return elem;
	}
}
