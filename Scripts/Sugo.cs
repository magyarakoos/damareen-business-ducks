using Godot;
using System.Diagnostics;

public partial class Sugo : Control
{
	private Button? altalanos;
	private Button? jatekmester;
	private Button? jatekos;
	private RichTextLabel? content;

	public override void _Ready()
	{
		var panel = GetNode<VBoxContainer>("Panel/VBoxContainer");
		altalanos = panel.GetNode<Button>("OptionsRow/Altalanos");
		jatekmester = panel.GetNode<Button>("OptionsRow/Jatekmester");
		jatekos = panel.GetNode<Button>("OptionsRow/Jatekos");
		content = panel.GetNode<RichTextLabel>("ContentPanel/ContentLabel");

		SetupOption(altalanos);
		SetupOption(jatekmester);
		SetupOption(jatekos);

		ShowContent(altalanos);
	}

	private void SetupOption(Button button)
	{
		button.ActionMode = BaseButton.ActionModeEnum.Press;
		button.Pressed += () => {
			ShowContent(button);
		};
	}

	private void ShowContent(Button selected)
	{
		selected.GrabFocus();

		content!.Text = selected.Text switch
		{
			"Általános" => "Ez egy fantasy kártyajáték melyben a pakliddal meghódíthatod a világ kazamatáit.",
			"Játékmester" => "A világokat szerkeszteni vagy törölni lehet.Szerkesztés opciót választva minden aspektusát a világnak megváltoztathatod.A törlés gombra kattintva a következő kattintással kitörölhetsz egy kártyát ami minden vele kapcsolatos játékelemet kitöröl.",
			"Játékos" => "Lorem játékos ipsum dolor sit amet, ut enim ad minim veniam.",
			_ => throw new UnreachableException(),
		};
	}

	private void Onvisszapressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
}
