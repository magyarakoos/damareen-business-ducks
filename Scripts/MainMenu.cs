using Godot;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		// Called every time the node is added to the scene.
		// Initialization here.
		GD.Print("Hello from C# to Godot :)");
	}

	public override void _Process(double delta)
	{
		// Called every frame. Delta is time since the last frame.
		// Update game logic here.
	}
	
	private void OnJatekmesterButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/jatekmester.tscn");
	}
	
	private void OnJatekosButtonPressed() {
		GetTree().ChangeSceneToFile("res://Scenes/jatekos.tscn");
	}
	
	private void OnSugoButttonPressed() {
		GetTree().ChangeSceneToFile("res://Scenes/sugo.tscn");
	}
}
