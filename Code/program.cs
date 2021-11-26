namespace program {
	public static class Program {
		public static void Main(string[] args) {
			// Configuration. TODO: Take from console input or args
			const int boardX = 9;
			const int boardY = 3;

			// Instantiate a UI output class
			ui.Output output = new ui.Output();

			// Create board
			model.Board board = new model.Board(boardX, boardY);

			// Print the board
			output.PrintBoard(board);
		}
	}
}
