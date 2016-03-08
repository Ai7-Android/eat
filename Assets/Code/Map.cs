using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public GameObject _door = null;
	public void init(){
		_door.gameObject.SetActive (false);
	}
	public void openDoor(){
		_door.gameObject.SetActive (true);
	}
}
