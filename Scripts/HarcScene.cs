using Godot;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;

public partial class HarcScene : Control
{
	Harc? harc = null;
	int kor_i = 1;

	public override void _Ready()
	{
		var title = GetNode<Label>("Panel/VBoxContainer/Title");
		title.Text = Global.Instance!.aktivKaza!.nev;
		
		Vilag vilag = Global.Instance!.aktivVilag!;

		List<Vezer> vezerek = [];
		if (Global.Instance!.aktivKaza!.vezer != null)
		{
			vezerek.Add(Global.Instance!.aktivKaza!.vezer);
		}

		if (vilag.jatekos.pakli == null)
		{
			throw new InvalidOperationException("Pakli nelkul nem lehet harcolni.");
		}

		harc = new Harc(vilag.jatekos.pakli, Global.Instance!.aktivKaza!.kartyak, vezerek);

		var jatekos = GetNode<HFlowContainer>("Panel/VBoxContainer/CenterContainer/HBoxContainer/JatekosSide/Jatekos");
		var kaza = GetNode<HFlowContainer>("Panel/VBoxContainer/CenterContainer/HBoxContainer/KazaSide/Kaza");

		int i = 0;
		foreach (Harckartya kartya in harc!.kazamata.Reverse())
		{
			kaza.AddChild(Card.CreateHarckartya(kartya, i == 0 && Global.Instance!.aktivKaza!.tipus != KazamataTipus.Egyszeru));
			i++;
		}
		foreach (Harckartya kartya in harc!.jatekos)
		{
			jatekos.AddChild(Card.CreateHarckartya(kartya, false));
		}
	}

	public void RerenderCards()
	{
		var kazaAktiv = GetNode<Control>("Panel/VBoxContainer/CenterContainer/HBoxContainer/Panel/MiddleSide/HBoxContainer/KazaHolder");
		var jatekosAktiv = GetNode<Control>("Panel/VBoxContainer/CenterContainer/HBoxContainer/Panel/MiddleSide/HBoxContainer/JatekosHolder");
		var jatekos = GetNode<HFlowContainer>("Panel/VBoxContainer/CenterContainer/HBoxContainer/JatekosSide/Jatekos");
		var kaza = GetNode<HFlowContainer>("Panel/VBoxContainer/CenterContainer/HBoxContainer/KazaSide/Kaza");

		foreach (Node child in jatekos.GetChildren())
		{
			jatekos.RemoveChild(child);
		}
		foreach (Node child in jatekosAktiv.GetChildren())
		{
			jatekosAktiv.RemoveChild(child);
		}
		foreach (Node child in kazaAktiv.GetChildren())
		{
			kazaAktiv.RemoveChild(child);
		}
		foreach (Node child in kaza.GetChildren())
		{
			kaza.RemoveChild(child);
		}

		int i = 0;
		foreach (Harckartya kartya in harc!.kazamata.Reverse())
		{
			bool isVezer = i == 0 && Global.Instance!.aktivKaza!.tipus != KazamataTipus.Egyszeru;
			if (i + 1 == harc!.kazamata.Count && harc!.kazamata_aktiv)
			{
				kazaAktiv.AddChild(Card.CreateHarckartya(kartya, isVezer));	
			}
			else
			{
				var card = Card.CreateHarckartya(kartya, isVezer);
				kaza.AddChild(card);
			}
			i++;
		}

		i = 0;
		foreach (Harckartya kartya in harc!.jatekos)
		{
			if (i == 0 && harc!.jatekos_aktiv)
			{
				jatekosAktiv.AddChild(Card.CreateHarckartya(kartya, false));
			}
			else
			{
				jatekos.AddChild(Card.CreateHarckartya(kartya, false));
			}
			i++;
		}
	}

