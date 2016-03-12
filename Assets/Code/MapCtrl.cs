using UnityEngine;
using System.Collections;
namespace GDGeek{

	public class MapCtrl : MonoBehaviour {

		private bool isOpen_ = false;
		public Map _map = null;
		private MapModel model_ = null;
		private MapExport export_ = new MapExport ();

		public bool isEscape{
			get{ 
				return export_.isOut ();
			}
		}
		public void Start(){
			initialize ();
		}
		public void initialize(){
			isOpen_ = false;
			//this._map.init ();
		
		}
		public VectorInt2 export(){
			return new VectorInt2 (0, 6);
		}
		public bool solve(){
			return export_.isOut ();
		}
		public bool isOpen(){
			return isOpen_;
		}
		public void open(){
			Debug.Log ("isOpen");
			isOpen_ = true;
			_map.openDoor ();
			
		}
		public void close(){

			isOpen_ = false;
		}


		public void setup(MapModel model){
			model_ = model;
		
		}
		public IMapData ask(VectorInt2 cell){
			if (isOpen_ && cell == export ()) {
				return export_;
			}
			if (cell.x < 0 || cell.y < 0 || cell.x >= model_.field.GetLength(0) || cell.y >= model_.field.GetLength(1)) {
				return new MapData(false);
			}
			if (model_.field [cell.x, cell.y] == "grass") {
				return new MapData(false);
			}
			return new MapData(true);
		} 

		public VectorInt2 position2cell(Vector2 position){
			VectorInt2 cell = new VectorInt2 ();
			if (position.x >= 0.0f) {
				cell.x = (int)(position.x / 16);
			} else {
				cell.x = (int)(position.x / 16)-1;
			}


			if (position.y >= 0.0f) {
				cell.y = (int)(position.y / 16);
			} else {
				cell.y = (int)(position.y / 16)-1;
			}

			return cell;
		}
		public void clear(){
			
		}
	}
}