using System.Security.Cryptography;
using Godot;

public partial class Jatek : Control
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetupPanels();
	}
	
	private void SetupPanels()
	{
		GetNode<Panel>("MainPanel").Hide();
		GetNode<Panel>("VilagKivalasztas").Show();

		var vilaglista = GetNode<OptionButton>("VilagKivalasztas/VBoxContainer/Vilagok");
		foreach (Vilag vilag in Global.Instance!.vilagok)
		{
			vilaglista.AddItem(vilag.nev);
		}

		var cardManager = GetNode<CardManager>("MainPanel/VBoxContainer/CenterContainer/HBoxContainer/Node2D");
		cardManager.CardsRerender += RerenderKartyak;
	}

	private void RerenderVilagkartyak(VBoxContainer panel)
	{
		var vilagkartyak = panel.GetNode<HFlowContainer>("Vilagkartyak");
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
		var vilagvezerek = panel.GetNode<HFlowContainer>("Vezerek");
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
		var kazamatak = panel.GetNode<HFlowContainer>("Kazamatak");
		foreach (Node child in kazamatak.GetChildren())
		{
			kazamatak.RemoveChild(child);
		}
		foreach (Kazamata kaza in Global.Instance!.aktivVilag!.kazamatak)
		{
			kazamatak.AddChild(KazaCard.CreateKaza(kaza));
		}
	}

	private void RerenderGyujtemeny(VBoxContainer panel)
	{
		Global.Instance!.aktivVilag!.jatekos.pakli ??= [];
		var gyujtemeny = panel.GetNode<HFlowContainer>("Gyujtemeny");
		foreach (Node child in gyujtemeny.GetChildren())
		{
			gyujtemeny.RemoveChild(child);
		}
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Where(kartya => Global.Instance!.aktivVilag!.jatekos.pakli!.Find(kx => kx.nev == kartya.nev) == null))
		{
			gyujtemeny.AddChild(Card.CreateKartya(kartya));
		}

		var cardManager = GetNode<CardManager>("MainPanel/VBoxContainer/CenterContainer/HBoxContainer/Node2D");
		cardManager.ConnectCards(cardManager.GetNode<HFlowContainer>("JatekosInfo/Gyujtemeny"));
	}

	private void RerenderPakli(VBoxContainer panel)
	{
		var pakli = panel.GetNode<HFlowContainer>("Pakli");
		foreach (Node child in pakli.GetChildren())
		{
			pakli.RemoveChild(child);
		}
		Global.Instance!.aktivVilag!.jatekos.pakli ??= [];
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.jatekos.pakli)
		{
			pakli.AddChild(Card.CreateKartya(kartya));
		}

		var cardManager = GetNode<CardManager>("MainPanel/VBoxContainer/CenterContainer/HBoxContainer/Node2D");
		cardManager.ConnectCards(cardManager.GetNode<HFlowContainer>("JatekosInfo/Pakli"));
		
		for (int i = 0; i < (Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Count + 1) / 2 - Global.Instance!.aktivVilag!.jatekos.pakli.Count; i++)
		{
			pakli.AddChild(CardHolder.CreateHolder());
		}
	}
	
	private void RerenderKartyak()
	{
		var panel = GetNode<VBoxContainer>("MainPanel/VBoxContainer/CenterContainer/HBoxContainer/VilagInfo");
		RerenderVilagkartyak(panel);
		RerenderVezerek(panel);
		RerenderKazamatak(panel);

		panel = GetNode<VBoxContainer>("MainPanel/VBoxContainer/CenterContainer/HBoxContainer/Node2D/JatekosInfo");
		RerenderGyujtemeny(panel);
		RerenderPakli(panel);
	}

	private void OnVilagKivalasztPressed()
	{
		var vilaglista = GetNode<OptionButton>("VilagKivalasztas/VBoxContainer/Vilagok");
		string selected = vilaglista.GetItemText(vilaglista.Selected);
		GD.Print("Selected option: " + selected);

		Global.Instance!.aktivVilag = Global.Instance!.vilagok.Find((vilag) => vilag.nev == selected)!;

		GetNode<Panel>("VilagKivalasztas").Hide();
		GetNode<Panel>("MainPanel").Show();
		RerenderKartyak();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
