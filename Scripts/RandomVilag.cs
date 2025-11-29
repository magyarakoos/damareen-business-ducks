using Godot;
using System;
using System.Diagnostics;

public partial class RandomVilag : Control
{
	// Called when the node enters the scene tree for the first time.
	private static readonly Random rng = new();

	private static readonly string[] kartyaNevek = [
		"cmwjwfee", "xkfgwveu", "yjtvkcnf", "nuegvydz", "wcmwrzuw", "azwwfkaz", "gokesism", "ibnmsacw", "tdwyeduq", "ooyhdzns",
		"vcbxzevb", "ekoedbgt", "fslneeih", "zvtemupm", "eybwejmr", "rgntdgbz", "oosclryl", "elgmrayr", "lgfpoils", "wggtrwkn",
		"xnmfqjdt", "botvcicl", "hsbppjrd", "szhupxpu", "dpauagtz", "kwkopstw", "mrygzeuv", "uwubqkny", "owhyjbnf", "ddaiplkd",
		"roxsouhn", "xrlimjkc", "ihxplyhi", "gfchrwih", "glbrkfnl", "djodlcdo", "ymiilahq", "ozzsqtix", "vgiwohtr", "shxtrbae",
		"nxcrmlfp", "gdvkncos", "vlvptfqx", "chfvyjrt", "vjtmaoov", "orexniav", "rjotdpjv", "gpcpkyop", "bpuvfoka", "jivtojeo",
		"qsrtseet", "gojbziuk", "jtvyeccw", "kyeuaosp", "rhjopwgs", "laumvlbm", "nrqobrmr", "tvzpkfvg", "vhipzblb", "cqcqevxj",
		"llgvahjl", "thxskwqy", "xaukbuis", "icfwrenx", "jywuybfk", "uyluqhol", "tybpqhhd", "lkenqswc", "njtfiyne", "fkecgiku",
		"awvuwfvg", "qgcxnnxx", "wvajkcwl", "dfedacll", "xgmcmftk", "mtfybkup", "kwzsuvub", "vrxtkvcj", "vhffisur", "hflvnyme",
		"qnrdlwzy", "kkaxxfod", "qzzaywzf", "shvivqlz", "elpttusz", "davjccto", "lcvmshxf", "btbsdoou", "gujsumlq", "jxqytwge",
		"zlscfqhc", "ydmagnay", "wuqbjfop", "fovrufjr", "kaaozhyy", "bnsvuewa", "cenakbkq", "uspaqreo", "oioopawn", "crsooovx"
	];

	private static readonly string[] vezerNevek = [
		"Lord ewufaelz", "Lord jczeexak", "Lord dymwfrbi", "Lord qgguekgk", "Lord smhsmujf", "Lord jowjngtw", "Lord taviyqrg", "Lord hvanejml", "Lord vwtfowbj", "Lord ualwapmh",
		"Lord lwsxwojp", "Lord vbpsbhbo", "Lord qidfmfhp", "Lord dvxxzaby", "Lord fmzrguir", "Lord ndcpvksl", "Lord vztskoqe", "Lord raoasfgk", "Lord slqxnumi", "Lord nfezoqyn",
		"Lord wilqrspx", "Lord rtddilit", "Lord feepfzub", "Lord tnljxqup", "Lord exmogoaw", "Lord ciaxazpx", "Lord dquelfvw", "Lord oqwxvrjy", "Lord fokphbpp", "Lord scasalss",
		"Lord hsmhifom", "Lord txnlilwl", "Lord llnkgafu", "Lord igusicyc", "Lord mzfetpqi", "Lord fscgohhb", "Lord jxkdhfzi", "Lord sztbdofk", "Lord nultexiz", "Lord wkvrrxvf",
		"Lord qauoasal", "Lord folaxdol", "Lord kqvykxes", "Lord grbajmgw", "Lord kbeyrcop", "Lord cxyervfc", "Lord zyjshpmu", "Lord tnokirkv", "Lord ukmoiqgn", "Lord hzbzkqpg",
		"Lord vwntehct", "Lord nsewozup", "Lord yozekqvp", "Lord uvqhniws", "Lord frkzofma", "Lord jmulobfg", "Lord lydoqlxj", "Lord dacwxlru", "Lord jbkorenx", "Lord dhlcjfcl",
		"Lord hjbbgzvl", "Lord cdrdotsg", "Lord jijfpacr", "Lord zkagunog", "Lord ycyqxiri", "Lord pgaokzds", "Lord jcgcdgcb", "Lord wrhaybqq", "Lord siacopbu", "Lord qwdshewb",
		"Lord rdzqqoip", "Lord uldemuam", "Lord beyydrcj", "Lord irbrngds", "Lord mbpxoyuq", "Lord qpalbqea", "Lord gkofbdco", "Lord nftjvvrk", "Lord gnsvfqhj", "Lord vrgxyrij",
		"Lord cdauoqyf", "Lord ghonhplh", "Lord rlnpnwls", "Lord ebxvveys", "Lord qallhuvd", "Lord jvxpkvmw", "Lord pztwwrez", "Lord fuvkczld", "Lord gjlrtdgj", "Lord rbdxmbey",
		"Lord ictwlhhw", "Lord jbkjvxez", "Lord bpklfurr", "Lord isgcmnru", "Lord pdfxsuwj", "Lord yizkjwxd", "Lord cqskfnmg", "Lord fkwsahug", "Lord kudszpuz", "Lord kxgnvsiy"
	];

