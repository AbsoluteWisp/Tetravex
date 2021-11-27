namespace program {
	public static class Program {
		public static void Main(string[] args) {
			// Configuration. TODO: Take from console input or args
			const int boardX = 3;
			const int boardY = 3;

			// Instantiate a UI output class
			ui.Output output = new ui.Output();
			Input input = new Input();

			// Create board
			model.Board board = new model.Board(boardX, boardY);

			// Game loop
			bool shouldClose = false;
			Console.Clear();
			output.PrintBoard(board);
			
			while (!shouldClose) {				
				// Get meaningful input
				string? userInput;
				do {
					Console.Write("Please input a command > ");
					userInput = Console.ReadLine();
				} while (String.IsNullOrWhiteSpace(userInput));

				Input.inputIntent intent = input.GetUserInputIntent(userInput);
				if (intent == Input.inputIntent.move) {
					input.HandleMove(userInput, board);
					Console.Clear();
					output.PrintBoard(board);
				}
				if (intent == Input.inputIntent.exit) {
					shouldClose = true;
				}
				if (intent == Input.inputIntent.clear) {
					Console.Clear();
					output.PrintBoard(board);
				}
				if (intent == Input.inputIntent.board) {
					output.PrintBoard(board);
				}
				if (intent == Input.inputIntent.solution) {
					output.PrintMap(board.solvedMap);
				}
				if (intent == Input.inputIntent.help) {
					Console.Clear();
					output.PrintHelp();
				}
				if (intent == Input.inputIntent.none) {
					Console.Write("Couldn't understand input.\n");
				}

				// Check for win
				if (board.WinCheck()) {
					Console.Write("Congratulations! You won! :D\n");
					shouldClose = true;
				}
			}

			Console.Write("Thank you for playing C# Terminal Tetravex!\n");
			Environment.Exit(0);
		}
	}

	public class Input {
		public inputIntent GetUserInputIntent(string userInput) {
			if (System.Text.RegularExpressions.Regex.Match(userInput, "^move [aAbB] [0-9]* [0-9]* [aAbB] [0-9]* [0-9]*").Success) {
				return inputIntent.move;
			}
			if (userInput == "q" || userInput == "exit" || userInput == "quit") {
				return inputIntent.exit;
			}
			if (userInput == "cls" || userInput == "clear") {
				return inputIntent.clear;
			}
			if (userInput == "board" || userInput == "print") {
				return inputIntent.board;
			}
			if (userInput == "solve" || userInput == "solution") {
				return inputIntent.solution;
			}
			if (userInput == "help") {
				return inputIntent.help;
			}

			return inputIntent.none;
		}

		public void HandleMove(string input, model.Board board) {
			model.Tile?[,] sourceMap;
			int sourceX;
			int sourceY;

			model.Tile?[,] targetMap;
			int targetX;
			int targetY;

			string[] substrings = input.Split(" ");
			
			if (substrings[1] == "a" || substrings[1] == "A") {
				sourceMap = board.tileMap;
			}
			else {
				sourceMap = board.puzzleMap;
			}

			int.TryParse(substrings[2], out sourceX);
			int.TryParse(substrings[3], out sourceY);

			if (substrings[4] == "b" || substrings[1] == "B") {
				targetMap = board.puzzleMap;
			}
			else {
				targetMap = board.tileMap;
			}

			int.TryParse(substrings[5], out targetX);
			int.TryParse(substrings[6], out targetY);

			try {
				board.MoveTile(sourceMap, sourceX, sourceY, targetMap, targetX, targetY);
			}
			catch (Exception e) {
				Console.Write(e.Message + "\n");
			}
		}

		public enum inputIntent {
			move,
			exit,
			clear,
			board,
			solution,
			help,
			none,
		}
	}
}