	public HarcAllapot Lepes()
	{
		if (harc!.jatekos.Count == 0 || harc!.kazamata.Count == 0)
		{
			AddMessage("A harc véget ért, már nem lehet lépni.");
			throw new InvalidOperationException("A harc mar befejezodott, nem lehet lepni.");
		}

		if (!harc!.kazamata_aktiv)
		{
			harc!.kazamata_aktiv = true;
			AddMessage($"A kazamata kijáttsza {harc!.kazamata.Peek().nev}-t.");
		}
		else
		{
			string msg = $"A kazamata támad {harc!.jatekos.Peek().Megut(harc!.kazamata.Peek()).Split(';')[1]} sebzéssel.";
			if (harc!.jatekos.Peek().eletero == 0)
			{
				harc!.jatekos.Dequeue();
				harc!.jatekos_aktiv = false;
				msg = msg.Substr(0, msg.Length - 1);
				msg += ", ebbe a játékos lapja belehal.";
				if (harc!.jatekos.Count == 0)
				{
					AddMessage(msg);
					return HarcAllapot.KazamataNyert;
				}
			}
			AddMessage(msg);
		}

		if (!harc!.jatekos_aktiv)
		{
			harc!.jatekos_aktiv = true;
			AddMessage($"A játékos kijáttsza {harc!.jatekos.Peek().nev}-t.");
		}
		else
		{
			string msg = $"A játékos támad {harc!.kazamata.Peek().Megut(harc!.jatekos.Peek()).Split(';')[1]} sebzéssel.";
			if (harc!.kazamata.Peek().eletero == 0)
			{
				harc!.kazamata.Dequeue();
				harc!.kazamata_aktiv = false;
				msg = msg.Substr(0, msg.Length - 1);
				msg += ", ebbe a kazamata lapja belehal.";
				
				if (harc!.kazamata.Count == 0)
				{
					AddMessage(msg);
					return HarcAllapot.JatekosNyert;
				}
			}
			AddMessage(msg);
		}

		return HarcAllapot.Aktiv;
	}

	private void AddMessage(string text)
	{
		GetNode<Label>("Panel/VBoxContainer/Message").Text += $"\n{text}";
	}

	private void ClearMessage()
	{
		GetNode<Label>("Panel/VBoxContainer/Message").Text = "";
	}

	public void OnLeptetButtonPressed()
	{
		ClearMessage();

		Vilag vilag = Global.Instance!.aktivVilag!;
		Kazamata kaza = Global.Instance!.aktivKaza!;

		HarcAllapot eredmeny = Lepes();
		kor_i++;
		RerenderCards();

		if (eredmeny == HarcAllapot.Aktiv)
		{
			return;
		}

		if (eredmeny == HarcAllapot.KazamataNyert)
		{
			AddMessage("A játékos vesztett.");
		}
		else
		{
			if (kaza.tipus == KazamataTipus.Nagy)
			{
				Kartya? uj_kartya = vilag.vilagkartyak.Find((item) => vilag.jatekos.gyujtemeny.Find((itx) => itx.nev == item.nev) == null);
				if (uj_kartya == null)
				{
					throw new InvalidOperationException("Nem lehet nagy kazamatazni, mert mar megvan az osszes kartya.");
				}
				AddMessage($"A játékos nyert, jutalmul kapja {uj_kartya.nev}-t");
				vilag.jatekos.gyujtemeny.Add(uj_kartya.Clone());
			}
			else
			{
				Kartya fejlodik = vilag.jatekos.gyujtemeny.Find((item) => item.nev == harc!.jatekos.Peek().nev)!;
				string msg = $"A játékos nyert, jutalmul {fejlodik.nev} fejlődik, ";
				switch (kaza.fejlesztes)
				{
					case FejlesztesTipus.Sebzes:
						fejlodik.sebzes += 1;
						msg += "+1 sebzést szerez.";
						break;
					case FejlesztesTipus.Eletero:
						msg += "+2 életerőt szerez.";
						fejlodik.eletero += 2;
						break;
					default:
						throw new UnreachableException();
				}
				AddMessage(msg);
			}
		}

		GetNode<Button>("Panel/KilepesButton").Show();
		GetNode<Button>("Panel/VBoxContainer/CenterContainer2/LeptetButton").Hide();
	}

	private void OnKilepesButtonPressed()
	{
		Global.Instance!.aktivKaza = null;
		GetTree().ChangeSceneToFile("res://Scenes/jatekos.tscn");
	}
}
