namespace ui {
	public class Output {
		public void PrintBoard(model.Board board) {
			Console.Clear();
			Console.Write("Target map (map A)\n");
			PrintMap(board.puzzleMap);
			Console.Write("\n");
			Console.Write("Your tiles (map B)\n");
			PrintMap(board.tileMap);
		}

		void PrintMap(model.Tile?[,] map) {
			int boardX = map.GetLength(0);
			int boardY = map.GetLength(1);

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
					model.Tile? curTileNull = map[x, y];
					if (curTileNull != null) {
						model.Tile curTileNonNull = (model.Tile)curTileNull;
						Console.Write("\\ " + curTileNonNull.U + " /|");					
					}
					else {
						Console.Write("     |");
					}
				}
				Console.Write("\n");
				
				// Start of row
				Console.Write("|");
				// Second line: left and right values of each tile in the row
				for (int x = 0; x < boardX; x++) {
					model.Tile? curTileNull = map[x, y];
					if (curTileNull != null) {
						model.Tile curTileNonNull = (model.Tile)curTileNull;
						Console.Write(curTileNonNull.L + " X " + curTileNonNull.R + "|");					
					}
					else {
						Console.Write("     |");
					}
				}
				Console.Write("\n");

				// Start of row
				Console.Write("|");
				// Third line: down value of each tile in the row
				for (int x = 0; x < boardX; x++) {
					model.Tile? curTileNull = map[x, y];
					if (curTileNull != null) {
						model.Tile curTileNonNull = (model.Tile)curTileNull;
						Console.Write("/ " + curTileNonNull.D + " \\|");				
					}
					else {
						Console.Write("     |");
					}
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
}