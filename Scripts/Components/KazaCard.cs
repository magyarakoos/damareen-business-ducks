using System.Diagnostics;
using Godot;

public class KazaCard
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
}
