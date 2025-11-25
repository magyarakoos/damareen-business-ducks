using Godot;

// The one and only partial class, attached to the main Control node.
public partial class JatekmesterMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RerenderVilaglista();
	}

	private void RerenderVilaglista()
	{
		var vilaglista = GetNode<VBoxContainer>("VBoxContainer/CenterContainer/VBoxContainer2/ScrollContainer/Vilaglista");
		
		foreach (Node child in vilaglista.GetChildren())
		{
			vilaglista.RemoveChild(child);
		}

		foreach (Vilag vilag in Global.Instance!.vilagok)
		{
			var listaElem = VilaglistaElem.CreateElem(vilag);
			listaElem.RerenderVilaglista += RerenderVilaglista;
			vilaglista.AddChild(listaElem);
		}
	}

	private void UjVilagPressed()
	{
		Global.Instance!.aktivVilag = new Vilag();
		GetTree().ChangeSceneToFile("res://Scenes/vilag_szerkeszto.tscn");
	}

	private void OnVisszaButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
}
