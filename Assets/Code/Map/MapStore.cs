using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;


namespace GDGeek{
	public class MapStore : MonoBehaviour {
		[Serializable]
		public class MapItem{
			public string type;
			public TextAsset assert;
			public VoxelStruct[] vstruct = null;

			//
			//public VectorInt3
		};
		public List<MapItem> _items = new List<MapItem>();
		private List<SplitVoxel.Box> boxes_;
		public enum Around{
			None = 0,
			Up = 1,
			Down = 2,
			Left = 4,
			Right = 8,

		}
		private VoxelStruct[] loadAssert(TextAsset assert){


			Stream sw = new MemoryStream(assert.bytes);
			System.IO.BinaryReader br = new System.IO.BinaryReader (sw); 

			VoxelStruct vs = VoxelFormater.ReadFromMagicaVoxel (br);
			SplitVoxel split = new SplitVoxel (vs);
//			Debug.Log ("!!!" + vs.datas.Count);
			for (int i = 0; i < boxes_.Count; ++i) {
				split.addBox (boxes_[i]);
			}

			VoxelStruct[] vss = split.doIt ();

			return vss;

			//return null;
		}
		public VoxelStruct[] getCells(string type){
			for(int i = 0; i<_items.Count; ++i){
				if (_items [i].type == type) {
					if (_items [i].vstruct == null) {
						_items [i].vstruct = this.loadAssert (_items [i].assert);
					}
					return _items [i].vstruct;
				}
			}
			return null;
		}
		public VoxelStruct getCell(string type, int around){
			VoxelStruct[] vs = getCells (type);
			if (vs == null || vs.Length <= around) {
				return null;
			}
			return vs [around];
			/*
			for(int i = 0; i<_items.Count; ++i){
				if (_items [i].type == type) {
					if (_items [i].vstruct == null) {
						_items [i].vstruct = this.loadAssert (_items [i].assert);
					}
					return _items [i].vstruct [around];
				}
			}
			return null;*/
		}
		public void init(){
			boxes_ = new List<SplitVoxel.Box> ();

			boxes_.Add(new SplitVoxel.Box(new VectorInt3(0,54,0), new VectorInt3(16,16,3)));//3
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(0,0,0), new VectorInt3(16,16,3)));//0
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(0,34,0), new VectorInt3(16,16,3)));//2
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(0,17,0), new VectorInt3(16,16,3)));//1


			boxes_.Add(new SplitVoxel.Box(new VectorInt3(54,54,0), new VectorInt3(16,16,3)));//15
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(54,0,0), new VectorInt3(16,16,3)));//12
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(54,34,0), new VectorInt3(16,16,3)));//14
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(54,17,0), new VectorInt3(16,16,3)));//13

			boxes_.Add(new SplitVoxel.Box(new VectorInt3(20,54,0), new VectorInt3(16,16,3)));//7
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(20,0,0), new VectorInt3(16,16,3)));//4
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(20,34,0), new VectorInt3(16,16,3)));//6
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(20,17,0), new VectorInt3(16,16,3)));//5


			boxes_.Add(new SplitVoxel.Box(new VectorInt3(37,54,0), new VectorInt3(16,16,3)));//11
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(37,0,0), new VectorInt3(16,16,3)));//8
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(37,34,0), new VectorInt3(16,16,3)));//10
			boxes_.Add(new SplitVoxel.Box(new VectorInt3(37,17,0), new VectorInt3(16,16,3)));//9


		}
		public void Start(){
			init ();
		}
	}
}
