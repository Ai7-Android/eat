using UnityEngine;
using System.Collections;
namespace GDGeek{
	[ExecuteInEditMode]
	public class VoxelMapMaker : MonoBehaviour {
		/*public TextAsset _voxFile = null;
		public bool _building = true;
		public VoxelTextAssetLoader _loader = null;	
		public VoxelModel _model = null;
		public VoxelFileMaker _file = null;
	


		// Update is called once per frame
		void Update () {
			if (_building == true) {
				

				initFile ();
				initModel();
				initLoader();
				/*initMesh();
				if(!_director.empty){
					_director.clear ();
				}	


				_loader.read();

				if(_director.empty){
					_director.build (_model.data);

				}	
				VoxelMaker[] makers = this.gameObject.GetComponentsInChildren<VoxelMaker> ();
				for (int i = 0; i < makers.Length; ++i) {
					Vector3 offset = makers [i].gameObject.transform.localPosition;
					offset.x = Mathf.Round (offset.x);
					offset.y = Mathf.Round (offset.y);
					offset.z = Mathf.Round (offset.z);
					makers [i].gameObject.transform.localPosition = offset;
					makers [i]._loader.read ();
//					Debug.Log (makers [i]._model.data.Length);
					_file.addFile (makers [i]._model, new VectorInt3(offset));
//					Debug.Log (offset);
				}
				_file.save ();
				_building = false;	

			}
		}*/

		public VoxelStruct building(){
			
			VoxelMaker[] makers = this.gameObject.GetComponentsInChildren<VoxelMaker> ();
			SpliceVoxel splice = new SpliceVoxel ();
			for (int i = 0; i < makers.Length; ++i) {
				Vector3 offset = makers [i].gameObject.transform.localPosition;
				offset.x = Mathf.Round (offset.x);
				offset.y = Mathf.Round (offset.y);
				offset.z = Mathf.Round (offset.z);
				makers [i].gameObject.transform.localPosition = offset;
				makers [i]._loader.read ();
				Debug.Log (offset);
				splice.addVoxel (makers [i]._model.vs, new VectorInt3((int)offset.x,  (int)offset.z, (int)offset.y));
				//makers [i]._loader.read ();
				//					Debug.Log (makers [i]._model.data.Length);
				//_file.addFile (makers [i]._model, );
				//					Debug.Log (offset);
			}
			VoxelStruct vs = splice.spliceAll ();
			return vs;
		}
	}

}