using Godot;

// The one and only partial class, attached to the main Control node.
public partial class JatekmesterMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (Vilag vilag in Global.Instance!.vilagok)
		{
			CreateUIComponent(vilag.nev);
		}
	}
		private void ujvilagletrehozasapressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/vilagletrehozo.tscn");
	}


	private void CreateUIComponent(string name)
	{
		var vilaglista = GetNode<VBoxContainer>("Vilaglista");
		var hbox = new VilagListaElem(name, vilaglista);
		vilaglista.AddChild(hbox);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Your main game logic for Jatekmester goes here
	}
}
