using UnityEngine;
using System.Collections;
namespace GDGeek{
	public class Play : MonoBehaviour {

		public CopCtrl _cop = null;
		public MapCtrl _map = null;
		public FSM _fsm = new FSM ();
		private bool weakup_ = false;
		private State sleepState(){
			StateWithEventMap sleep = new StateWithEventMap ();
			sleep.addAction ("weakup", "weakup");
			return sleep;
		}
		private State weakupState(){
			StateWithEventMap weakup = new StateWithEventMap ();
			weakup.onStart += delegate() {
				weakup_ = true;
			};
			weakup.onOver += delegate() {
				weakup_ = false;
			};
			return weakup;
		}
		private State rescueState(){

			StateWithEventMap rescue = TaskState.Create (delegate() {
				return new TaskCheck(delegate{
					return (_cop.rescueNum == 2);
				});
			}, this._fsm, "escape");

			return rescue;
		}
		private State escapeState(){
			StateWithEventMap escape = TaskState.Create (delegate() {
				return new TaskCheck(delegate{
					return _map.isEscape;
				});

			}, this._fsm, "sleep");
			escape.onStart += delegate {
				_map.open();
			};
			return escape;
		}

		public void Start(){
			_fsm.addState ("sleep", sleepState (), "");
			_fsm.addState ("weakup", "rescue", weakupState (), "");
			_fsm.addState("rescue", rescueState(), "weakup");
			_fsm.addState("escape", escapeState(), "weakup");
			_fsm.init ("sleep");
			TaskManager.Run (this.running ());

		}
		public Task running(){

			Task task = new Task ();

			task.init = delegate {
				//this.gameObject.SetActive(true);
				_fsm.post("weakup");
			};
			task.isOver = delegate {
			//	Debug.Log("what!!!!");
				//this.gameObject.SetActive(false);
				return 	!weakup_;
			};
			task.shutdown = delegate {
				this.gameObject.SetActive (false);
			};
			return task;
		}

	}
}