	private static readonly string[] kazaNevek = [
		"syixqieztzpyqyl", "ockeqhdshpdchpr", "earqhkstdblgest", "kywefligtkmppju", "holyrxbdauuhudd",
		"iztywludylmiklf", "byqbftclyfsdhcg", "doyekhmqeaxxzzu", "sfspqjsqfbygkic", "bmbzlcyergoijel",
		"jzmpbtwxwimahvl", "knokntnniyyddnz", "yirlziyqxjvynew", "rfablbknmjcbqmw", "bqhubxfzgzddwrh",
		"toitdbgkmsbskwl", "cvkqfkgtlafzank", "ejlzzdiefeigetb", "yoelttcmiuxixol", "ngeiinrajxbxpmo",
		"zgjolfjacecolbi", "oubbpeemxvyajlu", "mdbpxsjralcxhll", "ztthjogselvvnme", "cjxvcqjoojeqqnu",
		"jnggwulumdjnqdb", "mqvmpbtiztppvub", "sjpzbfenyphoivk", "hudcraxrdqepxzw", "hykdwffpkdoyxsa"
	];

	public override void _Ready()
	{
		var kartyaInput = GetNode<SpinBox>("Panel/VBoxContainer/KartyaContainer/KartyaInput");
		var gyujtemenyInput = GetNode<SpinBox>("Panel/VBoxContainer/GyujtemenyContainer/GyujtemenyInput");

		kartyaInput.ValueChanged += value =>
		{
			gyujtemenyInput.MaxValue = value;
		};
	}

	private static T PickRandom<T>(IList<T> list)
	{
		return list[rng.Next(list.Count)];
	}

	private static List<T> PickNRandom<T>(IEnumerable<T> list, int n)
	{
		return [.. list.OrderBy(_ => rng.Next()).Take(n)];
	}

	private Kartya RandomKartya(string nev)
	{
		return new Kartya(nev, rng.Next(2, 101), rng.Next(1, 101), rng.Next(4) switch
		{
			0 => KartyaTipus.Tuz,
			1 => KartyaTipus.Fold,
			2 => KartyaTipus.Viz,
			3 => KartyaTipus.Levego,
			_ => throw new UnreachableException(),
		});
	}

	private Vezer RandomVezer(string nev, Kartya kartya)
	{
		return new Vezer(nev, kartya, rng.Next(2) switch
		{
			0 => FejlesztesTipus.Sebzes,
			1 => FejlesztesTipus.Eletero,
			_ => throw new UnreachableException(),
		});
	}

