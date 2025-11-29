using Godot;
using System;
using System.Diagnostics;

public partial class RandomVilag : Control
{
	// Called when the node enters the scene tree for the first time.
	private static readonly Random rng = new();

	private static readonly string[] kartyaNevek = [
"Thargol", "Ravmok", "Khardin", "Brugath", "Valrok", "Morvak", "Draven", "Korvul", "Jorhak", "Targan",
"Rhalgor", "Vornak", "Grumor", "Kelvok", "Thalven", "Darvok", "Lorgath", "Marvok", "Zurgan", "Fralor",
"Kharven", "Drogar", "Varkul", "Gorath", "Hagrun", "Torvak", "Ragnor", "Brakor", "Thirak", "Malgor",
"Jargun", "Krodar", "Vyrnak", "Ulthor", "Drakor", "Zorvun", "Haldor", "Grynak", "Thornak", "Vorgal",
"Krulon", "Mordak", "Yargol", "Thavok", "Durnak", "Gorvak", "Harven", "Ralvok", "Vishar", "Korath",
"Grendor", "Varkos", "Trovak", "Murnok", "Dalven", "Strakor", "Branak", "Torhal", "Kravor", "Vurnak",
"Falgrin", "Kurnok", "Raldor", "Gorven", "Hrakan", "Zurnok", "Bralgor", "Torkin", "Vashor", "Malruk",
"Gralvek", "Dorvun", "Krylor", "Thorgat", "Varlok", "Marvol", "Jalkor", "Hurnak", "Dravon", "Rukmar",
"Korzal", "Frakor", "Vragun", "Brumak", "Thrakor", "Gorzak", "Marhul", "Dragan", "Korven", "Zarlok",
"Vandor", "Thrunak", "Groltar", "Morgul", "Harkon", "Ralmar", "Zorlan", "Trakor", "Varnok", "Drakor"
	];

	private static readonly string[] vezerNevek = [
	"Arvok", "Belmor", "Cadrin", "Dargul", "Elvran", "Farrok", "Galven", "Harvox", "Ilmora", "Jorvak",
	"Kalder", "Lirvon", "Morrek", "Norvak", "Orlien", "Pirram", "Quelar", "Rhalin", "Sorvak", "Torven",
	"Ulrath", "Valmor", "Werrin", "Xandor", "Yravel", "Zaroth", "Arlina", "Brovak", "Cevarn", "Dorlak",
	"Eltora", "Feryon", "Gralor", "Helvak", "Irveth", "Jandor", "Korlin", "Lelran", "Mirval", "Narith",
	"Orveth", "Palron", "Qirath", "Randon", "Selvak", "Tirren", "Ulvorn", "Varlek", "Welmar", "Xirvon",
	"Ylthar", "Zorvak", "Atheon", "Bronar", "Celyth", "Dralor", "Elthon", "Fynrak", "Ghoran", "Hyrven",
	"Ivthor", "Jorlan", "Kylmar", "Lornak", "Myrvol", "Norlith", "Orvran", "Pyrron", "Quivar", "Rolvik",
	"Sylvar", "Tragon", "Ulthor", "Vaelth", "Wyrlen", "Xandil", "Yvronn", "Zalthor", "Arveth", "Balnor",
	"Crenar", "Dalmir", "Enthor", "Faldor", "Garion", "Halrek", "Islor", "Jorvek", "Kelnor", "Lanthor",
	"Marvik", "Nalthor", "Othrin", "Pyrion", "Qalvor", "Ralvek", "Sornak", "Tylmor", "Uvron", "Velrak"
	];

	private static readonly string[] kazaNevek = [
	"Vérmély", "Ködárka", "Sötétverem", "Csontodú", "Kovácsüreg", "Feketevájat", "Pengelabirintus", "Homálygödör", "Vesztetörés", "Fagyüregek",
"Vészszurdok", "Siralomjárat", "Viharkamra", "Porverem", "Láncszakadék", "Mérgesszirt", "Árnyékrács", "Földmoraj", "Sírdereglye", "Vaskoporsó",
"Zúzmaraverem", "Hollóodú", "Kárpitbarlang", "Zordonvájat", "Kormodú", "Halálszirt", "Fátyolrács", "Vasfészek", "Suttogóverem", "Kátrányüreg"
	];

	public override void _Ready()
	{
		var kartyaInput = GetNode<SpinBox>("Panel/VBoxContainer/KartyaContainer/KartyaInput");
		var gyujtemenyInput = GetNode<SpinBox>("Panel/VBoxContainer/GyujtemenyContainer/GyujtemenyInput");

		kartyaInput.ValueChanged += value =>
		{
			gyujtemenyInput.MaxValue = value;
		};
	}

	private static T PickRandom<T>(IList<T> list)
	{
		return list[rng.Next(list.Count)];
	}

	private static List<T> PickNRandom<T>(IEnumerable<T> list, int n)
	{
		return [.. list.OrderBy(_ => rng.Next()).Take(n)];
	}

