using System.Collections.Generic;
using Godot;

public class Vilag
{
	public string nev;
	public List<Kartya> vilagkartyak;
	public List<Vezer> vilagvezerek;
	public List<Kazamata> kazamatak;
	public Jatekos jatekos;

	public int difficulty = 0;

	public Vilag()
	{
		this.nev = "";
		this.vilagkartyak = [];
		this.vilagvezerek = [];
		this.kazamatak = [];
		this.jatekos = new Jatekos();
	}

	public Vilag(Vilag masik)
	{
		this.nev = masik.nev;
		this.vilagkartyak = [];
		foreach (Kartya kartya in masik.vilagkartyak)
		{
			this.vilagkartyak.Add(kartya.Clone());
		}
		this.vilagvezerek = [];
		foreach (Vezer vezer in masik.vilagvezerek)
		{
			this.vilagvezerek.Add(new Vezer(vezer));
		}
		this.kazamatak = [];
		foreach (Kazamata kaza in masik.kazamatak)
		{
			this.kazamatak.Add(new Kazamata(kaza));
		}
		this.jatekos = new Jatekos(masik.jatekos);
	}

	public List<string> Harc(string input)
	{
		var tokens = input.Split(';');
		Kazamata? kaza = this.kazamatak.Find((item) => item.nev == tokens[1]);
		if (kaza == null)
		{
			throw new InvalidDataException("Nincs ilyen kazamata.");
		}
		List<string> log = [];

		log.Add($"harc kezdodik;{kaza.nev}");
		log.Add("");

		List<Vezer> vezerek = [];
		if (kaza.vezer != null)
		{
			vezerek.Add(kaza.vezer);
		}

		if (this.jatekos.pakli == null)
		{
			throw new InvalidOperationException("Pakli nelkul nem lehet harcolni.");
		}

		Harc harc = new Harc(this.jatekos.pakli, kaza.kartyak, vezerek, difficulty);

		HarcAllapot vegeredmeny = HarcAllapot.Aktiv;
		for (int kor_i = 1; ; kor_i++)
		{	
			HarcAllapot eredmeny = harc.Lepes(log, kor_i);
			if (eredmeny != HarcAllapot.Aktiv)
			{
				vegeredmeny = eredmeny;
				break;
			}
		}

		if (vegeredmeny == HarcAllapot.KazamataNyert)
		{
			log.Add("jatekos vesztett");
		}
		else
		{
			Kartya fejlodik = this.jatekos.gyujtemeny.Find((item) => item.nev == harc.jatekos.Peek().nev)!;
			if (kaza.tipus == KazamataTipus.Nagy)
			{
				Kartya? uj_kartya = this.vilagkartyak.Find((item) => this.jatekos.gyujtemeny.Find((itx) => itx.nev == item.nev) == null);
				if (uj_kartya == null)
				{
					throw new InvalidOperationException("Nem lehet nagy kazamatazni, mert mar megvan az osszes kartya.");
				}
				log.Add($"jatekos nyert;{uj_kartya.nev}");
				this.jatekos.gyujtemeny.Add(uj_kartya.Clone());
			}
			else
			{
				switch (kaza.fejlesztes)
				{
					case FejlesztesTipus.Sebzes:
						fejlodik.sebzes += 1;
						break;
					case FejlesztesTipus.Eletero:
						fejlodik.eletero += 2;
						break;
				}
				log.Add($"jatekos nyert;{kaza.fejlesztes.ToString()!.ToLower()};{fejlodik.nev}");
			}
		}

		return log;
	}

