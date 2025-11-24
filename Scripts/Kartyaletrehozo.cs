using Godot;
using System;

public partial class Kartyaletrehozo : Control
{
	public event EventHandler<Kartya> KartyaLetrehozva = null!; 
	
	[Export] public LineEdit? Nev { get; set; }
	[Export] public SpinBox? Sebzes { get; set; }
	[Export] public SpinBox? Elet { get; set; }
	[Export] public OptionButton? Tipus { get; set; }
	[Export] public Button? Letrehoz { get; set; }

	public override void _Ready()
	{
		if (Nev == null || Sebzes == null || Elet == null || Tipus == null || Letrehoz == null)
		{
			GD.PrintErr("Export mezők nincsenek kitöltve!");
			return;
		}

		Sebzes.MinValue = 1;
		Elet.MinValue = 1;

		// Az enum nevek feltöltése a robusztusabb konverzió érdekében
		foreach (var tipusNev in Enum.GetNames(typeof(KartyaTipus)))
		{
			Tipus.AddItem(tipusNev);
		}

		Letrehoz.Pressed += _on_Letrehoz_pressed;
	}

	private KartyaTipus ConvertOption(string selectedName)
	{
		return Enum.Parse<KartyaTipus>(selectedName);
	}

	private void _on_Letrehoz_pressed()
	{
		if (string.IsNullOrEmpty(Nev!.Text))
		{
			GD.PrintErr("A név nem lehet üres!");
			return;
		}

		string tipusNev = Tipus!.GetItemText(Tipus.Selected);
		KartyaTipus tipusEnum = ConvertOption(tipusNev);

		Kartya newCard = new Kartya(
			Nev.Text,
			(int)Sebzes!.Value,
			(int)Elet!.Value,
			tipusEnum
		);

		KartyaLetrehozva?.Invoke(this, newCard); 

		Nev.Clear();
		Sebzes.Value = 1;
		Elet.Value = 1;
		Tipus.Select(0);
	}
}
