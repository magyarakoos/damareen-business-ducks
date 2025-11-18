//using Godot;
//
//// Define a simple structure for your card data (optional, but good practice)
//public class CardData : GodotObject
//{
	//public string CardName { get; set; }
	//public int Damage { get; set; }
	//public int Health { get; set; }
	//public string Type { get; set; }
//}
//
//
//public partial class CardCreatorMenu : Control
//{
	//// --- 1. Link UI Nodes using [Export] ---
	//
	//// Note: Vilaglista must be defined inside the class it's attached to, 
	//// or as a member of another class that instances it.
	//// Assuming Vilaglista is for a different purpose and is not part of this class's exports.
	//
	//[Export] public LineEdit nev { get; set; }
	//[Export] public SpinBox sebzes { get; set; }
	//[Export] public SpinBox eletero { get; set; } // Renamed from 'elet' to match property
	//[Export] public OptionButton tipus { get; set; } // OptionButton is for selecting from a list
	//[Export] public Button Létrehoz { get; set; }
//
	//public override void _Ready()
	//{
		//// Set up the numerical limits for the SpinBoxes
		//sebzes.MinValue = 0;
		//
		//// FIX: Changed 'elet' to 'eletero'
		//eletero.MinValue = 1; 
		//
		//// Connect the button's 'Pressed' signal to our submission method
		//Létrehoz.Pressed += OnCreateButtonPressed;
		//
		//// Optional: Ensure the OptionButton has items if it was created dynamically
		//if (tipus.ItemCount == 0)
		//{
			//tipus.AddItem("Attack");
			//tipus.AddItem("Defense");
			//tipus.AddItem("Utility");
		//}
	//}
//
	//// --- 2. Data Submission Logic ---
//
	//private void OnCreateButtonPressed()
	//{
		//// 1. Validate Input
		//if (string.IsNullOrWhiteSpace(nev.Text))
		//{
			//GD.PrintErr("Card Name cannot be empty!");
			//return;
		//}
//
		//// Get the selected text from the OptionButton
		//string selectedType = tipus.GetItemText(tipus.Selected);
//
		//// 2. Collect the data
		//CardData newCard = new CardData
		//{
			//CardName = nev.Text,
			//
			//// FIX: Changed 'DamageInput' to 'sebzes'
			//Damage = (int)sebzes.Value, 
			//
			//// FIX: Changed 'HealthInput' to 'eletero'
			//Health = (int)eletero.Value, 
			//
			//// FIX: Used the correct way to get OptionButton text
			//Type = selectedType 
		//};
//
		//// 3. Process the data (Print for testing)
		//GD.Print($"--- New Card Created ---");
		//GD.Print($"Name: {newCard.CardName}");
		//GD.Print($"Damage: {newCard.Damage}");
		//GD.Print($"Health: {newCard.Health}");
		//GD.Print($"Type: {newCard.Type}");
//
		//// 4. Reset the form
		//// FIX: Changed 'NameInput' to 'nev'
		//nev.Clear();
		//
		//// FIX: SpinBoxes must be reset by setting their Value
		//sebzes.Value = 0;
		//eletero.Value = 1;
//
		//// OptionButton is usually reset by selecting the first item (index 0)
		//tipus.Select(0);
		//
		//GD.Print("Form reset.");
	//}
	//
	//// You should ensure the Vilaglista logic (adding a new HBox item) is also integrated here 
	//// if you want to display the new card's data in the VBoxContainer immediately.
//}
