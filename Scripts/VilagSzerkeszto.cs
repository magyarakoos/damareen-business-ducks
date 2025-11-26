using Godot;
using System;

public partial class VilagSzerkeszto : Control
{
	private Sprite2D? deleteIcon;
	private string? oldName;

	public override void _Ready()
	{
		oldName = Global.Instance!.aktivVilag!.nev;

		var name = GetNode<LineEdit>("Panel/VBoxContainer/CenterContainer2/Name/NameInput");
		name.Text = Global.Instance!.aktivVilag!.nev;

		RerenderKartyak();
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
			var card = Card.CreateKartya(kartya);

			card.GuiInput += @event =>
			{
				if (@event is InputEventMouseButton btn && btn.Pressed && btn.ButtonMask == MouseButtonMask.Left)
				{
					if (deleteIcon == null) return;
					deleteIcon.Hide();
					deleteIcon = null;

					Global.Instance!.aktivVilag!.jatekos.gyujtemeny = [.. Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Where(kx => kx.nev != kartya.nev)];
					Global.Instance!.aktivVilag!.vilagkartyak = [.. Global.Instance!.aktivVilag!.vilagkartyak.Where(kx => kx.nev != kartya.nev)];
					Global.Instance!.aktivVilag!.vilagvezerek = [.. Global.Instance!.aktivVilag!.vilagvezerek.Where(kx => kx.kartya.nev != kartya.nev)];
					card.EmitSignal(Card.SignalName.RerenderKartyak);
				}
			};

			card.RerenderKartyak += () =>
			{
				RerenderGyujtemeny(GetNode<VBoxContainer>("Panel/VBoxContainer/CenterContainer/ScrollContainer/HBoxContainer/Node2D/JatekosInfo"));
				RerenderVilagkartyak(panel);
				RerenderVezerek(panel);
			};

			vilagkartyak.AddChild(card);
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
			var card = Card.CreateVezer(vezer);

			card.GuiInput += @event =>
			{
				if (@event is InputEventMouseButton btn && btn.Pressed && btn.ButtonMask == MouseButtonMask.Left)
				{
					if (deleteIcon == null) return;
					deleteIcon.Hide();
					deleteIcon = null;

					Global.Instance!.aktivVilag!.vilagvezerek = [.. Global.Instance!.aktivVilag!.vilagvezerek.Where(vx => vx.nev != vezer.nev)];
					card.EmitSignal(Card.SignalName.RerenderKartyak);
				}
			};

			card.RerenderKartyak += () =>
			{
				RerenderVezerek(panel);
			};

			vilagvezerek.AddChild(card);
		}

		var button = panel.GetNode<Button>("VezerekTitle/VezerLetrehozo");
		if (Global.Instance!.aktivVilag!.vilagkartyak.Count == 0)
		{
			button.Hide();
		}
		else
		{
			button.Show();
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
			var card = KazamataCard.CreateKaza(kaza, false);
			
			card.GuiInput += @event =>
			{
				if (@event is InputEventMouseButton btn && btn.Pressed && btn.ButtonMask == MouseButtonMask.Left)
				{
					if (deleteIcon == null) return;
					deleteIcon.Hide();
					deleteIcon = null;

					Global.Instance!.aktivVilag!.kazamatak = [.. Global.Instance!.aktivVilag!.kazamatak.Where(kx => kx.nev != kaza.nev)];
					card.EmitSignal(Card.SignalName.RerenderKartyak);
				}
			};

			card.RerenderKartyak += () =>
			{
				RerenderKazamatak(panel);
			};

			kazamatak.AddChild(card);
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
			var card = Card.CreateKartya(kartya);

			card.GuiInput += @event =>
			{
				if (@event is InputEventMouseButton btn && btn.Pressed && btn.ButtonMask == MouseButtonMask.Left)
				{
					if (deleteIcon == null) return;
					deleteIcon.Hide();
					deleteIcon = null;

					Global.Instance!.aktivVilag!.jatekos.gyujtemeny = [.. Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Where(kx => kx.nev != kartya.nev)];
					card.EmitSignal(Card.SignalName.RerenderKartyak);
				}
			};

			card.RerenderKartyak += () =>
			{
				RerenderGyujtemeny(panel);
			};

			gyujtemeny.AddChild(card);
		}

		var button = panel.GetNode<Button>("GyujtemenyTitle/GyujtemenyHozzaado");
		if (Global.Instance!.aktivVilag!.vilagkartyak.Count == Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Count)
		{
			button.Hide();
		}
		else
		{
			button.Show();
		}
	}

	private void RerenderKartyak(bool jatekosOnly = false)
	{
		if (!jatekosOnly)
		{
			var vilagPanel = GetNode<VBoxContainer>("Panel/VBoxContainer/CenterContainer/ScrollContainer/HBoxContainer/VilagInfo");
			RerenderVilagkartyak(vilagPanel);
			RerenderVezerek(vilagPanel);
			RerenderKazamatak(vilagPanel);
		}

		var jatekosPanel = GetNode<VBoxContainer>("Panel/VBoxContainer/CenterContainer/ScrollContainer/HBoxContainer/Node2D/JatekosInfo");
		RerenderGyujtemeny(jatekosPanel);
	}

	private void OnElvetesButtonPressed()
	{
		Global.Instance!.aktivVilag = null;
		GetTree().ChangeSceneToFile("res://Scenes/jatekmester.tscn");
	}

	private void OnMentesButtonPressed()
	{
		string nev =  GetNode<LineEdit>("Panel/VBoxContainer/CenterContainer2/Name/NameInput").Text;
		if(nev.Contains(";"))
		{
			return;
			
		}
		if (nev.Length == 0)
		{
			GetNode<Label>("Panel/VBoxContainer/CenterContainer2/Error").Text = "A világnév nem lehet üres.";
			return;
		}

		Global.Instance!.aktivVilag!.nev = nev;

		VilagExport.Export(Global.Instance!.aktivVilag!);
		if (Global.Instance!.aktivVilag!.nev != oldName!)
		{
			VilagExport.Delete(oldName!);
		}
		OnElvetesButtonPressed();
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

	private void OnGyujtemenyHozzaadoPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/gyujtemeny_hozzaado.tscn");
	}

	private void OnKartyaLetrehozoPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/kartya_letrehozo.tscn");
	}

	private void OnVezerLetrehozoPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/vezer_letrehozo.tscn");
	}

	public override void _Process(double delta)
	{
		if (deleteIcon != null)
		{
			deleteIcon.Position = GetGlobalMousePosition();
		}
	}
}
