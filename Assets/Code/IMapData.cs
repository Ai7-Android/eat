using UnityEngine;
using System.Collections;
namespace GDGeek{
public interface IMapData {


	bool pass (VectorInt2 from);
	void trample ();
	/*
	private bool pass_ = false;
	public MapData(bool pass){
		pass_ = pass;
	}
	public bool pass(Vector2 from){
		return pass_;
	}

	public void trample(){
		
	
	}*/

}
}