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

	}

	public void ConnectCards(Node parent)
	{
		foreach (Node child in parent.GetChildren())
		{
			if (child is Card card)
			{
				GD.Print("Adding handlers: ", card.GetNode<Label>("CardName").Text);
				card.CardPressed += OnCardPressed;
				card.CardReleased += OnCardReleased;
			}
		}
	}

	private void OnCardPressed(Card card)
	{
		if (cardDragged != null) return;

		Node par = card.GetParent();
		if (par.Name == "Gyujtemeny")
		{
			fromPakli = false;
			par.RemoveChild(card);
		}
		else if (par.Name == "Pakli")
		{
			fromPakli = true;
			int i = card.GetIndex();
			par.RemoveChild(card);
			var holder = CardHolder.CreateHolder();
			par.AddChild(holder);
			par.MoveChild(holder, i);
		}
		else
		{
			throw new UnreachableException();
		}

		GetTree().CurrentScene.AddChild(card);

		cardDragged = card;

		GD.Print("Pressed card: " + card.GetNode<Label>("CardName").Text);
	}

	private void OnCardReleased(Card card)
	{
		if (cardDragged == null) return;

		Kartya kartya = Global.Instance!.aktivVilag!.jatekos.gyujtemeny.Find(kartya => kartya.nev == cardDragged.GetNode<Label>("CardName").Text)!;

		GetTree().CurrentScene.RemoveChild(cardDragged);

		CardHolder? holder = GetCardHolderUnderMouse();
		if (holder == null)
		{
			GetNode<HFlowContainer>("JatekosInfo/Gyujtemeny").AddChild(cardDragged);

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

		cardDragged = null;
		fromPakli = false;

		EmitSignal(SignalName.CardsRerender);

		GD.Print("Released card: " + card.GetNode<Label>("CardName").Text);
	}

	public CardHolder? GetCardHolderUnderMouse()
	{
		var spaceState = GetWorld2D().DirectSpaceState;
		var mousePos = GetGlobalMousePosition();

		var query = new PhysicsPointQueryParameters2D
		{
			Position = mousePos,
			CollisionMask = 2, // Use whatever collision layer your card holders are on
			CollideWithAreas = true,
			CollideWithBodies = false
		};

		var result = spaceState.IntersectPoint(query);

		if (result.Count > 0)
		{
			// Get the first (topmost) result
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
