using UnityEngine;
using System.Collections;
using GDGeek;
public class Layout	 : MonoBehaviour {

	public void doIt(){
		VoxelMaker[] maker = this.gameObject.GetComponentsInChildren<VoxelMaker> ();

		Debug.Log ("!!!!" + maker.Length);
		for (int i = 0; i < maker.Length; ++i) {
			Vector3 pos = maker [i].gameObject.transform.position;
			pos.x = 16 * i;
			maker [i].gameObject.transform.position = pos;
		}
	}
}
