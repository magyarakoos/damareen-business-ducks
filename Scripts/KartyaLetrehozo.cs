using Godot;
using System;
using System.Diagnostics;

public partial class KartyaLetrehozo : Control
{
	public override void _Ready()
	{
		var nevInput = GetNode<LineEdit>("Panel/VBoxContainer/Nev/NevInput");
		var sebzesInput = GetNode<SpinBox>("Panel/VBoxContainer/Sebzes/SebzesInput");
		var eleteroInput = GetNode<SpinBox>("Panel/VBoxContainer/Eletero/EleteroInput");
		var tipusInput = GetNode<OptionButton>("Panel/VBoxContainer/Tipus/TipusInput");
		List<string> tipusok = ["Föld", "Levegő", "Tűz", "Víz"];
		foreach (string tipus in tipusok)
		{
			tipusInput.AddItem(tipus);
		}

		nevInput.TextChanged += text => UpdateKartya();
		sebzesInput.ValueChanged += number => UpdateKartya();
		eleteroInput.ValueChanged += number => UpdateKartya();
		tipusInput.ItemSelected += index => UpdateKartya();
	}

	public void UpdateKartya()
	{
		var kartyatarto = GetNode<Control>("Kartyatarto");
		foreach (Node child in kartyatarto.GetChildren())
		{
			kartyatarto.RemoveChild(child);
		}

		var nevInput = GetNode<LineEdit>("Panel/VBoxContainer/Nev/NevInput");
		var sebzesInput = GetNode<SpinBox>("Panel/VBoxContainer/Sebzes/SebzesInput");
		var eleteroInput = GetNode<SpinBox>("Panel/VBoxContainer/Eletero/EleteroInput");
		var tipusInput = GetNode<OptionButton>("Panel/VBoxContainer/Tipus/TipusInput");

		if (nevInput.Text.Length == 0 || nevInput.Text.Contains(';'))
		{
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

		kartyatarto.AddChild(Card.CreateKartya(kartya));
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
			error = "A név nem lehet üres.";
		}
		else if (nevInput.Text.Contains(';'))
		{
			error = "A névben nem lehet \";\" karakter.";
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
