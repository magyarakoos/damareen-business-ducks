using Godot;

public partial class Global : Node
{
	private static string[] ui_vilag = [
		"Basic Vilag",
		"kartya;Arin;2;5;fold",
		"kartya;Liora;2;4;levego",
		"kartya;Nerun;3;3;tuz",
		"kartya;Selia;2;6;viz",
		"kartya;Torak;3;4;fold",
		"kartya;Emera;2;5;levego",
		"kartya;Vorn;2;7;viz",
		"kartya;Kael;3;5;tuz",
		"kartya;Myra;2;6;fold",
		"kartya;Thalen;3;5;levego",
		"kartya;Isara;2;6;viz",
		"vezer;Lord Torak;Torak;sebzes",
		"vezer;Priestess Selia;Selia;eletero",
		"kazamata;egyszeru;Barlangi Portya;Nerun;sebzes",
		"kazamata;kis;Osi Szentely;Arin,Emera,Selia;Lord Torak;eletero",
		"kazamata;nagy;A melyseg kiralynoje;Liora,Arin,Selia,Nerun,Torak;Priestess Selia",
		"gyujtemeny;Arin;2;5;fold",
		"gyujtemeny;Liora;2;4;levego",
		"gyujtemeny;Selia;2;6;viz",
		"gyujtemeny;Nerun;3;3;tuz",
		"gyujtemeny;Torak;3;4;fold",
		"gyujtemeny;Emera;2;5;levego",
		"gyujtemeny;Kael;3;5;tuz",
		"gyujtemeny;Myra;2;6;fold",
		"gyujtemeny;Thalen;3;5;levego",
		"gyujtemeny;Isara;2;6;void",
	];

	public static Global? Instance { get; private set; }

	public List<Vilag> vilagok = [];
	public Vilag? aktivVilag;
	public Kazamata? aktivKaza;

	public override void _Ready()
	{
		Instance = this;
		VilagExport.MakeVilagokDir();

		var basic_vilag = new Vilag(ui_vilag);
		if (!Godot.FileAccess.FileExists($"{VilagExport.basePath}{basic_vilag.nev}.txt"))
		{
			VilagExport.Export(basic_vilag);
		}
		
		this.vilagok = VilagExport.ImportAll();
	}
}
