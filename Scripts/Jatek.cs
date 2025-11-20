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
	}
	
	private void AddKartyak()
	{
		var panel = GetNode<VBoxContainer>("MainPanel/VBoxContainer/CenterContainer/HBoxContainer/VilagInfo");

		var vilagkartyak = panel.GetNode<HFlowContainer>("Vilagkartyak");
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.vilagkartyak)
		{
			vilagkartyak.AddChild(Card.CreateKartya(kartya));
		}
		
		var vilagvezerek = panel.GetNode<HFlowContainer>("Vezerek");
		foreach (Vezer vezer in Global.Instance!.aktivVilag!.vilagvezerek)
		{
			vilagvezerek.AddChild(Card.CreateVezer(vezer));
		}

		var kazamatak = panel.GetNode<HFlowContainer>("Kazamatak");
		foreach (Kazamata kaza in Global.Instance!.aktivVilag!.kazamatak)
		{
			kazamatak.AddChild(KazaCard.CreateKaza(kaza));
		}

		panel = GetNode<VBoxContainer>("MainPanel/VBoxContainer/CenterContainer/HBoxContainer/JatekosInfo");

		var gyujtemeny = panel.GetNode<HFlowContainer>("Gyujtemeny");
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.jatekos.gyujtemeny)
		{
			gyujtemeny.AddChild(Card.CreateKartya(kartya));
		}

		Global.Instance!.aktivVilag!.jatekos.pakli ??= [];

		// var pakli = panel.GetNode<HFlowContainer>("Pakli");
		// foreach (Kartya kartya in Global.Instance!.aktivVilag!.jatekos.pakli)
		// {
		// 	pakli.AddChild(Card.CreateKartya(kartya));
		// }

		var pakli = panel.GetNode<HFlowContainer>("Pakli");
		for (int i = 0; i < (Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Count + 1) / 2 + 2; i++)
		{
			pakli.AddChild(CardHolder.CreateHolder());
		}
	}

	private void OnVilagKivalasztPressed()
	{
		var vilaglista = GetNode<OptionButton>("VilagKivalasztas/VBoxContainer/Vilagok");
		string selected = vilaglista.GetItemText(vilaglista.Selected);
		GD.Print("Selected option: " + selected);

		Global.Instance!.aktivVilag = Global.Instance!.vilagok.Find((vilag) => vilag.nev == selected)!;

		GetNode<Panel>("VilagKivalasztas").Hide();
		GetNode<Panel>("MainPanel").Show();
		AddKartyak();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
