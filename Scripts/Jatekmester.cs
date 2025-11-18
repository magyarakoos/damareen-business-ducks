using Godot;

// The one and only partial class, attached to the main Control node.
public partial class Jatekmester : Control
{
	// Fields for the UI elements, now belonging to Jatekmester
	private HBoxContainer _hbox;
	private Label _statusLabel;
	private Button _button1;
	private Button _button2;

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
		// 1. Create the HBoxContainer (The parent for the UI elements)
		_hbox = new HBoxContainer();
		_hbox.Name = "ActionBox";
		_hbox.AnchorRight = 1.0f; 
		_hbox.Alignment = HBoxContainer.AlignmentMode.Center;
		
		// Add the container as a child of Jatekmester
		AddChild(_hbox);
		
		// 2. Create and configure the Label
		_statusLabel = new Label();
		_statusLabel.Text = "Első világ";
		
		// 3. Create and configure the Buttons
		_button1 = new Button();
		_button1.Text = "Szerkeszt";
		
		_button2 = new Button();
		_button2.Text = "Töröl";

		// 4. Connect button signals
		_button1.Pressed += OnButton1Pressed;
		_button2.Pressed += OnButton2Pressed;
		
		// 5. Add children to the HBoxContainer
		_hbox.AddChild(_statusLabel);
		_hbox.AddChild(_button1);
		_hbox.AddChild(_button2);
		
		GD.Print("Sikeresen létrejött egy világ");
	}

	// --- Signal Handling Methods ---

	private void OnButton1Pressed()
	{
		_statusLabel.Text = "szerkesztés";
		GD.Print("Szerkeszt gombot megnyomták.");
	}

	private void OnButton2Pressed()
	{
		_statusLabel.Text = "törlés";
		GD.Print("törlés gombot megnyomták");
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Your main game logic for Jatekmester goes here
	}
}
