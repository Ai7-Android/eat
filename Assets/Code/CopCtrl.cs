using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;

public class CopCtrl : MonoBehaviour {

	public Cop _cop; //警察
	public enum Arrow{
		Up,
		Down,
		Left,
		Right,
		Stop,
	};
	private FSM fsm_ = new FSM();
	public Arrow _director = Arrow.Down;
	public Arrow _arrow;

	public int rescueNum
	{
		get {
			return _childList.Count; 
		}

	}



	public List<ChildCtrl> _childList = new List<ChildCtrl>();
	public List<Vector3> _oldPosList = new List<Vector3>();

	private MapCtrl getMap(){
		return Component.FindObjectOfType<MapCtrl> ();
	
	}
	private Vector3 getLocalTarget(){
		return this._cop.gameObject.transform.localPosition + getArrow ();
	
	}
	private Vector3 getArrow(){
		switch(_arrow){
		case Arrow.Left:
			return new Vector3(-16.0f, 0.0f, 0.0f);
		case Arrow.Right:
			return  new Vector3(16.0f, 0.0f, 0.0f);
		case Arrow.Up:
			return new Vector3(0.0f, 0.0f, 16.0f);
		case Arrow.Down:
			return new Vector3(0.0f, 0.0f, -16.0f);
		}
		return new Vector3();

	}
	private Vector3 getWorldTarget(){

		return this._cop.gameObject.transform.position + getArrow ();
	}
	public State getStopState(){
		StateWithEventMap state = TaskState.Create(delegate{
			Task task = new Task();
			task.isOver = delegate {
				if(_arrow != Arrow.Stop){
					return true;
				}
				return false;
			};
			return task;

		}, this.fsm_, "test");

		return state;
	}
			
	private Task turnTask(){

		TweenTask task = new TweenTask(delegate() {

			switch(_arrow){
			case Arrow.Left:
				return TweenRotation.Begin(this._cop.gameObject, 0.1f, Quaternion.AngleAxis(90, Vector3.up));
			case Arrow.Right:
				return TweenRotation.Begin(this._cop.gameObject, 0.1f, Quaternion.AngleAxis(-90, Vector3.up));
			case Arrow.Up:
				return TweenRotation.Begin(this._cop.gameObject, 0.1f, Quaternion.AngleAxis(180, Vector3.up));
			case Arrow.Down:
				return TweenRotation.Begin(this._cop.gameObject, 0.1f, Quaternion.AngleAxis(0, Vector3.up));
			}
			return null;
		});

		return task;
	}

	private Task walkTask(){
		TweenTask task = new TweenTask(delegate() {

			return TweenLocalPosition.Begin(this._cop.gameObject, 0.3f, getLocalTarget());

		
		});
		return task;
	}
	public State getTestState(){
		StateWithEventMap state = TaskState.Create(delegate{
			TaskWait wait = new TaskWait(0.1f);
			return wait;
		}, this.fsm_, delegate(FSMEvent evt) {

			MapCtrl map = this.getMap();

			Vector3 target = getWorldTarget();
			Vector2 cell = map.position2cell(new Vector2(target.x, target.z));
			IMapData data = map.ask(cell);
			bool isPass = data.pass(
				map.position2cell(
					new Vector2(this._cop.gameObject.transform.position.x,
						this._cop.gameObject.transform.position.z)));

			if(isPass){
				return "walk";
			}else{
				_director = Arrow.Stop;
				_arrow = Arrow.Stop;
				return "stop";

			}
		});
	



		return state;

	
	}
	public State getWalkState(){
		StateWithEventMap state = TaskState.Create(delegate{
			TaskSet ts = new TaskSet();
			ts.push(turnTask());
			ts.push(walkTask());
			return ts;
		}, this.fsm_, delegate(FSMEvent evt) {
			return "stop";
		});
		state.onStart += delegate {
			_oldPosList.Insert(0, this._cop.transform.position);

			for(int i = 0; i< this._childList.Count && i<_oldPosList.Count; ++i){
				this._childList[i].mustGo(_oldPosList[i]);
			}

		};
		state.onOver += delegate {

			MapCtrl map = this.getMap();
			Vector2 cell = map.position2cell(new Vector2(_cop.gameObject.transform.position.x, _cop.gameObject.transform.position.z));
			Debug.Log("cell:"+cell);
			IMapData data = map.ask(cell);
			data.trample();

			_director = _arrow;
		};




		return state;
	
	}

	void Start () {
		_cop._triggerback += delegate(Collider other) {
			Debug.Log("eat:" + other.gameObject.name);
			ChildCtrl childCtrl = other.gameObject.GetComponent<ChildCtrl>();
			//HashSet
			ChildCtrl cc  =_childList.Find(delegate(ChildCtrl obj) {
				if(obj == childCtrl){
					return true;
				}
				return false;
			});

			if(cc == null){
				_childList.Add(childCtrl);
			}
		};
		fsm_.addState ("stop", getStopState());
		fsm_.addState("walk", getWalkState());
		fsm_.addState("test", getTestState());
	
		//fsm_.addState ("walk", getWalkState());
		fsm_.init ("stop");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if(_arrow != Arrow.Down)
				_arrow = Arrow.Up;
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//fsm_.post ("goDown");
			if(_arrow != Arrow.Up)
				_arrow = Arrow.Down;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if(_arrow != Arrow.Right)
			_arrow = Arrow.Left;
		}else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if(_arrow != Arrow.Left)
				_arrow = Arrow.Right;
		}

	}
}
	