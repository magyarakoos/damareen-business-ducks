using Godot;
using Godot.NativeInterop;
using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;

public partial class KazaLetrehozo : Control
{
	private Sprite2D? deleteIcon;

	private string name = "";
	private KazamataTipus tipus = KazamataTipus.Egyszeru;
	private FejlesztesTipus rewardType = FejlesztesTipus.Sebzes;
	private List<Kartya> kartyak = [];
	private Vezer? vezer = null;

	public override void _Ready()
	{
		var nameInput = GetNode<LineEdit>("Panel/VBoxContainer/CenterContainer/VBoxContainer/Name/NameInput");
		var typeInput = GetNode<OptionButton>("Panel/VBoxContainer/CenterContainer/VBoxContainer/Type/TypeInput");
		var rewardInput = GetNode<OptionButton>("Panel/VBoxContainer/RewardTitle/RewardInput");

		nameInput.TextChanged += text =>
		{
			name = text;
		};

		typeInput.ItemSelected += index =>
		{
			tipus = typeInput.GetItemText((int)index) switch
			{
				"Egyszerű találkozás" => KazamataTipus.Egyszeru,
				"Kis kazamata" => KazamataTipus.Kis,
				"Nagy kazamata" => KazamataTipus.Nagy,
				_ => throw new UnreachableException(),
			};

			RerenderAll();
		};

		rewardInput.ItemSelected += index =>
		{
			rewardType = rewardInput.GetItemText((int)index) switch
			{
				"+1 sebzés" => FejlesztesTipus.Sebzes,
				"+2 életerő" => FejlesztesTipus.Eletero,
				_ => throw new UnreachableException(),
			};
		};

		RerenderAll();
	}

	public int GetCardCount()
	{
		return tipus switch
		{
			KazamataTipus.Egyszeru => 1,
			KazamataTipus.Kis => 3,
			KazamataTipus.Nagy => 5,
			_ => throw new UnreachableException(),
		};
	}

