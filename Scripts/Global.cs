using Godot;

public partial class Global : Node
{
	public static Global? Instance { get; private set; }

	public List<Vilag> vilagok = [];
	public Vilag? aktivVilag;
	public Kazamata? aktivKaza;
	public string? aktivNev;

	public override void _Ready()
	{
		Instance = this;
		
		VilagExport.AddBasicVilag();
		this.vilagok = VilagExport.ImportAll();
	}
}