	public List<string> Export()
	{
		List<string> log = [];
		foreach (Kartya kartya in this.vilagkartyak)
		{
			log.Add($"kartya;{new Harckartya(kartya).Info()}");
		}
		if (this.vilagvezerek.Count > 0)
		{
			log.Add("");
			foreach (Vezer vezer in this.vilagvezerek)
			{
				log.Add($"vezer;{new Harckartya(vezer).Info()}");
			}
		}
		if (this.kazamatak.Count > 0)
		{
			log.Add("");
			foreach (Kazamata kaza in this.kazamatak)
			{
				log.Add($"kazamata;{kaza.Info()}");
			}
		}
		return log;
	}
	// This kind of export also writes the origin of each Vezer. DO NOT use this in test mode!!!!
	public List<string> ExportExtra()
	{
		List<string> log = [];
		log.Add(this.nev);
		foreach (Kartya kartya in this.vilagkartyak)
		{
			log.Add($"kartya;{new Harckartya(kartya).Info()}");
		}
		if (this.vilagvezerek.Count > 0)
		{
			log.Add("");
			foreach (Vezer vezer in this.vilagvezerek)
			{
				log.Add($"vezer;{vezer.Info()}");
			}
		}
		if (this.kazamatak.Count > 0)
		{
			log.Add("");
			foreach (Kazamata kaza in this.kazamatak)
			{
				log.Add($"kazamata;{kaza.Info()}");
			}
		}
		List<string> jatekosLog = this.jatekos.Export();
		log.AddRange(jatekosLog);
		return log;
	}

	public Vilag(string[] data) : this()
	{
		this.nev = data[0];
		data = data.Skip(1).ToArray();
		foreach (string line in data)
		{
			var tokens = line.Split(';');

			switch (tokens[0])
			{
				case "":
					break;
				case "kartya":
					Kartya kartya = new Kartya(line);
					this.vilagkartyak.Add(kartya);
					break;
				case "vezer":
					Kartya? kartya2 = this.vilagkartyak.Find((item) => item.nev == tokens[2]);
					if (kartya2 == null)
					{
						throw new InvalidDataException($"Nincs ilyen kártya: {tokens[2]} ({line})");
					}
					this.vilagvezerek.Add(new Vezer(line, kartya2));
					break;
				case "kazamata":
					List<Kartya> kartyak = [];
					Vezer? vezer = null;

					foreach (string nev in tokens[3].Split(','))
					{
						Kartya? kartya1 = this.vilagkartyak.Find((item) => item.nev == nev);
						if (kartya1 == null)
						{
							throw new InvalidDataException($"Nincs ilyen kártya: {nev} ({line}) ({string.Join(',', this.vilagkartyak.Select(kartya => kartya.nev))})");
						}
						kartyak.Add(kartya1);
					}

					if (tokens[1] != "egyszeru")
					{
						Vezer? vezer1 = this.vilagvezerek.Find((item) => item.nev == tokens[4]);
						if (vezer1 == null)
						{
							throw new InvalidDataException("Nincs ilyen vezérkártya.");
						}
						vezer = vezer1;
					}
					this.kazamatak.Add(new Kazamata(line, kartyak, vezer));
					break;
				case "gyujtemeny":
					Kartya? kartya3 = this.vilagkartyak.Find((item) => item.nev == tokens[1]);
					if (kartya3 == null)
					{
						throw new InvalidDataException("Nincs ilyen kártya.");
					}
					this.jatekos.gyujtemeny.Add(new Kartya(line));
					break;
				case "pakli":
					// We don't import pakli information, that's not actually part of a world config
					break;
				default:
					throw new InvalidDataException(line);
			}
		}
	}

	public override string ToString()
	{
		string kartyakStr = this.vilagkartyak.Count > 0
			? string.Join("\n        ", this.vilagkartyak.Select(k => k.ToString()))
			: "nincs";

		string vezerekStr = this.vilagvezerek.Count > 0
			? string.Join("\n        ", this.vilagvezerek.Select(v => v.ToString()))
			: "nincs";

		string kazamatakStr = this.kazamatak.Count > 0
			? string.Join("\n        ", this.kazamatak.Select(k => k.ToString()))
			: "nincs";

		string jatekosStr = this.jatekos != null
			? this.jatekos.ToString()
			: "nincs";

		return
	$@"[Vilag]
    Vilagkartyak:
        {kartyakStr}
    Vilagvezerek:
        {vezerekStr}
    Kazamatak:
        {kazamatakStr}
    Jatekos:
		{jatekosStr}";
	}

}
