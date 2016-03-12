using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GDGeek{
	public class MapFactory : MonoBehaviour {
		public MapStore _store = null;

		private List<IMapBuilder> _builders = new List<IMapBuilder> ();
		public void add(IMapBuilder builder){
			_builders.Add (builder);
		}
		public MapStore store{

			get{ 
				return _store;
			}
		}
		public int hasUp(string[,] field, int i, int j){
			int x = i;
			int y = j + 1;
			if (x < 0 || x >= field.GetLength (0) || y < 0 || y >= field.GetLength (1)) {
				return 0;
			}

			if (field [x,y] != field [i,j]) {
				return 0;
			}
			return (int)MapStore.Around.Up;
		}
		public int hasDown(string[,] field, int i, int j){

			int x = i;
			int y = j - 1;
			if (x < 0 || x >= field.GetLength (0) || y < 0 || y >= field.GetLength (1)) {
				return 0;
			}
			if (field [x,y] != field [i,j]) {
				return 0;
			}
			return (int)MapStore.Around.Down;
		}
		public int hasLeft(string[,] field, int i, int j){
			int x = i-1;
			int y = j;
			if (x < 0 || x >= field.GetLength (0) || y < 0 || y >= field.GetLength (1)) {
				return 0;
			}
			if (field [x,y] != field [i,j]) {
				return 0;
			}
			return (int)MapStore.Around.Left;
		}
		public int hasRight(string[,] field, int i, int j){
			int x = i+1;
			int y = j;
			if (x < 0 || x >= field.GetLength (0) || y < 0 || y >= field.GetLength (1)) {
				return 0;
			}
			if (field [x,y] != field [i,j]) {
				return 0;
			}
			return (int)MapStore.Around.Right;
		}
		public VoxelStruct getFloor(string[,] field){

			JoinVoxel join = new JoinVoxel ();
			for(int i=0;i<field.GetLength(0); ++i){
				for(int j=0; j<field.GetLength(1); ++j){
					int n = hasUp (field, i, j) | hasDown (field, i, j) | hasLeft (field, i, j) | hasRight (field, i, j);

					VoxelStruct c = store.getCell (field[i, j], n);
					join.addVoxel(c, new VectorInt3(16 * i, 16 * j, 0));
				}

			}
			VoxelStruct vs = join.doIt ();
			return vs;
		}
		public VoxelStruct render(MapModel model){
			for (int i = 0; i < _builders.Count; ++i) {
				_builders[i].render (model, store);
			}
			return model.vs;
		}

		public MapModel create(){
			MapModel model = new MapModel ();
			model.clear ();


			for (int i = 0; i < _builders.Count; ++i) {
				_builders[i].build (model);
			}

		

			return model;
		}
	}
}