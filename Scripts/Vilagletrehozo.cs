using Godot;

[Export]
public VBoxContainer Vilaglista { get; set; }
// Define a simple structure for your card data (optional, but good practice)
public class CardData : GodotObject
{
	public string CardName { get; set; }
	public int Damage { get; set; }
	public int Health { get; set; }
	public string Type { get; set; }
}


public partial class CardCreatorMenu : Control
{
	// --- 1. Link UI Nodes using [Export] ---
	
	[Export] public LineEdit nev { get; set; }
	[Export] public SpinBox sebzes { get; set; }
	[Export] public SpinBox eletero { get; set; }
	[Export] public OptionButton tipus { get; set; } 
	[Export] public Button Létrehoz { get; set; }

	public override void _Ready()
	{
		// Set up the numerical limits for the SpinBoxes
		sebzes.MinValue = 0;
		elet.MinValue = 1;
		
		// Connect the button's 'Pressed' signal to our submission method
		Létrehoz.Pressed += OnCreateButtonPressed;
	}

	// --- 2. Data Submission Logic ---

	private void OnCreateButtonPressed()
	{
		// 1. Validate Input (Optional, but important!)
		if (string.IsNullOrWhiteSpace(nev.Text))
		{
			GD.PrintErr("Card Name cannot be empty!");
			return;
		}

		// 2. Collect the data
		CardData newCard = new CardData
		{
			CardName = nev.Text,
			// SpinBox returns a double, so cast it to int
			Damage = (int)sebzes.Value,
			Health = (int)elet.Value,
			Type = tipus.Text
		};

		// 3. Process the data (Print for testing, save to a file/database in a real game)
		GD.Print($"--- New Card Created ---");
		GD.Print($"Name: {newCard.CardName}");
		GD.Print($"Damage: {newCard.Damage}");
		GD.Print($"Health: {newCard.Health}");
		GD.Print($"Type: {newCard.Type}");

		// 4. Reset the form
		NameInput.Clear();
		TypeInput.Clear();
		DamageInput.Value = 0;
		HealthInput.Value = 1;
	}
}
