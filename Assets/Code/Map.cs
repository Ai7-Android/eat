using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public MapData ask(Vector2 cell){
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
		//Vector2 cell = new Vector2 ((int)(position.x/16), (int)(position.y/16));
		return cell;
	}
}
