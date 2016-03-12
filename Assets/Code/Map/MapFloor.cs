using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class MapFloor : IMapBuilder{
		private string[,] field_;
		public MapFloor (VectorInt2 size) {
			field_ = new string[size.x, size.y];
		}

		public int has(string[,] field, int i, int j, MapStore.Around around){
			int x = i;
			int y = j;

			switch(around){
			case MapStore.Around.Right:
				++x;
				break;
			case MapStore.Around.Left:
				--x;
				break;

			case MapStore.Around.Up:
				++y;
				break;
			case MapStore.Around.Down:
				--y;
				break;

			}

			if (x < 0 || x >= field.GetLength (0) || y < 0 || y >= field.GetLength (1)) {
				return 0;
			}
			if (field [x,y] != field [i,j]) {
				return 0;
			}
			return (int)around;
		}
		public VoxelStruct getFloor(string[,] field, MapStore store){

			JoinVoxel join = new JoinVoxel ();
			for(int i=0;i<field.GetLength(0); ++i){
				for(int j=0; j<field.GetLength(1); ++j){
					int n = has(field, i, j,MapStore.Around.Up) 
						| has (field, i, j,MapStore.Around.Down) 
						| has (field, i, j,MapStore.Around.Left) 
						| has (field, i, j,MapStore.Around.Right);

					VoxelStruct c = store.getCell (field[i, j], n);
					join.addVoxel(c, new VectorInt3(16 * i, 16 * j, 0));
				}

			}
			VoxelStruct vs = join.doIt ();
			return vs;
		}


		public void render(MapModel model, MapStore store){
			model.vs = this.getFloor (field_, store);
		}
		public void build(MapModel model){
			model.field = field_;

		}
		public void cell(string type, int i, int j){
			field_ [i, j] = type;
		}
		public void def(string type){
			for (int i = 0; i < field_.GetLength (0); ++i) {
				for (int j = 0; j < field_.GetLength (1); ++j) {
					if (string.IsNullOrEmpty (field_ [i, j])) {
						field_ [i, j] = type;
					}
				}
			}
		}

	}
}