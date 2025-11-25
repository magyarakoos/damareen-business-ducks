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

	public static void Delete(string vilagNev)
	{
		vilagNev += ".txt";
		VilagExport.MakeVilagokDir();

		DirAccess dir = DirAccess.Open(basePath);
		if (dir.FileExists(vilagNev))
		{
			Error _ = dir.Remove(vilagNev);
		}

		Global.Instance!.vilagok = VilagExport.ImportAll();
	}
}
