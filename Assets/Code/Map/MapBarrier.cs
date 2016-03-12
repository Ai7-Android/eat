using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class MapBarrier : IMapBuilder {


		private string block_;
		public MapBarrier(){
			
		}
		public void block(string type){
			block_ = type;

		}
		private int testIJ(string [,] field, int i, int j, MapStore.Around around){
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
			if (field [i, j] == block_ && field [x,y] != block_) {
				return 0;
			}
			return (int)around;
		}
		public void render (MapModel map, MapStore store){
			//return 
		}
		public void build(MapModel map){
			int[,] barrier = new int[map.field.GetLength(0), map.field.GetLength(1)];
			for (int i = 0; i < barrier.GetLength (0); ++i) {
				for (int j = 0; j < barrier.GetLength (0); ++j) {
					int n = testIJ (map.field, i, j, MapStore.Around.Up) |
					        testIJ (map.field, i, j, MapStore.Around.Down) |
					        testIJ (map.field, i, j, MapStore.Around.Left) |
					        testIJ (map.field, i, j, MapStore.Around.Right);

//					Debug.Log ("i" +i+ "j"+ j+":"+n);
					barrier [i, j] = n;
				}
			}
			map.barrier = barrier;
		}

	}
}