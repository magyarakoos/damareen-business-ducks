using System.Diagnostics;
using System.Security.AccessControl;
using Godot;

public partial class CardManager : Control
{
	[Signal] public delegate void CardsRerenderEventHandler();
	private Card? cardDragged;
	private bool fromPakli;

	public override void _Ready()
	{
		MouseFilter = MouseFilterEnum.Stop;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouse && mouse.ButtonIndex == MouseButton.Left)
		{
			if (mouse.Pressed)
			{
				HandleCardPress();
			}
			else
			{
				HandleCardRelease();
			}
		}
	}

	public Card? GetTopCardUnderMouse()
	{
		var mousePos = GetGlobalMousePosition();
		var spaceState = GetWorld2D().DirectSpaceState;

		var query = new PhysicsPointQueryParameters2D
		{
			Position = mousePos,
			CollisionMask = 1,
			CollideWithAreas = true,
			CollideWithBodies = false
		};

		var results = spaceState.IntersectPoint(query);

		Card? topCard = null;
		int highestZIndex = -9999;

		foreach (var result in results)
		{
			var area = result["collider"].As<Area2D>();
			if (area != null)
			{
				var par = area.GetParent();
				if (par is Card card)
				{

					if (card != null && card.Visible)
					{
						// Check if this card has a higher Z-index than current top
						if (card.ZIndex > highestZIndex)
						{
							highestZIndex = card.ZIndex;
							topCard = card;
						}
						// If same Z-index, use the one that appears later in scene tree (drawn on top)
						else if (card.ZIndex == highestZIndex && topCard != null)
						{
							if (card.GetIndex() > topCard.GetIndex())
							{
								topCard = card;
							}
						}
					}
				}
			}
		}

		return topCard;
	}

	private void HandleCardPress()
	{
		if (cardDragged != null) return;

		Card? card = GetTopCardUnderMouse();
		if (card == null) return;

		Node par = card.GetParent();
		if (par.Name == "Pakli")
		{
			fromPakli = true;
			int i = card.GetIndex();
			par.RemoveChild(card);
			var holder = CardHolder.CreateHolder();
			par.AddChild(holder);
			par.MoveChild(holder, i);
		}
		else if (par.Name == "Gyujtemeny")
		{
			fromPakli = false;
			par.RemoveChild(card);
		}
		else { return; }

		GetTree().CurrentScene.AddChild(card);

		card.ZIndex = 2;
		card.Scale = new Vector2(1.2f, 1.2f);
		cardDragged = card;
	}

	private void HandleCardRelease()
	{
		if (cardDragged == null) return;

		Kartya kartya = Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Find(kartya => kartya.nev == cardDragged.GetNode<Label>("CardName").Text)!;

		GetTree().CurrentScene.RemoveChild(cardDragged);

		CardHolder? holder = GetCardHolderUnderMouse();
		if (holder == null)
		{
			GetNode<HFlowContainer>("JatekosInfo/GyujtemenyPanel/MarginContainer/VBoxContainer/Gyujtemeny").AddChild(cardDragged);

			Global.Instance!.aktivVilag!.jatekos.pakli!.Remove(kartya);
		}
		else
		{
			int i = holder.GetIndex();
			Node par = holder.GetParent();
			par.RemoveChild(holder);
			par.AddChild(cardDragged);
			par.MoveChild(cardDragged, i);

			if (!fromPakli)
			{
				Global.Instance!.aktivVilag!.jatekos.pakli ??= [];
				Global.Instance!.aktivVilag!.jatekos.pakli.Add(kartya);
			}
		}

		cardDragged.ZIndex = 1;
		cardDragged.Scale = new Vector2(1, 1);

		cardDragged = null;
		fromPakli = false;

		EmitSignal(SignalName.CardsRerender);
	}

	public CardHolder? GetCardHolderUnderMouse()
	{
		var spaceState = GetWorld2D().DirectSpaceState;
		var mousePos = GetGlobalMousePosition();

		var query = new PhysicsPointQueryParameters2D
		{
			Position = mousePos,
			CollisionMask = 2,
			CollideWithAreas = true,
			CollideWithBodies = false
		};

		var result = spaceState.IntersectPoint(query);

		if (result.Count > 0)
		{
			var collider = result[0]["collider"].As<Area2D>();
			if (collider != null)
			{
				return collider.GetParent<CardHolder>();
			}
		}

		return null;
	}

	public override void _Process(double delta)
	{
		if (cardDragged != null)
		{
			cardDragged.Position = GetGlobalMousePosition() - cardDragged.Size / 2;
		}
	}
}
