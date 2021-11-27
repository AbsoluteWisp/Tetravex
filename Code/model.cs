namespace model {
	public class Board {
		ui.Output output = new ui.Output();

		int boardX, boardY; 
		public Tile?[,] puzzleMap {get; private set;}
		public Tile?[,] tileMap {get; private set;}
		public Tile?[,] solvedMap {get; private set;}

		public Board(int cBoardX, int cBoardY) {
			boardX = cBoardX;
			boardY = cBoardY;

			// The puzzle starts empty
			puzzleMap = new Tile?[boardX, boardY]; 
			// The tiles get generated and shuffled
			(Tile?[,], List<Tile?>) generated = GenerateMap();
			solvedMap = generated.Item1;
			List<Tile?> solvedList = generated.Item2;
			tileMap = ShuffleTilesToMap(solvedList);
		}

		(Tile?[,], List<Tile?>) GenerateMap() {
			var map = new Tile?[boardX, boardY];
			var tileList = new List<Tile?>();

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
					tileList.Add(generatedTile);
					map[x, y] = generatedTile;
				}
			}

			return (map, tileList);
		}

		Tile?[,] ShuffleTilesToMap(List<Tile?> tilesToShuffle) {
			Tile?[,] shuffledMap = new Tile?[boardX, boardY];
			Random random = new Random();

			for (int x = 0; x < boardX; x++) {
				for (int y = 0; y < boardY; y++) {
					int randomTileIndex = random.Next(0, tilesToShuffle.Count - 1);

					shuffledMap[x, y] = tilesToShuffle[randomTileIndex];
					tilesToShuffle.RemoveAt(randomTileIndex);
				}
			}

			return shuffledMap;
		}
	
		public void MoveTile(Tile?[,] sourceMap, int sourceX, int sourceY, Tile?[,] targetMap, int targetX, int targetY) {
			// Validation: coordinates cannot exceed array bounds, source tile can't be null
			if (sourceX > (sourceMap.GetLength(0) - 1) || sourceX < 0 || sourceY > (sourceMap.GetLength(1) - 1) || sourceY < 0) {
				throw new Exception("Source tile out of board bounds!");
			}
			if (targetX > (targetMap.GetLength(0) - 1) || targetX < 0 || targetY > (targetMap.GetLength(1) - 1) || targetY < 0) {
				throw new Exception("Target tile out of board bounds!");
			}
			if (sourceMap[sourceX, sourceY] == null) {
				throw new Exception("Source tile empty!");
			}

			// Need to swap tiles
			if (targetMap[targetX, targetY] != null) {
				Tile? temp = targetMap[targetX, targetY];
				targetMap[targetX, targetY] = sourceMap[sourceX, sourceY];
				sourceMap[sourceX, sourceY] = temp;
			}
			// No need to switch since target tile is null 
			else {
				targetMap[targetX, targetY] = sourceMap[sourceX, sourceY];
				// Overwrite source tile with null to avoid duplication
				sourceMap[sourceX, sourceY] = null;
			}
		}

		public bool WinCheck() {
			for (int x = 0; x < boardX; x++) {
				for (int y = 0; y < boardY; y++) {
					Tile? curTile = puzzleMap[x, y];
					if (curTile == null) {
						return false;
					}

					if (x != 0) {
						Tile? neighbour = puzzleMap[x-1, y];
						if (neighbour != null) {
							if (neighbour.Value.R != curTile.Value.L) {
								return false;
							}
						}
					}

					if (y != 0) {
						Tile? neighbour = puzzleMap[x, y-1];
						if (neighbour != null) {
							if (neighbour.Value.D != curTile.Value.U) {
								return false;
							}
						}
					}

					if (x != (boardX - 1)) {
						Tile? neighbour = puzzleMap[x+1, y];
						if (neighbour != null) {
							if (neighbour.Value.L != curTile.Value.R) {
								return false;
							}
						}
					}

					if (y != (boardY - 1)) {
						Tile? neighbour = puzzleMap[x, y+1];
						if (neighbour != null) {
							if (neighbour.Value.U != curTile.Value.D) {
								return false;
							}
						}
					}
				}
			}

			return true;
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