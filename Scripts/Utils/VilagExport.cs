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
		var data = vilag.ExportExtra().ToArray();

		var path = $"{basePath}{vilag.nev}.txt";

		using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);
		file.StoreString(string.Join('\n', data));
		return;
	}

	public static Vilag Import(string vilagNevTxt)
	{
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
					GD.Print(fileName);
					vilagok.Add(VilagExport.Import(fileName));
				}

				fileName = dir.GetNext();
			}

			dir.ListDirEnd();
		}

		return vilagok;
	}

	public static void Delete(string vilagNev)
	{
		vilagNev += ".txt";
		GD.Print(vilagNev);
		VilagExport.MakeVilagokDir();

		DirAccess dir = DirAccess.Open(basePath);
		if (dir.FileExists(vilagNev))
		{
			Error _ = dir.Remove(vilagNev);
		}
	}
}
