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
			GD.Print($"Original:\n{vilag}\nClone:\n{copy}");
			Global.Instance!.aktivVilag = copy;
			elem.GetTree().ChangeSceneToFile("res://Scenes/vilag_szerkeszto.tscn");
		};

		elem.GetNode<Button>("Panel/HBoxContainer/Torles").Pressed += () =>
		{
			GD.Print($"Starting to delete {vilag.nev}");
			VilagExport.Delete(vilag.nev);
			Global.Instance!.vilagok = VilagExport.ImportAll();
			elem.EmitSignal(SignalName.RerenderVilaglista);
		};

		return elem;
	}
}
