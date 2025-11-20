using Godot;
using System;
using System.Diagnostics;

public partial class KazamataCard : Button
{
	public static Control CreateKaza(Kazamata kaza)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/kazamata_card.tscn");
		var card = scene.Instantiate<Control>();

		card.GetNode<Label>("KazaName").Text = kaza.nev;
		card.GetNode<Label>("KazaType").Text = kaza.tipus switch
		{
			KazamataTipus.Egyszeru => "Egyszerű találkozás", 
			KazamataTipus.Kis => "Kis kazamata",
			KazamataTipus.Nagy => "Nagy kazamata",
			_ => throw new UnreachableException(),
		};

		return card;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	private void OnCardPressed()
	{
		Global.Instance!.aktivKaza = Global.Instance!.aktivVilag!.kazamatak.Find(kaza => kaza.nev == GetNode<Label>("KazaName").Text)!;
		GetTree().ChangeSceneToFile("res://Scenes/kaza_viewer.tscn");
	}
}
