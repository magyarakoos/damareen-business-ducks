using System.Diagnostics;
using Godot;

public enum KartyaTipus
{
	Fold,
	Levego,
	Viz,
	Tuz,
	Void,
}

public static class Extensions
{
	public static int Sebzes(this KartyaTipus enyem, KartyaTipus ellenseg, int sebzes)
	{
		if (enyem == ellenseg)
		{
			return sebzes;
		}
		switch (enyem)
		{
			case KartyaTipus.Levego:
			case KartyaTipus.Tuz:
				if (ellenseg == KartyaTipus.Fold || ellenseg == KartyaTipus.Viz)
				{
					return 2 * sebzes;
				}
				return sebzes / 2;
			case KartyaTipus.Fold:
			case KartyaTipus.Viz:
				if (ellenseg == KartyaTipus.Levego || ellenseg == KartyaTipus.Tuz)
				{
					return 2 * sebzes;
				}
				return sebzes / 2;
		}
		throw new UnreachableException();
	}
}


public class Kartya
{
	public string nev;
	public int sebzes;
	public int eletero;
	public KartyaTipus tipus;

	public Kartya(string nev, int sebzes, int eletero, KartyaTipus tipus)
	{
		this.nev = nev;
		this.sebzes = sebzes;
		this.eletero = eletero;
		this.tipus = tipus;
	}

	public Kartya(string input)
	{
		var tokens = input.Split(';');
		this.nev = tokens[1];
		this.sebzes = int.Parse(tokens[2]);
		this.eletero = int.Parse(tokens[3]);
		this.tipus = tokens[4] switch
		{
			"fold" => KartyaTipus.Fold,
			"levego" => KartyaTipus.Levego,
			"viz" => KartyaTipus.Viz,
			"tuz" => KartyaTipus.Tuz,
			"void" => KartyaTipus.Void,
			_ => throw new InvalidDataException(),
		};
	}

	public override string ToString()
	{
		return $"[Kartya | nev: {this.nev} | sebzes: {this.sebzes} | eletero: {this.eletero} | tipus: {this.tipus}]";
	}

	public Kartya Clone()
	{
		return new Kartya(this.nev, this.sebzes, this.eletero, this.tipus);
	}
}
