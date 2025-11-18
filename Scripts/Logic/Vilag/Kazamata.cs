public enum KazamataTipus
{
    Egyszeru,
    Kis,
    Nagy,
}

public class Kazamata
{
    public KazamataTipus tipus;
    public string nev;
    public List<Kartya> kartyak;
    public Vezer? vezer;
    public FejlesztesTipus? fejlesztes;

    public Kazamata(KazamataTipus tipus, string nev, List<Kartya> kartyak, Vezer? vezer, FejlesztesTipus? fejlesztes)
    {
        this.tipus = tipus;
        this.nev = nev;
        this.kartyak = kartyak;
        this.vezer = vezer;
        this.fejlesztes = fejlesztes;
    }

    public Kazamata(string input, List<Kartya> kartyak, Vezer? vezer)
    {
        string[] tokens = input.Split(';');
        this.tipus = tokens[1] switch
        {
            "egyszeru" => KazamataTipus.Egyszeru,
            "kis" => KazamataTipus.Kis,
            "nagy" => KazamataTipus.Nagy,
            _ => throw new InvalidDataException(),
        };
        this.nev = tokens[2];
        this.kartyak = kartyak;
        this.vezer = vezer;
        if (this.tipus != KazamataTipus.Nagy)
        {
            this.fejlesztes = tokens[tokens.Length - 1] switch
            {
                "eletero" => FejlesztesTipus.Eletero,
                "sebzes" => FejlesztesTipus.Sebzes,
                _ => throw new InvalidDataException(),
            };
        }
        else
        {
            this.fejlesztes = null;
        }
    }

    public string Info()
    {
        string kartyakStr = string.Join(',', this.kartyak.Select(k => k.nev));
        string result = $"{this.tipus.ToString()!.ToLower()};{this.nev};{kartyakStr}";
        if (this.vezer != null)
        {
            result += $";{this.vezer.nev}";
        }
        if (this.fejlesztes != null)
        {
            result += $";{this.fejlesztes.ToString()!.ToLower()}";
        }
        return result;
    }

    public List<string> CardFormat()
    {
        List<string> result = [];
        result.Add("");
        result.Add($"{this.nev}");
        result.Add($"{this.tipus} kazamata");
        result.Add("");
        return result;
    }

    public override string ToString()
    {
        string kartyakStr = string.Join(", ", this.kartyak.Select(k => k.nev));
        string vezerStr = this.vezer != null ? this.vezer.nev : "nincs";
        string fejlesztesStr = this.fejlesztes != null ? this.fejlesztes.ToString()! : "nincs";

        return $"[Kazamata | nev: {this.nev} | tipus: {this.tipus} | kartyak: [{kartyakStr}] | vezer: {vezerStr} | fejlesztes: {fejlesztesStr}]";
    }

}