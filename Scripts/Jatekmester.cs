using Godot;

// The one and only partial class, attached to the main Control node.
public partial class Jatekmester : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Now Jatekmester calls its own creation method
		CreateUIComponent();
		CreateUIComponent();
		CreateUIComponent();
	}

	private void CreateUIComponent()
	{
		var hbox = new VilagListaElem();
		var vilaglista = GetNode<VBoxContainer>("Vilaglista");
		vilaglista.AddChild(hbox);
		GD.Print("Sikeresen létrejött egy világ");
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Your main game logic for Jatekmester goes here
	}
}