	private Kartya RandomKartya(string nev)
	{
		return new Kartya(nev, rng.Next(2, 101), rng.Next(1, 101), rng.Next(4) switch
		{
			0 => KartyaTipus.Tuz,
			1 => KartyaTipus.Fold,
			2 => KartyaTipus.Viz,
			3 => KartyaTipus.Levego,
			_ => throw new UnreachableException(),
		});
	}

	private Vezer RandomVezer(string nev, Kartya kartya)
	{
		return new Vezer(nev, kartya, rng.Next(2) switch
		{
			0 => FejlesztesTipus.Sebzes,
			1 => FejlesztesTipus.Eletero,
			_ => throw new UnreachableException(),
		});
	}

	private Kazamata RandomKaza(string nev, Vilag vilag)
	{
		List<KazamataTipus> options = [];
		if (vilag.vilagkartyak.Count >= 5 && vilag.vilagvezerek.Count >= 1)
		{
			options.Add(KazamataTipus.Nagy);
		}
		if (vilag.vilagkartyak.Count >= 3 && vilag.vilagvezerek.Count >= 1)
		{
			options.Add(KazamataTipus.Kis);
		}
		if (vilag.vilagkartyak.Count >= 1)
		{
			options.Add(KazamataTipus.Egyszeru);
		}

		var tipus = PickRandom(options);
		List<Kartya> kartyak = [];
		Vezer? vezer = null;
		FejlesztesTipus? fejlesztes = null;

		switch (tipus)
		{
			case KazamataTipus.Egyszeru:
				kartyak = PickNRandom(vilag.vilagkartyak, 1);
				fejlesztes = rng.Next(2) switch
				{
					0 => FejlesztesTipus.Sebzes,
					1 => FejlesztesTipus.Eletero,
					_ => throw new UnreachableException(),
				};
				break;
			case KazamataTipus.Kis:
				kartyak = PickNRandom(vilag.vilagkartyak, 3);
				vezer = PickRandom(vilag.vilagvezerek);
				fejlesztes = rng.Next(2) switch
				{
					0 => FejlesztesTipus.Sebzes,
					1 => FejlesztesTipus.Eletero,
					_ => throw new UnreachableException(),
				};
				break;
			case KazamataTipus.Nagy:
				kartyak = PickNRandom(vilag.vilagkartyak, 5);
				vezer = PickRandom(vilag.vilagvezerek);
				break;
			default:
				throw new UnreachableException();
		}

		return new Kazamata(tipus, nev, kartyak, vezer, fejlesztes);
	}

	private Vilag VilagGeneral(string nev, int kartyak, int vezer, int gyujtemeny, int kaza)
	{
		var vilag = new Vilag
		{
			nev = nev
		};

		foreach (string kartyaNev in PickNRandom(kartyaNevek, kartyak))
		{
			vilag.vilagkartyak.Add(RandomKartya(kartyaNev));
		}

		foreach (Kartya kartya in PickNRandom(vilag.vilagkartyak, gyujtemeny))
		{
			vilag.jatekos.gyujtemeny.Add(kartya.Clone());
		}

		foreach (string vezerNev in PickNRandom(vezerNevek, vezer))
		{
			vilag.vilagvezerek.Add(RandomVezer(vezerNev, PickRandom(vilag.vilagkartyak)));
		}

		foreach (string kazaNev in PickNRandom(kazaNevek, kaza))
		{
			vilag.kazamatak.Add(RandomKaza(kazaNev, vilag));
		}

		return vilag;
	}

	private void OnLetrehozButtonPressed()
	{
		var nevInput = GetNode<LineEdit>("Panel/VBoxContainer/NevContainer/NevInput");
		var kartyaInput = GetNode<SpinBox>("Panel/VBoxContainer/KartyaContainer/KartyaInput");
		var gyujtemenyInput = GetNode<SpinBox>("Panel/VBoxContainer/GyujtemenyContainer/GyujtemenyInput");
		var vezerInput = GetNode<SpinBox>("Panel/VBoxContainer/VezerContainer/VezerInput");
		var kazaInput = GetNode<SpinBox>("Panel/VBoxContainer/KazaContainer/KazaInput");

		string? error = null;
		if (nevInput.Text.Length == 0)
		{
			error = "A név nem lehet üres.";
		}
		else if (nevInput.Text.Contains(';'))
		{
			error = "A névben nem lehet \";\" karakter.";
		}
		else if (Global.Instance!.vilagok.Find(vilag => vilag.nev == nevInput.Text) != null)
		{
			error = "Létezik már világ ezzel a névvel.";
		}

		if (error != null)
		{
			GetNode<Label>("Panel/VBoxContainer/Error").Text = error;
			return;
		}

		var vilag = VilagGeneral(nevInput.Text, (int)kartyaInput.Value, (int)vezerInput.Value, (int)gyujtemenyInput.Value, (int)kazaInput.Value);
		VilagExport.Export(vilag);

		OnMegsemButtonPressed();
	}

	private void OnMegsemButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/jatekmester.tscn");
	}
}
