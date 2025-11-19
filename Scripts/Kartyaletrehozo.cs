using Godot;


public partial class Kartyaletrehozo : Control
{
	[Export] public LineEdit? Nev { get; set; }
	[Export] public SpinBox? Sebzes { get; set; }
	[Export] public SpinBox? Elet { get; set; }
	[Export] public OptionButton? Tipus { get; set; }
	[Export] public Button? Letrehoz { get; set; }

	public override void _Ready()
	{
		if (Nev == null || Sebzes == null || Elet == null || Tipus == null || Letrehoz == null)
		{
			GD.PrintErr("❌ Export mezők nincsenek kitöltve!");
			return;
		}

		Sebzes.MinValue = 1;
		Elet.MinValue = 1;

		Tipus.AddItem("Tűz");
		Tipus.AddItem("Víz");
		Tipus.AddItem("Föld");
		Tipus.AddItem("Levegő");

		Letrehoz.Pressed += _on_Letrehoz_pressed;
	}

	private KartyaTipus ConvertOption(int index)
	{
		return index switch
		{
			0 => KartyaTipus.Tuz,
			1 => KartyaTipus.Viz,
			2 => KartyaTipus.Fold,
			3 => KartyaTipus.Levego,
			_ => KartyaTipus.Fold,
		};
	}

	private void _on_Letrehoz_pressed()
	{
		if (string.IsNullOrEmpty(Nev!.Text))
		{
			GD.PrintErr("A név nem lehet üres!");
			return;
		}

		KartyaTipus tipusEnum = ConvertOption(Tipus!.Selected);

		Kartya newCard = new Kartya(
			Nev.Text,
			(int)Sebzes!.Value,
			(int)Elet!.Value,
			tipusEnum
		);

		GD.Print("--- Új kártya ---");
		GD.Print(newCard.ToString());

		Nev.Clear();
		Sebzes.Value = 1;
		Elet.Value = 1;
		Tipus.Select(0);
	}
}
