using Godot;
using System;
using System.Diagnostics;

public partial class VezerLetrehozo : Control
{
	public override void _Ready()
	{
		var alapkartyaInput = GetNode<OptionButton>("Panel/VBoxContainer/Alapkartya/AlapkartyaInput");
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.vilagkartyak)
		{
			alapkartyaInput.AddItem(kartya.nev);
		}

		var fejlesztesInput = GetNode<OptionButton>("Panel/VBoxContainer/Fejlesztes/FejlesztesInput");
		List<string> fejlesztesek = ["Sebzés duplázás", "Életerő duplázás"];
		foreach (string fejlesztes in fejlesztesek)
		{
			fejlesztesInput.AddItem(fejlesztes);
		}
	}

	private void OnHozzaadButtonPressed()
	{
		var nevInput = GetNode<LineEdit>("Panel/VBoxContainer/Nev/NevInput");
		var alapkartyaInput = GetNode<OptionButton>("Panel/VBoxContainer/Alapkartya/AlapkartyaInput");
		var fejlesztesInput = GetNode<OptionButton>("Panel/VBoxContainer/Fejlesztes/FejlesztesInput");

		if (alapkartyaInput.ItemCount == 0)
		{
			return;
		}

		string? error = null;
		if (nevInput.Text.Length == 0)
		{
			error = "A névmező nem lehet üres.";
		}
		else if (Global.Instance!.aktivVilag!.vilagvezerek.Find(kartya => kartya.nev == nevInput.Text) != null || Global.Instance!.aktivVilag!.vilagkartyak.Find(kartya => kartya.nev == nevInput.Text) != null)
		{
			error = "Létezik már kártyal ilyen névvel.";
		}

		if (error != null)
		{
			GetNode<Label>("Panel/VBoxContainer/Error").Text = error;
			return;
		}

		string nev = nevInput.Text;
		string kartyaNev = alapkartyaInput.GetItemText(alapkartyaInput.Selected);

		var kartya = Global.Instance!.aktivVilag!.vilagkartyak.Find(kartya => kartya.nev == kartyaNev)!;

		FejlesztesTipus fejlesztes = fejlesztesInput.GetItemText(fejlesztesInput.Selected) switch
		{
			"Sebzés duplázás" => FejlesztesTipus.Sebzes,
			"Életerő duplázás" => FejlesztesTipus.Eletero,
			_ => throw new UnreachableException(),
		};

		var vezer = new Vezer(nev, kartya, fejlesztes);
		Global.Instance!.aktivVilag!.vilagvezerek.Add(vezer);

		OnMegsemButtonPressed();
	}

	private void OnMegsemButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/vilag_szerkeszto.tscn");
	}
}