	private Kazamata RandomKaza(string nev, Vilag vilag)
	{
		List<KazamataTipus> options = [];
		if (vilag.vilagkartyak.Count >= 5 && vilag.vilagvezerek.Count >= 1)
		{
			options.Add(KazamataTipus.Nagy);
		}
		if (vilag.vilagkartyak.Count >= 3 && vilag.vilagvezerek.Count >= 1)
		{
			options.Add(KazamataTipus.Kis);
		}
		if (vilag.vilagkartyak.Count >= 1)
		{
			options.Add(KazamataTipus.Egyszeru);
		}

		var tipus = PickRandom(options);
		List<Kartya> kartyak = [];
		Vezer? vezer = null;
		FejlesztesTipus? fejlesztes = null;

		switch (tipus)
		{
			case KazamataTipus.Egyszeru:
				kartyak = PickNRandom(vilag.vilagkartyak, 1);
				fejlesztes = rng.Next(2) switch
				{
					0 => FejlesztesTipus.Sebzes,
					1 => FejlesztesTipus.Eletero,
					_ => throw new UnreachableException(),
				};
				break;
			case KazamataTipus.Kis:
				kartyak = PickNRandom(vilag.vilagkartyak, 3);
				vezer = PickRandom(vilag.vilagvezerek);
				fejlesztes = rng.Next(2) switch
				{
					0 => FejlesztesTipus.Sebzes,
					1 => FejlesztesTipus.Eletero,
					_ => throw new UnreachableException(),
				};
				break;
			case KazamataTipus.Nagy:
				kartyak = PickNRandom(vilag.vilagkartyak, 5);
				vezer = PickRandom(vilag.vilagvezerek);
				break;
			default:
				throw new UnreachableException();
		}

		return new Kazamata(tipus, nev, kartyak, vezer, fejlesztes);
	}

	private Vilag VilagGeneral(string nev, int kartyak, int vezer, int gyujtemeny, int kaza)
	{
		var vilag = new Vilag
		{
			nev = nev
		};

		foreach (string kartyaNev in PickNRandom(kartyaNevek, kartyak))
		{
			vilag.vilagkartyak.Add(RandomKartya(kartyaNev));
		}

		foreach (Kartya kartya in PickNRandom(vilag.vilagkartyak, gyujtemeny))
		{
			vilag.jatekos.gyujtemeny.Add(kartya.Clone());
		}

		foreach (string vezerNev in PickNRandom(vezerNevek, vezer))
		{
			vilag.vilagvezerek.Add(RandomVezer(vezerNev, PickRandom(vilag.vilagkartyak)));
		}

		foreach (string kazaNev in PickNRandom(kazaNevek, kaza))
		{
			vilag.kazamatak.Add(RandomKaza(kazaNev, vilag));
		}

		return vilag;
	}

	private void OnLetrehozButtonPressed()
	{
		var nevInput = GetNode<LineEdit>("Panel/VBoxContainer/NevContainer/NevInput");
		var kartyaInput = GetNode<SpinBox>("Panel/VBoxContainer/KartyaContainer/KartyaInput");
		var gyujtemenyInput = GetNode<SpinBox>("Panel/VBoxContainer/GyujtemenyContainer/GyujtemenyInput");
		var vezerInput = GetNode<SpinBox>("Panel/VBoxContainer/VezerContainer/VezerInput");
		var kazaInput = GetNode<SpinBox>("Panel/VBoxContainer/KazaContainer/KazaInput");

		string? error = null;
		if (nevInput.Text.Length == 0)
		{
			error = "A név nem lehet üres.";
		}
		else if (nevInput.Text.Contains(';'))
		{
			error = "A névben nem lehet \";\" karakter.";
		}
		else if (Global.Instance!.vilagok.Find(vilag => vilag.nev == nevInput.Text) != null)
		{
			error = "Létezik már világ ezzel a névvel.";
		}

		if (error != null)
		{
			GetNode<Label>("Panel/VBoxContainer/Error").Text = error;
			return;
		}

		var vilag = VilagGeneral(nevInput.Text, (int)kartyaInput.Value, (int)vezerInput.Value, (int)gyujtemenyInput.Value, (int)kazaInput.Value);
		VilagExport.Export(vilag);

		OnMegsemButtonPressed();
	}

	private void OnMegsemButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/jatekmester.tscn");
	}
}
