namespace ui {
	public class Output {
		public void PrintBoard(model.Board board) {
			Console.Write("Your tiles (map A)\n");
			PrintMap(board.tileMap);
			Console.Write("\n");

			Console.Write("Target map (map B)\n");
			PrintMap(board.puzzleMap);
			Console.Write("\n");
		}

		public void PrintMap(model.Tile?[,] map) {
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

		public void PrintHelp() {
			Console.Write("\n");

			Console.Write("C# Terminal Tetravex\n");
			Console.Write("Help Page\n\n");
			Console.Write("COMMAND           |DESCRIPTION            |SYNTAX                                                                       \n");
			Console.Write("help              |Display this help page |help                                                                         \n");
			Console.Write("board (print)     |Print the board maps   |board                                                                        \n");
			Console.Write("move              |Move a tile            |move <sourceBoardID> <sourceX> <sourceY> <targetBoardID> <targetX> <targetY> \n");
			Console.Write("clear (cls)       |Clear screen           |clear                                                                        \n");
			Console.Write("solve (solution)  |Display a solution     |solve                                                                        \n");
			Console.Write("quit (q, Q, exit) |Quit Tetravex          |quit                                                                         \n");
		}
	}
}