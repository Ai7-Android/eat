using UnityEngine;
using System.Collections;
using GDGeek;

public class Child : MonoBehaviour {
	public delegate void TriggerBack (Collider other);
	public event TriggerBack _triggerback;
	public MeshRenderer _mesh; 
	void OnTriggerEnter( Collider other ){
	//	Debug.Log ("i get " + other.gameObject.name);
		_triggerback (other);
	}
}
