using Godot;
using System;
using System.Diagnostics;

public partial class KartyaLetrehozo : Control
{
	public override void _Ready()
	{
		var tipusInput = GetNode<OptionButton>("Panel/VBoxContainer/Tipus/TipusInput");
		List<string> tipusok = ["Föld", "Levegő", "Tűz", "Víz"];
		foreach (string tipus in tipusok)
		{
			tipusInput.AddItem(tipus);
		}
	}

	private void OnHozzaadButtonPressed()
	{
		var nevInput = GetNode<LineEdit>("Panel/VBoxContainer/Nev/NevInput");
		var sebzesInput = GetNode<SpinBox>("Panel/VBoxContainer/Sebzes/SebzesInput");
		var eleteroInput = GetNode<SpinBox>("Panel/VBoxContainer/Eletero/EleteroInput");
		var tipusInput = GetNode<OptionButton>("Panel/VBoxContainer/Tipus/TipusInput");

		string? error = null;
		if (nevInput.Text.Length == 0)
		{
			error = "A névmező nem lehet üres.";
		}
		else if (Global.Instance!.aktivVilag!.vilagkartyak.Find(kartya => kartya.nev == nevInput.Text) != null || Global.Instance!.aktivVilag!.vilagvezerek.Find(kartya => kartya.nev == nevInput.Text) != null)
		{
			error = "Létezik már kártya ilyen névvel.";
		}

		if (error != null)
		{
			GetNode<Label>("Panel/VBoxContainer/Error").Text = error;
			return;
		}

		string nev = nevInput.Text;
		int sebzes = (int)sebzesInput.Value;
		int eletero = (int)eleteroInput.Value;
		KartyaTipus tipus = tipusInput.GetItemText(tipusInput.Selected) switch
		{
			"Föld" => KartyaTipus.Fold,
			"Levegő" => KartyaTipus.Levego,
			"Tűz" => KartyaTipus.Tuz,
			"Víz" => KartyaTipus.Viz,
			_ => throw new UnreachableException(),
		};

		var kartya = new Kartya(nev, sebzes, eletero, tipus);
		Global.Instance!.aktivVilag!.vilagkartyak.Add(kartya);

		OnMegsemButtonPressed();
	}

	private void OnMegsemButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/vilag_szerkeszto.tscn");
	}
}
