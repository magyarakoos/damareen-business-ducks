using System.Diagnostics;
using Godot;

public partial class CardHolder : Control
{
	public static CardHolder CreateHolder()
	{
		var scene = GD.Load<PackedScene>("res://Scenes/card_holder.tscn");
		var holder = scene.Instantiate<CardHolder>();

		return holder;
	}
}
