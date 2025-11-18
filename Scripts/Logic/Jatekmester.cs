public class Jatekmester
{

    public static Vilag Parse(string[] input, string base_path)
    {
        Vilag vilag = new();

        foreach (string line in input)
        {
            if (line.Length == 0)
            {
                continue;
            }

            string[] tokens = line.Split(';');

            switch (tokens[0])
            {
                case "uj kartya":
                    vilag.vilagkartyak.Add(new Kartya(line));
                    break;
                case "uj vezer":
                    Kartya? kartya = vilag.vilagkartyak.Find((item) => item.nev == tokens[2]);
                    if (kartya == null)
                    {
                        throw new InvalidDataException("Nincs ilyen kártya.");
                    }
                    vilag.vilagvezerek.Add(new Vezer(line, kartya));
                    break;
                case "uj kazamata":
                    List<Kartya> kartyak = [];
                    Vezer? vezer = null;

                    foreach (string nev in tokens[3].Split(','))
                    {
                        Kartya? kartya1 = vilag.vilagkartyak.Find((item) => item.nev == nev);
                        if (kartya1 == null)
                        {
                            throw new InvalidDataException("Nincs ilyen kártya.");
                        }
                        kartyak.Add(kartya1);
                    }

                    if (tokens[1] != "egyszeru")
                    {
                        Vezer? vezer1 = vilag.vilagvezerek.Find((item) => item.nev == tokens[4]);
                        if (vezer1 == null)
                        {
                            throw new InvalidDataException("Nincs ilyen vezérkártya.");
                        }
                        vezer = vezer1;
                    }
                    vilag.kazamatak.Add(new Kazamata(line, kartyak, vezer));

                    break;
                case "uj jatekos":
                    // itt nincs semmi
                    break;
                case "felvetel gyujtemenybe":
                    Kartya? kartya2 = vilag.vilagkartyak.Find((item) => item.nev == tokens[1]);
                    if (kartya2 == null)
                    {
                        throw new InvalidDataException("Nincs ilyen kártya.");
                    }
                    vilag.jatekos.gyujtemeny.Add(kartya2.Clone());
                    break;
                case "uj pakli":
                    vilag.jatekos.UjPakli(line);
                    break;
                case "harc":
                    File.WriteAllLines(Path.Combine(base_path, tokens[2]), vilag.Harc(line));
                    break;
                case "export vilag":
                    File.WriteAllLines(Path.Combine(base_path, tokens[1]), vilag.Export());
                    break;
                case "export jatekos":
                    File.WriteAllLines(Path.Combine(base_path, tokens[1]), vilag.jatekos.Export());
                    break;
                default:
                    throw new InvalidDataException();
            }
        }
        return vilag;
    }
}