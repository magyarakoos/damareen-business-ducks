using System.Diagnostics;
using Godot;

public class CardHolder
{	
	public static Control CreateHolder()
	{
		var scene = GD.Load<PackedScene>("res://Scenes/card_holder.tscn");
		var card = scene.Instantiate<Control>();

		return card;
	}
}
