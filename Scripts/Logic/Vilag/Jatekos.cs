public class Jatekos
{
	public List<Kartya> gyujtemeny;
	public List<Kartya>? pakli;

	public Jatekos()
	{
		this.gyujtemeny = [];
		this.pakli = null;
	}

	public Jatekos(Jatekos masik)
	{
		this.gyujtemeny = [];
		foreach (Kartya kartya in masik.gyujtemeny)
		{
			this.gyujtemeny.Add(kartya.Clone());
		}
		if (masik.pakli != null)
		{
			this.pakli = [];
			foreach (Kartya kartya in masik.pakli)
			{
				this.pakli.Add(kartya.Clone());
			}
		}
		else
		{
			this.pakli = null;
		}
	}

	public void UjPakli(string input)
	{
		var nevek = input.Split(';')[1].Split(',');
		List<Kartya> kartyak = [];
		foreach (string nev in nevek)
		{
			Kartya? kartya = this.gyujtemeny.Find((item) => item.nev == nev);
			if (kartya == null)
			{
				throw new InvalidOperationException("Nincs ilyen kartya a gyujtemenyedben.");
			}
			if (kartyak.Count == (gyujtemeny.Count + 1) / 2)
			{
				throw new InvalidOperationException("Ezt a kartyat mar nem teheted a paklidba.");
			}
			kartyak.Add(kartya);
		}
		this.pakli = kartyak;
	}

	public List<string> Export()
	{
		List<string> log = [];
		foreach (Kartya kartya in this.gyujtemeny)
		{
			log.Add($"gyujtemeny;{new Harckartya(kartya).Info()}");
		}
		if (this.pakli != null && this.pakli.Count > 0)
		{
			log.Add("");
			foreach (Kartya kartya in this.pakli)
			{
				log.Add($"pakli;{kartya.nev}");
			}
		}
		return log;
	}

	public override string ToString()
	{
		string gyujtStr = string.Join(", ", this.gyujtemeny.Select(k => k.nev));
		string pakliStr = this.pakli != null
			? string.Join(", ", this.pakli.Select(k => k.nev))
			: "nincs";

		return $"[Jatekos | gyujtemeny: [{gyujtStr}] | pakli: [{pakliStr}]]";
	}
}
