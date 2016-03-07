using UnityEngine;
using UnityEditor;
using GDGeek;
using System.IO;
using System.Collections.Generic;


namespace GDGeek{
	public class SpliceVoxel
	{
		public struct Packed
		{
			public VoxelStruct vs;
			public VectorInt3 offset;

		}
		private HashSet<Color> palette_ = new HashSet<Color>();
		private Dictionary<VectorInt3, VoxelData> dictionary_ = new Dictionary<VectorInt3, VoxelData>();
		private List<Packed> list_ = new List<Packed>();
		private VectorInt3 min_ = new VectorInt3(9999, 9999, 9999);
		private VectorInt3 max_ = new VectorInt3(-9999, -9999,-9999);

		public void addVoxel(VoxelStruct vs, VectorInt3 offset){
			Packed packed = new Packed ();
			packed.vs = vs;
			packed.offset = offset;
			list_.Add (packed);
		}
		public void clear(){
			palette_.Clear ();
			dictionary_.Clear ();
			min_ = new VectorInt3(9999, 9999, 9999);
			max_ = new VectorInt3(-9999, -9999,-9999);
		}
		public void readIt(Packed packed){
			for (int i = 0; i < packed.vs.datas.Length; ++i) {
				palette_.Add (packed.vs.datas[i].color);

				VectorInt3 pos = new VectorInt3 (packed.vs.datas [i].x, packed.vs.datas [i].y, packed.vs.datas [i].z) + packed.offset;
				dictionary_ [pos] = packed.vs.datas [i];

				min_.x = Mathf.Min (pos.x, min_.x);
				min_.y = Mathf.Min (pos.y, min_.y);
				min_.z = Mathf.Min (pos.z, min_.z);
				max_.x = Mathf.Max (pos.x, max_.x);
				max_.y = Mathf.Max (pos.y, max_.y);
				max_.z = Mathf.Max (pos.z, max_.z);

			}
		
		
		}
		public VoxelData[] getDatas(){
			VoxelData[] datas = new VoxelData[dictionary_.Count];
			int i = 0;
			foreach(KeyValuePair<VectorInt3, VoxelData> item in dictionary_){
				VoxelData data = new VoxelData ();
				data.color = item.Value.color;
				data.x = item.Key.x;
				data.y = item.Key.y;
				data.z = item.Key.z;

				data.id = i;
				datas [i] = data;
				++i;
			}
			return datas;
		}
		public VectorInt4[] getPalette(){
			int size = Mathf.Max (palette_.Count, 256);
			VectorInt4[] palette = new VectorInt4[size];
			int i = 0;
			foreach (Color c in palette_)
			{
				palette [i] = VoxelFormater.Color2Bytes (c);
				++i;
			}
			//for(int i =0; i<)
			return palette;
		}

		//public 
		public VoxelStruct spliceAll(){

			this.clear ();
			for (int i = 0; i < list_.Count; ++i) {
				Packed p = this.list_ [i];
				this.readIt(p);
			}

			VoxelStruct vs = new VoxelStruct();

			vs.main = new VoxelStruct.Main ();
			vs.main.name = "MAIN";
			vs.main.size = 0;

		
			vs.size = new VoxelStruct.Size ();
			vs.size.name = "SIZE";
			vs.size.size = 12;
			vs.size.chunks = 0;

			vs.size.box = new VectorInt3 ();
		

			vs.size.box.x = this.max_.x - this.min_.x +1;
			vs.size.box.y = this.max_.y - this.min_.y +1;
			vs.size.box.z = this.max_.z - this.min_.z +1;


			vs.rgba = new VoxelStruct.Rgba ();//list_ [0].vs.rgba;
			vs.rgba.palette = this.getPalette ();

			vs.rgba.size = vs.rgba.palette.Length * 4;
			vs.rgba.name = "RGBA";
			vs.rgba.chunks = 0;

			/**/
			vs.datas = this.getDatas ();
			Debug.Log (vs.datas.Length);
			vs.version = 150;


			vs.main.chunks = 52 + vs.rgba.palette.Length *4 + vs.datas.Length *4;
			Debug.Log (vs.main.chunks);
			return vs;

		}
	}
}

