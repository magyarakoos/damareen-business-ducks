using System.Diagnostics;
using System.Security.Principal;
using Godot;

public partial class Card : Control
{
	[Signal] public delegate void RerenderKartyakEventHandler();

	public static Card CreateKartya(Kartya kartya)
	{
		return CreateHarckartya(new Harckartya(kartya), false);
	}

	public static Card CreateVezer(Vezer vezer)
	{
		return CreateHarckartya(new Harckartya(vezer), true);
	}

	public static Card CreateHarckartya(Harckartya kartya, bool vezer)
	{
		var scene = GD.Load<PackedScene>("res://Scenes/card.tscn");
		var card = scene.Instantiate<Card>();

		card.GetNode<Label>("CardName").Text = kartya.nev;
		card.GetNode<Label>("DamageContainer/CardDamage").Text = $"{kartya.sebzes}";
		card.GetNode<Label>("HealthContainer/CardHealth").Text = $"{kartya.eletero}";

		string cardPath;
		Color? textColor = null;

		switch (kartya.tipus)
		{
			case KartyaTipus.Fold:
				cardPath = "res://Assets/card_earth";
				break;
			case KartyaTipus.Levego:
				cardPath = "res://Assets/card_wind";
				break;
			case KartyaTipus.Tuz:
				cardPath = "res://Assets/card_fire";
				break;
			case KartyaTipus.Viz:
				cardPath = "res://Assets/card_water";
				break;
			default:
				throw new UnreachableException();
		}

		if (vezer)
		{
			cardPath += "_vezer";
		}

		card.GetNode<Sprite2D>("CardImage").Texture = GD.Load<Texture2D>(cardPath + ".svg");
		if (textColor != null)
		{
			card.GetNode<Label>("CardName").AddThemeColorOverride("font_color", (Color)textColor);
		}

		var label = card.GetNode<Label>("CardName");
		var font = label.GetThemeDefaultFont();

		int fontSize = 12;
		int maxWidth = 0;
		while (fontSize > 6)
		{
			maxWidth = kartya.nev
				.Split(' ')
				.Select(szo => new Label() { Text = szo })
				.Select(l => font.GetStringSize(l.Text, HorizontalAlignment.Left, -1, fontSize).X)
				.Select(f => (int)Math.Round(f))
				.Max();
			
			if (maxWidth <= 67)
			{
				break;
			}
			
			fontSize--;
		}

		label.AddThemeFontSizeOverride("font_size", fontSize);

		var area = card.GetNode<Area2D>("Area2D");

		return card;
	}
}
