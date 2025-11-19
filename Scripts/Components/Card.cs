using System.Diagnostics;
using Godot;

public class Card
{	
	public static Control CreateKartya(Kartya kartya)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/card.tscn");
		var card = scene.Instantiate<Control>();

		card.GetNode<Label>("CardName").Text = kartya.nev;
		card.GetNode<Label>("CardDamage").Text = $"⚔️ {kartya.sebzes}";
		card.GetNode<Label>("CardHealth").Text = $"❤️ {kartya.eletero}";
		card.GetNode<Label>("CardType").Text = kartya.tipus switch
		{
			KartyaTipus.Fold => "🗿 Föld 🗿",
			KartyaTipus.Levego => "🍃 Levegő 🍃",
			KartyaTipus.Tuz => "🔥 Tűz 🔥",
			KartyaTipus.Viz => "💧 Víz 💧",
			_ => throw new UnreachableException(),
		};

		if (kartya.nev.Length >= 10)
		{
			card.GetNode<Label>("CardName").AddThemeFontSizeOverride("font_size", 8);
		}

		return card;
	}

	public static Control CreateVezer(Vezer vezer)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/card.tscn");
		var card = scene.Instantiate<Control>();

		Harckartya harckartya = new Harckartya(vezer);

		card.GetNode<Label>("CardName").Text = harckartya.nev;
		card.GetNode<Label>("CardDamage").Text = $"⚔️ {harckartya.sebzes}";
		card.GetNode<Label>("CardHealth").Text = $"❤️ {harckartya.eletero}";
		card.GetNode<Label>("CardType").Text = harckartya.tipus switch
		{
			KartyaTipus.Fold => "🗿 Föld 🗿",
			KartyaTipus.Levego => "🍃 Levegő 🍃",
			KartyaTipus.Tuz => "🔥 Tűz 🔥",
			KartyaTipus.Viz => "💧 Víz 💧",
			_ => throw new UnreachableException(),
		};

		if (harckartya.nev.Length >= 10)
		{
			card.GetNode<Label>("CardName").AddThemeFontSizeOverride("font_size", 8);
		}

		return card;
	}
}
