namespace model {
	public class Board {
		ui.Output output = new ui.Output();

		int boardX, boardY; 
		public Tile?[,] puzzleMap {get; private set;}
		public Tile?[,] tileMap {get; private set;}

		public Board(int cBoardX, int cBoardY) {
			boardX = cBoardX;
			boardY = cBoardY;

			// The puzzle starts empty
			puzzleMap = new Tile?[boardX, boardY];
			// The tiles get generated and shuffled
			tileMap = ShuffleMap(GenerateMap());
		}

		Tile?[,] GenerateMap() {
			var map = new Tile?[boardX, boardY];

			// Loop through all board positions, generating a valid tile at each one
			for (int x = 0; x < boardX; x++) {
				for (int y = 0; y < boardY; y++) {
					// These decide the value of a given edge of the tile on (x,y). Null means random, and is the default.
					int? L = null, U = null, R = null, D = null;

					// Decide if value of the L edge needs to be taken from a neighbouring tile
					if (x != 0) {
						Tile? neighbour = map[x-1, y];
						if (neighbour != null) {
							L = neighbour.Value.R;							
						}
					}

					// Decide if value of the U edge needs to be taken from a neighbouring tile
					if (y != 0) {
						Tile? neighbour = map[x, y-1];
						if (neighbour != null) {
							U = neighbour.Value.D;							
						}
					}

					// Decide if value of the R edge needs to be taken from a neighbouring tile
					if (x != (boardX - 1)) {
						Tile? neighbour = map[x+1, y];
						if (neighbour != null) {
							R = neighbour.Value.L;							
						}
					}

					// Decide if value of the D edge needs to be taken from a neighbouring tile
					if (y != (boardY - 1)) {
						Tile? neighbour = map[x, y+1];
						if (neighbour != null) {
							D = neighbour.Value.U;							
						}
					}

					var generatedTile = new Tile(L, U, R, D);
					map[x, y] = generatedTile;
				}
			}

			return map;
		}

		Tile?[,] ShuffleMap(Tile?[,] mapToShuffle) {
			Tile?[,] shuffledMap = new Tile?[boardX, boardY];
			Random random = new Random();

			for (int x = 0; x < boardX; x++) {
				for (int y = 0; y < boardY; y++) {
					bool goAgain = true;

					while (goAgain) {
						int randomX = random.Next(0, boardX);
						int randomY = random.Next(0, boardY);

						if (mapToShuffle[randomX, randomY] != null) {
							shuffledMap[x, y] = mapToShuffle[randomX, randomY];
							goAgain = false;						
						}						
					}
				}
			}

			return shuffledMap;
		}
	}
	
	public struct Tile {
		public int L;
		public int U;
		public int R;
		public int D;

		public Tile(int? cL, int? cU, int? cR, int? cD) {
			var random = new Random();
			
			if (cL != null) {
				L = (int)cL;
			}
			else {
				L = random.Next(1,10);
			}
			
			if (cU != null) {
				U = (int)cU;
			}
			else {
				U = random.Next(1,10);
			}

			if (cR != null) {
				R = (int)cR;
			}
			else {
				R = random.Next(1,10);
			}

			if (cD != null) {
				D = (int)cD;
			}
			else {
				D = random.Next(1,10);
			}
		}
	}
}