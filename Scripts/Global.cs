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
		"gyujtemeny;Arin",
		"gyujtemeny;Liora",
		"gyujtemeny;Selia",
		"gyujtemeny;Nerun",
		"gyujtemeny;Torak",
		"gyujtemeny;Emera",
		"gyujtemeny;Kael",
		"gyujtemeny;Myra",
		"gyujtemeny;Thalen",
		"gyujtemeny;Isara",
	];

	public static Global? Instance { get; private set; }

	public List<Vilag> vilagok = [];
	public Vilag? aktivVilag;
	public Kazamata? aktivKaza;

	public override void _Ready()
	{
		Instance = this;
		VilagExport.MakeVilagokDir();
		VilagExport.Export(new Vilag(ui_vilag));
		this.vilagok = VilagExport.ImportAll();
		foreach (Vilag vilag in vilagok)
		{
			GD.Print(vilag);
		}
	}
}
