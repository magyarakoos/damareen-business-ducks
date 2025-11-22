using Godot;
using System;
using System.Diagnostics;

public partial class KazaViewer : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var panel = GetNode<VBoxContainer>("Panel/VBoxContainer");
		var kaza = Global.Instance!.aktivKaza!;
		panel.GetNode<Label>("Title").Text = kaza.nev;
		panel.GetNode<Label>("Type").Text = KazamataCard.CreateKaza(kaza).GetNode<Label>("KazaType").Text;

		var kartyaPanel = panel.GetNode<HBoxContainer>("HBoxContainer");
		
		var enemies = kartyaPanel.GetNode<HFlowContainer>("EnemiesControl/Enemies");
		foreach (Kartya kartya in kaza.kartyak)
		{
			enemies.AddChild(Card.CreateKartya(kartya));
		}
		if (kaza.vezer != null)
		{
			enemies.AddChild(Card.CreateVezer(kaza.vezer));
		}

		var jatekosLapok = kartyaPanel.GetNode<HFlowContainer>("JatekosControl/JatekosLapok");
		foreach (Kartya kartya in Global.Instance!.aktivVilag!.jatekos.pakli!)
		{
			jatekosLapok.AddChild(Card.CreateKartya(kartya));
		}

		var reward = panel.GetNode<Control>("Reward");
		if (kaza.fejlesztes == null)
		{
			Kartya uj_kartya = Global.Instance!.aktivVilag!.vilagkartyak.Find((item) => Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Find((itx) => itx.nev == item.nev) == null)!;
			reward.AddChild(Card.CreateKartya(uj_kartya));
		}
		else
		{
			reward.AddChild(new Label()
			{
				Text = kaza.fejlesztes! switch
				{
					FejlesztesTipus.Sebzes => "Az utolsó ütést leadó kártya fejlődik, +1 sebzést szerez.",
					FejlesztesTipus.Eletero => "Az utolsó ütést leadó kártya fejlődik, +2 életerőt szerez.",
					_ => throw new UnreachableException(),
				},
			});
		}
	}

	private void OnVisszaButtonPressed()
	{
		Global.Instance!.aktivKaza = null;
		GetTree().ChangeSceneToFile("res://Scenes/jatekos.tscn");
	}

	private void OnFightButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/harc.tscn");
	}
}
