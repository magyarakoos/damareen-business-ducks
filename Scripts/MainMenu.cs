using Godot;

public partial class MainMenu : Control
{	
	private void OnJatekmesterButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/jatekmester.tscn");
	}
	
	private void OnJatekosButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/vilag_valaszto.tscn");
	}
	
	private void OnSugoButttonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/sugo.tscn");
	}
	
	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}
