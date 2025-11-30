// Vezér vagy sima lapból származtatott, ezek segítségével történnek meg a harcok
public class Harckartya
{
	public string nev;
	public int sebzes;
	public int eletero;
	public KartyaTipus tipus;
	public bool isVoid = false;

	public Harckartya(Kartya kartya)
	{
		this.nev = kartya.nev;
		this.sebzes = kartya.sebzes;
		this.eletero = kartya.eletero;
		this.tipus = kartya.tipus;
	}

	public Harckartya(Vezer vezer)
	{
		this.nev = vezer.nev;
		this.sebzes = vezer.kartya.sebzes;
		this.eletero = vezer.kartya.eletero;
		this.tipus = vezer.kartya.tipus;

		switch (vezer.tipus)
		{
			case FejlesztesTipus.Sebzes:
				this.sebzes *= 2;
				break;
			case FejlesztesTipus.Eletero:
				this.eletero *= 2;
				break;
		}
	}

	public string Megut(Harckartya utokartya, double difficulty)
	{
		int valodi_sebzes = utokartya.tipus.Sebzes(this.tipus, utokartya.sebzes, difficulty, utokartya.isVoid, this.isVoid);
		this.eletero -= Math.Min(valodi_sebzes, this.eletero);
		return $"{utokartya.nev};{valodi_sebzes};{this.nev};{this.eletero}";
	}
	public List<string> CardFormat()
	{
		List<string> result = [];
		result.Add("");
		result.Add($"{this.nev}");
		result.Add($"{this.sebzes}/{this.eletero}");
		result.Add($"{this.tipus.ToString().ToLower()}");
		result.Add("");
		return result;
	}

	public string Info()
	{
		return $"{this.nev};{this.sebzes};{this.eletero};{this.tipus.ToString().ToLower()}";
	}

	public override string ToString()
	{
		return $"[Harcartya | nev: {this.nev} | sebzes: {this.sebzes} | eletero: {this.eletero} | tipus: {this.tipus}]";
	}
}
