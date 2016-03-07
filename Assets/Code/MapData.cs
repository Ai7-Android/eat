using UnityEngine;
using System.Collections;

public class MapData {

	private bool pass_ = false;
	public MapData(bool pass){

		pass_ = pass;
	}
	public bool pass(Vector2 from){
		return pass_;
	}


}
