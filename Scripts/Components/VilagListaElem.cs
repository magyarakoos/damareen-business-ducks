using Godot;
using System;

public partial class VilagListaElem : HBoxContainer
{
	public Label label;
	public Button modify;
	public Button remove;
	
	public VilagListaElem(string name, VBoxContainer container) : base()
	{
		Name = "ActionBox";
		AnchorRight = 1.0f; 
		Alignment = HBoxContainer.AlignmentMode.Center;
		
		label = new Label();
		label.Text = name;
		
		modify = new Button();
		modify.Text = "Szerkesztés";
		
		remove = new Button();
		remove.Text = "Törlés";

		modify.Pressed += () =>
		{
			throw new NotImplementedException("TODO: Modify action");
		};
		remove.Pressed += () =>
		{
			VilagExport.Delete(name);
			container.RemoveChild(this);
			// throw new NotImplementedException("TODO: Refresh the list, since an element was deleted");
		};
		
		AddChild(label);
		AddChild(modify);
		AddChild(remove);
	}
}
