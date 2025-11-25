using Godot;
using System;
using System.Diagnostics;

public partial class KazamataCard : Control
{
	[Signal] public delegate void RerenderKartyakEventHandler();

	public static KazamataCard CreateKaza(Kazamata kaza, bool click = true)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/kazamata_card.tscn");
		var card = scene.Instantiate<KazamataCard>();

		card.GetNode<Label>("KazaName").Text = kaza.nev;

		card.GetNode<Sprite2D>("CardImage").Texture = GD.Load<Texture2D>("res://Assets/" + kaza.tipus switch
		{
			KazamataTipus.Egyszeru => "egy_kaz",
			KazamataTipus.Kis => "kis_kaz",
			KazamataTipus.Nagy => "nagy_kaz",
			_ => throw new UnreachableException(),
		} + ".svg");

		if (click)
		{
			card.GuiInput += @event =>
			{
				if (@event is InputEventMouseButton btn && btn.Pressed && btn.ButtonMask == MouseButtonMask.Left)
				{
					Global.Instance!.aktivKaza = Global.Instance!.aktivVilag!.kazamatak.Find(kaza => kaza.nev == card.GetNode<Label>("KazaName").Text)!;
					card.GetTree().ChangeSceneToFile("res://Scenes/kaza_viewer.tscn");
				}
			};
		}

		return card;
	}
}
