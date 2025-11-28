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
		var panel = GetNode<VBoxContainer>("VBoxContainer");
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
			"Általános" => "Üdvözlünk a Damareen világában!\n\nA játékmester módban létrehozhat és szerkeszthet világokat.\nA játékos módban pedig végig tudja játszani ezeket a világokat.",
			"Játékmester" => "A játékmester gombra nyomva feljön a világ kiválasztó menü.\nItt hozhat létre a játékmester új világokat és törölhet ki korábbiakat, továbbá szerkesztheti is őket.\n\nA világ szerkesztési menüben található gombokkal a játékmester képes létrehozni új világkártyákat, vezéreket és kazamatákat. Továbbá megszabhatja hogy mi található a játékos gyűjteményében a játék kezdetekor.\n\nA 'törlés mód' gombra kattintva a kurzoron megjelenik egy szemetes, ilyenkor az első dolog amire rákattint a játékmester ki lesz törölve, ezután kilép a törlés módból.",
			"Játékos" => "A játékos módra kattintva kiválaszthatja melyik lokálisan elmentett világgal szeretne játszani.\nEzután bedob a játékba ahol a játékos összeállíthatja saját pakliját meglévő gyűjteményéből.\nA bal alul található kazamata kártyákra kattintva megkezdheti velük a csatát!\n\nKazamata fokozatok:\nZöld sisak - egyszerű találkozás\nKék kardok - kis kazamata\nVörös zászló - nagy kazamata",
			_ => throw new UnreachableException(),
		};
	}

	private void Onvisszapressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
}
