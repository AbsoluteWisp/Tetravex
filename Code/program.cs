public static class Program {
	public static void Main(string[] args) {
		const int boardX = 7;
		const int boardY = 7;

		// Instantiate the UI class
		UI ui = new UI();

		// Generate a solved board
		model.MapGenerator tilegen = new model.MapGenerator();
		model.Tile[,] boardMap = tilegen.GenerateMap(boardX, boardY);

		// Prettyprint the board
		ui.PrettyPrintBoard(boardMap, boardX, boardY);
	}
}

public class UI {
	public void PrettyPrintBoard(model.Tile[,] boardMap, int boardX, int boardY) {
		// Repeat the top separation line for boardX times and add the missing end
		for (int i = 0; i < boardX; i++) {
			Console.Write("+-----");
		}
		Console.Write("+\n");
		
		for (int y = 0; y < boardY; y++) {
			// Start of row
			Console.Write("|");
			// First line: top value of each tile in the row
			for (int x = 0; x < boardX; x++) {
				Console.Write("\\ " + boardMap[x, y].U + " /|");
			}
			Console.Write("\n");
			
			// Start of row
			Console.Write("|");
			// Second line: left and right values of each tile in the row
			for (int x = 0; x < boardX; x++) {
				Console.Write(boardMap[x, y].L + " X " + boardMap[x, y].R + "|");
			}
			Console.Write("\n");

			// Start of row
			Console.Write("|");
			// Third line: down value of each tile in the row
			for (int x = 0; x < boardX; x++) {
				Console.Write("/ " + boardMap[x, y].D + " \\|");
			}
			Console.Write("\n");
			
			// Repeat the bottom separation line for boardX times and add the missing end
			for (int i = 0; i < boardX; i++) {
				Console.Write("+-----");
			}
			Console.Write("+\n");
		}
	}
}