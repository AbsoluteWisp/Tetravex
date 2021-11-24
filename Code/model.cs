namespace model {
	public class MapGenerator {
		public Tile?[,] GenerateMap(int boardX, int boardY) {
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