using UnityEngine;
using System.Collections;

public class MapExport : IMapData {

	public MapExport(){
		
	}
	public bool pass(Vector2 from){
		Debug.Log ("!!!!");
		return true;
	}
	private bool _out = false;
	public bool isOut(){
		return _out;
	}
	public void trample(){
		Debug.Log ("out door");
		_out = true;

	}
}
