using UnityEngine;
using System.Collections;

public class Cop : MonoBehaviour {
	public delegate void TriggerBack (Collider other);
	public event TriggerBack _triggerback;
	void OnTriggerEnter( Collider other ){
		Debug.Log ("i get " + other.gameObject.name);
		_triggerback (other);
	}
}
