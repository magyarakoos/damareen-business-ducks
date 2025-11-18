public enum HarcAllapot
{
    Aktiv,
    JatekosNyert,
    KazamataNyert,
}

public class Harc
{
    public Queue<Harckartya> jatekos;
    public Queue<Harckartya> kazamata;
    public bool jatekos_aktiv;
    public bool kazamata_aktiv;
    public Harc(List<Kartya> jatekos_kartya, List<Kartya> kazamata_kartya, List<Vezer> kazamata_vezer)
    {
        this.jatekos = [];
        this.kazamata = [];
        this.jatekos_aktiv = false;
        this.kazamata_aktiv = false;
        foreach (Kartya kartya in jatekos_kartya)
        {
            jatekos.Enqueue(new Harckartya(kartya));
        }
        foreach (Kartya kartya in kazamata_kartya)
        {
            kazamata.Enqueue(new Harckartya(kartya));
        }
        foreach (Vezer vezer in kazamata_vezer)
        {
            kazamata.Enqueue(new Harckartya(vezer));
        }
    }

    public HarcAllapot Lepes(List<string> log, int kor_i)
    {
        if (jatekos.Count == 0 || kazamata.Count == 0)
        {
            throw new InvalidOperationException("A harc mar befejezodott, nem lehet lepni.");
        }

        if (!kazamata_aktiv)
        {
            kazamata_aktiv = true;
            log.Add($"{kor_i}.kor;kazamata;kijatszik;{kazamata.Peek().Info()}");
        }
        else
        {
            log.Add($"{kor_i}.kor;kazamata;tamad;{jatekos.Peek().Megut(kazamata.Peek())}");
            if (jatekos.Peek().eletero == 0)
            {
                jatekos.Dequeue();
                jatekos_aktiv = false;
                if (jatekos.Count == 0)
                {
                    log.Add("");
                    return HarcAllapot.KazamataNyert;
                }
            }
        }

        if (!jatekos_aktiv)
        {
            jatekos_aktiv = true;
            log.Add($"{kor_i}.kor;jatekos;kijatszik;{jatekos.Peek().Info()}");
        }
        else
        {
            log.Add($"{kor_i}.kor;jatekos;tamad;{kazamata.Peek().Megut(jatekos.Peek())}");
            if (kazamata.Peek().eletero == 0)
            {
                kazamata.Dequeue();
                kazamata_aktiv = false;
                if (kazamata.Count == 0)
                {
                    log.Add("");
                    return HarcAllapot.JatekosNyert;
                }
            }
        }

        log.Add("");
        return HarcAllapot.Aktiv;
    }
}