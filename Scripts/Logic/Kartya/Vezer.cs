public enum FejlesztesTipus
{
    Sebzes,
    Eletero,
}

public class Vezer
{
    public string nev;
    public Kartya kartya;
    public FejlesztesTipus tipus;

    public Vezer(string nev, Kartya kartya, FejlesztesTipus tipus)
    {
        this.nev = nev;
        this.kartya = kartya;
        this.tipus = tipus;
    }
    public Vezer(string input, Kartya kartya)
    {
        var tokens = input.Split(';');
        this.nev = tokens[1];
        this.kartya = kartya;
        this.tipus = tokens[3] switch
        {
            "sebzes" => FejlesztesTipus.Sebzes,
            "eletero" => FejlesztesTipus.Eletero,
            _ => throw new InvalidDataException(),
        };
    }

    public string Info()
    {
        return $"{this.nev};{this.kartya.nev};{this.tipus.ToString().ToLower()}";
    }

    public override string ToString()
    {
        return $"[Vezer | nev: {this.nev} | kartya: {this.kartya.nev} | fejlesztes: {this.tipus}]";
    }
}