//using Godot;
//
//public partial class CardData : GodotObject
//{
	//public string CardName { get; set; } = "";
	//public int Damage { get; set; }
	//public int Health { get; set; }
	//public string Type { get; set; } = "";
//}
//
//public partial class KartyaLetrehozo : Control
//{
	//[Export] public LineEdit? Nev { get; set; }
	//[Export] public SpinBox? Sebzes { get; set; }
	//[Export] public SpinBox? Elet { get; set; }
	//[Export] public OptionButton? Tipus { get; set; }
	//[Export] public Button? Letrehoz { get; set; }
//
	//public override void _Ready()
	//{
		//Sebzes!.MinValue = 1;
		//Elet!.MinValue = 1;
//
		//Tipus!.AddItem("Tűz");
		//Tipus.AddItem("Víz");
		//Tipus.AddItem("Föld");
		//Tipus.AddItem("Levegő");
//
		//// ✔ CONNECT HERE
		//Letrehoz!.Pressed += _on_Letrehoz_pressed;
	//}
//
	//
	//private void _on_Letrehoz_pressed()
	//{
		//if (string.IsNullOrEmpty(Nev!.Text))
		//{
			//GD.PrintErr("A név nem lehet üres!");
			//return;
		//}
//
		//GD.Print("A név nem üres");
//
		//var newCard = new CardData
		//{
			//CardName = Nev.Text,
			//Damage = (int)Sebzes!.Value,
			//Health = (int)Elet!.Value,
			//Type = Tipus!.GetItemText(Tipus.Selected)
		//};
//
		//GD.Print("--- Új kártya ---");
		//GD.Print($"Név: {newCard.CardName}");
		//GD.Print($"Sebzés: {newCard.Damage}");
		//GD.Print($"Élet: {newCard.Health}");
		//GD.Print($"Típus: {newCard.Type}");
//
		//Nev.Clear();
		//Sebzes.Value = 1;
		//Elet.Value = 1;
		//Tipus.Select(0);
	//}
//}