	private void RerenderKartyak()
	{
		var kartyaControl = GetNode<VBoxContainer>("Panel/VBoxContainer/HBoxContainer/KartyakControl");
		var kartyakLapok = kartyaControl.GetNode<HFlowContainer>("Kartyak");
		var kartyakHozzaad = kartyaControl.GetNode<HBoxContainer>("KartyakTitle/KartyakHozzaad");

		int maxCount = GetCardCount();
		if (kartyak.Count >= maxCount)
		{
			kartyak = [.. kartyak.Take(maxCount)];
			kartyakHozzaad.Hide();
		}
		else
		{
			kartyakHozzaad.Show();
		}

		foreach (Node child in kartyakLapok.GetChildren())
		{
			kartyakLapok.RemoveChild(child);
		}
		foreach (Kartya kartya in kartyak)
		{
			var card = Card.CreateKartya(kartya);

			card.GuiInput += @event =>
			{
				if (@event is InputEventMouseButton btn && btn.Pressed && btn.ButtonMask == MouseButtonMask.Left)
				{
					if (deleteIcon == null) return;
					deleteIcon.Hide();
					deleteIcon = null;

					kartyak = [.. kartyak.Where(kx => kx.nev != kartya.nev)];
					card.EmitSignal(Card.SignalName.RerenderKartyak);
				}
			};

			card.RerenderKartyak += RerenderKartyak;
			
			kartyakLapok.AddChild(card);
		}

		var kartyakInput = kartyakHozzaad.GetNode<OptionButton>("KartyakInput");
		kartyakInput.Clear();
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.vilagkartyak.Where(kx => kartyak.Find(ky => ky.nev == kx.nev) == null))
		{
			kartyakInput.AddItem(kartya.nev);
		}

	}

	private void RerenderVezer()
	{
		var vezerControl = GetNode<VBoxContainer>("Panel/VBoxContainer/HBoxContainer/VezerControl");
		var vezerLapok = vezerControl.GetNode<HFlowContainer>("VezerLapok");
		var vezerHozzaad = vezerControl.GetNode<HBoxContainer>("VezerTitle/VezerHozzaad");

		switch (tipus)
		{
			case KazamataTipus.Egyszeru:
				vezerControl.Hide();
				foreach (Node child in vezerLapok.GetChildren())
				{
					vezerLapok.RemoveChild(child);
				}
				break;
			case KazamataTipus.Kis:
			case KazamataTipus.Nagy:
				vezerControl.Show();
				foreach (Node child in vezerLapok.GetChildren())
				{
					vezerLapok.RemoveChild(child);
				}
				if (vezer == null)
				{
					vezerHozzaad.Show();
				}
				else
				{
					vezerHozzaad.Hide();

					var vezerCard = Card.CreateVezer(vezer);

					vezerCard.GuiInput += @event =>
					{
						if (@event is InputEventMouseButton btn && btn.Pressed && btn.ButtonMask == MouseButtonMask.Left)
						{
							if (deleteIcon == null) return;
							deleteIcon.Hide();
							deleteIcon = null;

							vezer = null;
							vezerCard.EmitSignal(Card.SignalName.RerenderKartyak);
						}
					};

					vezerCard.RerenderKartyak += RerenderVezer;

					vezerLapok.AddChild(vezerCard);
				}

				break;
			default:
				throw new UnreachableException();
		}

		var vezerInput = vezerHozzaad.GetNode<OptionButton>("VezerInput");
		vezerInput.Clear();
		foreach (Vezer vezer in Global.Instance!.aktivVilag!.vilagvezerek.Where(vx => vezer == null || vx.nev != vezer.nev))
		{
			vezerInput.AddItem(vezer.nev);
		}
	}

	private void RerenderReward()
	{
		var rewardTitle = GetNode<HBoxContainer>("Panel/VBoxContainer/RewardTitle");
		switch (tipus)
		{
			case KazamataTipus.Egyszeru:
			case KazamataTipus.Kis:
				rewardTitle.GetNode<OptionButton>("RewardInput").Show();
				rewardTitle.GetNode<Label>("RewardText").Hide();
				break;
			case KazamataTipus.Nagy:
				rewardTitle.GetNode<OptionButton>("RewardInput").Hide();
				rewardTitle.GetNode<Label>("RewardText").Show();
				break;
			default:
				throw new UnreachableException();
		}
	}

	private void RerenderAll()
	{
		RerenderKartyak();
		RerenderVezer();
		RerenderReward();
	}

	private void OnKartyakButtonPressed()
	{
		var kartyaInput = GetNode<OptionButton>("Panel/VBoxContainer/HBoxContainer/KartyakControl/KartyakTitle/KartyakHozzaad/KartyakInput");

		int index = kartyaInput.Selected;
		if (index < 0) return;

		string kartyaNev = kartyaInput.GetItemText(kartyaInput.Selected);
		Kartya kartya = Global.Instance!.aktivVilag!.vilagkartyak.Find(kx => kx.nev == kartyaNev)!;

		kartyak.Add(kartya);

		RerenderKartyak();
	}

	private void OnVezerButtonPressed()
	{
		var vezerInput = GetNode<OptionButton>("Panel/VBoxContainer/HBoxContainer/VezerControl/VezerTitle/VezerHozzaad/VezerInput");

		int index = vezerInput.Selected;
		if (index < 0) return;

		string vezerNev = vezerInput.GetItemText(vezerInput.Selected);
		Vezer vezer = Global.Instance!.aktivVilag!.vilagvezerek.Find(vx => vx.nev == vezerNev)!;

		this.vezer = vezer;

		RerenderVezer();
	}

	private void OnHozzaadButtonPressed()
	{
		string? error = null;
		if (name.Length == 0)
		{
			error = "A név nem lehet üres.";
		}
		else if (name.Contains(';'))
		{
			error = "A névben nem lehet \";\" karakter.";
		}
		else if (Global.Instance!.aktivVilag!.kazamatak.Find(kaza => kaza.nev == name) != null)
		{
			error = "Létezik már kazamata ilyen névvel.";
		}
		else if (kartyak.Count < GetCardCount())
		{
			error = $"Nincs elég kártya kiválasztva ({kartyak.Count}/{GetCardCount()}).";
		}
		else if (tipus != KazamataTipus.Egyszeru && vezer == null)
		{
			error = "Nincs vezér kiválasztva.";
		}

		if (error != null)
		{
			GetNode<Label>("Panel/VBoxContainer/Error").Text = error;
			return;
		}

		var kaza = new Kazamata(tipus, name, kartyak, vezer, rewardType);
		Global.Instance!.aktivVilag!.kazamatak.Add(kaza);

		OnMegsemButtonPressed();
	}

	private void OnDeleteButtonPressed()
	{
		if (deleteIcon != null)
		{
			deleteIcon.Hide();
			deleteIcon = null;
		}
		else
		{
			deleteIcon = GetNode<Sprite2D>("DeleteIcon");
			deleteIcon.Show();
		}
	}

	private void OnMegsemButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/vilag_szerkeszto.tscn");
	}

	public override void _Process(double delta)
	{
		if (deleteIcon != null)
		{
			deleteIcon.Position = GetGlobalMousePosition();
		}
	}
}
