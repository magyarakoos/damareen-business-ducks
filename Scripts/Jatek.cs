using Godot;

public partial class Jatek : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Panel>("MainPanel").Hide();
		GetNode<Panel>("VilagKivalasztas").Show();

		var vilaglista = GetNode<OptionButton>("VilagKivalasztas/VBoxContainer/Vilagok");
		foreach (Vilag vilag in Global.Instance!.vilagok)
		{
			vilaglista.AddItem(vilag.nev);
		}
	}

	private void OnVilagKivalasztPressed()
	{
		var vilaglista = GetNode<OptionButton>("VilagKivalasztas/VBoxContainer/Vilagok");
		string selected = vilaglista.GetItemText(vilaglista.Selected);
		GD.Print("Selected option: " + selected);

		Global.Instance!.aktivVilag = Global.Instance!.vilagok.Find((vilag) => vilag.nev == selected)!;

		GetNode<Label>("MainPanel/Label").Text = Global.Instance!.aktivVilag.nev;

		GetNode<Panel>("VilagKivalasztas").Hide();
		GetNode<Panel>("MainPanel").Show();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
