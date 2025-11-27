using Godot;

public class VilagExport
{
	public static readonly string basePath = "user://vilagok/";
	public static void MakeVilagokDir()
	{
		DirAccess dir = DirAccess.Open("user://");
		Error _ = dir.MakeDirRecursive(basePath);
	}
	public static void Export(Vilag vilag)
	{
		VilagExport.MakeVilagokDir();
	
		var data = vilag.ExportExtra().ToArray();

		var path = $"{basePath}{vilag.nev}.txt";

		using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);
		file.StoreString(string.Join('\n', data));
		file.Close();

		Global.Instance!.vilagok = VilagExport.ImportAll();
	}

	public static Vilag Import(string vilagNevTxt)
	{
		VilagExport.MakeVilagokDir();

		using var file = Godot.FileAccess.Open($"{basePath}{vilagNevTxt}", Godot.FileAccess.ModeFlags.Read);
		string[] data = file.GetAsText().Split('\n');

		return new Vilag(data);
	}

	public static List<Vilag> ImportAll()
	{
		List<Vilag> vilagok = [];
		var dir = DirAccess.Open(basePath);

		if (dir != null)
		{
			dir.ListDirBegin();

			string fileName = dir.GetNext();
			while (fileName != "")
			{
				if (!dir.CurrentIsDir())
				{
					vilagok.Add(VilagExport.Import(fileName));
				}

				fileName = dir.GetNext();
			}

			dir.ListDirEnd();
		}

		vilagok.Sort((a, b) => a.nev.CompareTo(b.nev));
		
		return vilagok;
	}
	
	private static readonly string[] ui_vilag = [
		"Basic Vilag",
		"kartya;Arin;2;5;fold",
		"kartya;Liora;2;4;levego",
		"kartya;Nerun;3;3;tuz",
		"kartya;Selia;2;6;viz",
		"kartya;Torak;3;4;fold",
		"kartya;Emera;2;5;levego",
		"kartya;Vorn;2;7;viz",
		"kartya;Kael;3;5;tuz",
		"kartya;Myra;2;6;fold",
		"kartya;Thalen;3;5;levego",
		"kartya;Isara;2;6;viz",
		"vezer;Lord Torak;Torak;sebzes",
		"vezer;Priestess Selia;Selia;eletero",
		"kazamata;egyszeru;Barlangi Portya;Nerun;sebzes",
		"kazamata;kis;Osi Szentely;Arin,Emera,Selia;Lord Torak;eletero",
		"kazamata;nagy;A melyseg kiralynoje;Liora,Arin,Selia,Nerun,Torak;Priestess Selia",
		"gyujtemeny;Arin;2;5;fold",
		"gyujtemeny;Liora;2;4;levego",
		"gyujtemeny;Selia;2;6;viz",
		"gyujtemeny;Nerun;3;3;tuz",
		"gyujtemeny;Torak;3;4;fold",
		"gyujtemeny;Emera;2;5;levego",
		"gyujtemeny;Kael;3;5;tuz",
		"gyujtemeny;Myra;2;6;fold",
		"gyujtemeny;Thalen;3;5;levego",
	];

	public static void AddBasicVilag()
	{
		var basic_vilag = new Vilag(ui_vilag);
		if (!Godot.FileAccess.FileExists($"{VilagExport.basePath}{basic_vilag.nev}.txt"))
		{
			VilagExport.Export(basic_vilag);
		}
	}

	public static void Delete(string vilagNev)
	{
		vilagNev += ".txt";
		VilagExport.MakeVilagokDir();

		DirAccess dir = DirAccess.Open(basePath);
		if (dir.FileExists(vilagNev))
		{
			Error _ = dir.Remove(vilagNev);
		}

		VilagExport.AddBasicVilag();
		Global.Instance!.vilagok = VilagExport.ImportAll();
	}
}
