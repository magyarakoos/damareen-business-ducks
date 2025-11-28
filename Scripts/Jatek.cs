using Godot;

public partial class Jatek : Control
{
	public override void _Ready()
	{
		SetupPanels();
	}
	
	private void SetupPanels()
	{
		RerenderKartyak(false);
		var cardManager = GetNode<CardManager>("VBoxContainer/CenterContainer/ScrollContainer/HBoxContainer/Node2D");
		cardManager.CardsRerender += () => RerenderKartyak(true);
	}

	private void RerenderVilagkartyak(VBoxContainer panel)
	{
		var vilagkartyak = panel.GetNode<HFlowContainer>("VilagkartyakPanel/MarginContainer/VBoxContainer/Vilagkartyak");
		foreach (Node child in vilagkartyak.GetChildren())
		{
			vilagkartyak.RemoveChild(child);
		}
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.vilagkartyak)
		{
			vilagkartyak.AddChild(Card.CreateKartya(kartya));
		}
	}

	private void RerenderVezerek(VBoxContainer panel)
	{
		var vilagvezerek = panel.GetNode<HFlowContainer>("VezerekPanel/MarginContainer/VBoxContainer/Vezerek");
		foreach (Node child in vilagvezerek.GetChildren())
		{
			vilagvezerek.RemoveChild(child);
		}
		foreach (Vezer vezer in Global.Instance!.aktivVilag!.vilagvezerek)
		{
			vilagvezerek.AddChild(Card.CreateVezer(vezer));
		}
	}

	private void RerenderKazamatak(VBoxContainer panel)
	{
		var kazamatak = panel.GetNode<HFlowContainer>("KazamatakPanel/MarginContainer/VBoxContainer/Kazamatak");
		foreach (Node child in kazamatak.GetChildren())
		{
			kazamatak.RemoveChild(child);
		}
		foreach (Kazamata kaza in Global.Instance!.aktivVilag!.kazamatak)
		{
			var card = KazamataCard.CreateKaza(kaza);
			//card.Disabled = false;
			kazamatak.AddChild(card);
			//myButton.Disabled = false;
		}
	}

	private void RerenderGyujtemeny(VBoxContainer panel)
	{
		Global.Instance!.aktivVilag!.jatekos.pakli ??= [];
		var gyujtemeny = panel.GetNode<HFlowContainer>("GyujtemenyPanel/MarginContainer/VBoxContainer/Gyujtemeny");
		foreach (Node child in gyujtemeny.GetChildren())
		{
			gyujtemeny.RemoveChild(child);
		}
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Where(kartya => Global.Instance!.aktivVilag!.jatekos.pakli!.Find(kx => kx.nev == kartya.nev) == null))
		{
			gyujtemeny.AddChild(Card.CreateKartya(kartya));
		}
	}

	private void RerenderPakli(VBoxContainer panel)
	{
		var pakli = panel.GetNode<HFlowContainer>("PakliPanel/MarginContainer/VBoxContainer/Pakli");
		foreach (Node child in pakli.GetChildren())
		{
			pakli.RemoveChild(child);
		}
		Global.Instance!.aktivVilag!.jatekos.pakli ??= [];
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.jatekos.pakli)
		{
			pakli.AddChild(Card.CreateKartya(kartya));
		}

		for (int i = 0; i < (Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Count + 1) / 2 - Global.Instance!.aktivVilag!.jatekos.pakli.Count; i++)
		{
			pakli.AddChild(CardHolder.CreateHolder());
		}
	}
	
	private void RerenderKartyak(bool jatekosOnly = false)
	{
		if (!jatekosOnly)
		{
			var vilagPanel = GetNode<VBoxContainer>("VBoxContainer/CenterContainer/ScrollContainer/HBoxContainer/VilagInfo");
			RerenderVilagkartyak(vilagPanel);
			RerenderVezerek(vilagPanel);
			RerenderKazamatak(vilagPanel);
		}

		var jatekosPanel = GetNode<VBoxContainer>("VBoxContainer/CenterContainer/ScrollContainer/HBoxContainer/Node2D/JatekosInfo");
		RerenderGyujtemeny(jatekosPanel);
		RerenderPakli(jatekosPanel);
	}

	private void OnVisszaButtonPressed()
	{
		VilagExport.Export(Global.Instance!.aktivVilag!);
		Global.Instance!.aktivVilag = null;
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
}
