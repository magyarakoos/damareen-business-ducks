using Godot;
using System;

public partial class VilagListaElem : HBoxContainer
{
	public Label label;
	public Button modify;
	public Button remove;
	
	public VilagListaElem() : base()
	{
		Name = "ActionBox";
		AnchorRight = 1.0f; 
		Alignment = HBoxContainer.AlignmentMode.Center;
		
		label = new Label();
		label.Text = "Világos";
		
		modify = new Button();
		modify.Text = "Szerkesztés";
		
		remove = new Button();
		remove.Text = "Törlés";

		modify.Pressed += () => label.Text = "szerkesztés";
		remove.Pressed += () => label.Text = "törlés";
		
		AddChild(label);
		AddChild(modify);
		AddChild(remove);
	}
}
