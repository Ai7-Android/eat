using UnityEngine;
using System.Collections;
using GDGeek;

public class ChildCtrl : MonoBehaviour {
	public Child _child;
	public FSM _fsm = new FSM();
	private bool isDie_ = false;
	public Vector3 _position = new Vector3();
	public void mustGo(Vector3 position){
		this._position = position;
	}
	public bool isDie{
		get{ 
			return isDie_;
		}
	}
	public State getWaitState(){
		StateWithEventMap state = TaskState.Create(delegate{
			Task task = new Task();
			task.isOver = delegate {
				if(_position != this._child.transform.position){
					return true;
				}
				return false;
			};
			return task;

		}, this._fsm, "follow");

		return state;
	}
	public State getFollowState(){

		StateWithEventMap state = TaskState.Create (delegate {

			TaskSet ts = new TaskSet();
			TweenTask turn  = new TweenTask(delegate() {
				Vector3 v = this._position - this._child.transform.position;
				if(Mathf.Abs(v.x)>  Mathf.Abs(v.z)){
					if(v.x > 0){		
						return TweenRotation.Begin(this._child.gameObject, 0.1f, Quaternion.AngleAxis(-90, Vector3.up));
						
					}else{
						return TweenRotation.Begin(this._child.gameObject, 0.1f, Quaternion.AngleAxis(90, Vector3.up));
					}

				}else{
					if(v.z > 0){		
						return TweenRotation.Begin(this._child.gameObject, 0.1f, Quaternion.AngleAxis(180, Vector3.up));

					}else{
						return TweenRotation.Begin(this._child.gameObject, 0.1f, Quaternion.AngleAxis(0, Vector3.up));
					}

				}
				return TweenRotation.Begin(this._child.gameObject, 0.1f, Quaternion.AngleAxis(-90, Vector3.up));
			});

			TweenTask walk = new TweenTask(delegate() {
				return TweenWorldPosition.Begin(this._child.gameObject, 0.3f, _position);
			});


			ts.push(walk);
			ts.push(turn);
			return ts;
		}, this._fsm, delegate {
			return "wait";
		});






		return state;

	}
	/*
	public State getFollowState(){
		StateWithEventMap state = TaskState.Create(delegate{
			TweenTask task = new TweenTask(delegate() {
				return TweenWorldPosition.Begin(this._child.gameObject, 0.3f, _position);
			});
			return task;

		}, this._fsm, "wait");


		return state;
	}*/
	void Start () {
		isDie_ = false;
		this._child._triggerback += delegate(Collider other) {
			AlienCtrl alien = other.gameObject.GetComponent<AlienCtrl>();
			if(alien != null){
				isDie_ = true;
				this._child._mesh.gameObject.SetActive(false);
				Debug.Log(" yes , i die!");
			}	
		};
		_position = this._child.transform.position;


		_fsm.addState ("wait", getWaitState());
		_fsm.addState ("follow", getFollowState ());
		_fsm.init ("wait");
	}
	

}
