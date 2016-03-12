using UnityEngine;
using System.Collections;

public class MapCtrl : MonoBehaviour {

	private bool isOpen_ = false;
	public Map _map = null;
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
	public Vector2 export(){
		return new Vector2 (7, 5);
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
	public IMapData ask(Vector2 cell){
		if (isOpen_ && cell == export ()) {
			return export_;
		}
		if (cell.x < 0 || cell.y < 0 || cell.y > 7 || cell.x > 7) {
			return new MapData(false);
		}

		return  new MapData(true);
	} 

	public Vector2 position2cell(Vector2 position){
		Vector2 cell = new Vector2 ();
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
