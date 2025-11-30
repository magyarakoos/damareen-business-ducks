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

	private static readonly string[] altalanosText = [
		"A játékmester módban létrehozhatsz és szerkeszthetsz világokat.",
		"A játékos módban játszhatsz ezekben a világokban.",
		"A kezelőfelületen megtalálható gombok és input mezők akadálymentesítettek, vagyis pusztán billentyűzettel is kezelhetők. [Tab] gombbal, illetve nyilakkal lehet lépkedni, az [Enter] gomb leütése pedig az aktuálisan kijelölt gomb megnyomását szimulálja.",
	];

	private static readonly string[] jatekmesterText = [
		"Itt hozhatsz létre új világokat, törölhetsz és szerkeszthetsz meglévőket, továbbá random világot is generálhasz.",
		"A [Basic Vilag] nevű világot nem lehet törölni, itt a [Törlés] gomb alaphelyzetbe állítja a világot.",
		"A [Törlés mód] gombra kattintva a kurzoron megjelenik egy szemetes. Ilyenkor az első dolog, amire rákattintasz, törlésre kerül. Törlés módból kilépni jobb kattintással, vagy a gomb újbóli megnyomásával lehet.",
	];

	private static readonly string[] jatekosText = [
		"Itt lehet a korábban elkészített világokban játszani. A Játékos módból való kilépésnél a rendszer automatikusan menti a játékos haladását.",
		"Kártyákat a pakliban elhelyezni, illetve onnan a gyűjteménybe visszarakni drag-and-drop módszerrel lehet. (Ha egy kártyát kihúzol a pakliból, magától visszaugrik a gyűjteménybe.)",
		"Adott kazamatában harcolni a kazamata kártyájára való kattintással lehet. Ezek nehézség szerint színkódolva vannak:\n- Zöld:  Egyszerű találkozás\n- Kék:   Kis kazamata\n- Piros: Nagy kazamata",
		"A harc elején ki kell választani azt a kártyát a pakliból, amire [Void] fejlesztést raknál. Ez a kiválasztás ugyanúgy működik, mint a játékmesternél a [Törlés mód].",
	];

	private void ShowContent(Button selected)
	{
		selected.GrabFocus();

		content!.Text = selected.Text switch
		{
			"Általános" => string.Join("\n\n", altalanosText),
			"Játékmester" => string.Join("\n\n", jatekmesterText),
			"Játékos" => string.Join("\n\n", jatekosText),
			_ => throw new UnreachableException(),
		};
	}

	private void Onvisszapressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/main_menu.tscn");
	}
}
