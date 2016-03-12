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
					return (_cop.rescueNum == 3);
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
				MapFactory factory = Component.FindObjectOfType<MapFactory> ();
				MapFloor floor = new MapFloor (new VectorInt2(7, 7));


				floor.cell ("road", 0, 0);
				floor.cell ("road", 1, 0);
				floor.cell ("road", 3 ,0);
				floor.cell ("road",  4 ,0);
				floor.cell ("road",  5,0);
				floor.cell ("road", 6,0 );

				floor.cell ("road", 0,1);
				floor.cell ("road", 1, 1);

				floor.cell ("road", 3,1 );
				floor.cell ("road", 4,1 );
				floor.cell ("road", 5,1);
				floor.cell ("road", 6,1);


				floor.cell ("road", 0,2);
				floor.cell ("road", 1,2);

				floor.cell ("road", 3,2);
				floor.cell ("road", 4,2);
				floor.cell ("road", 5,2);
				floor.cell ("road", 6,2);


				floor.cell ("road", 0,3);
				floor.cell ("road", 1,3);
				floor.cell ("road", 2,3);
				floor.cell ("road", 3,3);
				floor.cell ("road", 4,3);
				floor.cell ("road", 5,3);
				floor.cell ("road", 6,3);

				floor.cell ("road", 6,4);
				floor.cell ("road", 6,5);
				floor.cell ("road", 6,6);

				floor.cell ("road", 5,5);
				floor.cell ("road", 5,6);


				floor.cell ("road", 4,5);
				floor.cell ("road", 4,6);

				floor.cell ("road", 3,5);
				floor.cell ("road", 3,6);
				floor.cell ("road", 2,5);
				floor.cell ("road", 2,6);
				floor.cell ("road", 1,5);
				floor.cell ("road", 1,6);
				floor.cell ("road", 0,5);
				floor.cell ("road", 0,6);

				floor.def("grass");

				factory.add (floor);

				MapBarrier barrier = new MapBarrier ();
				barrier.block("grass");

				factory.add (barrier);
				MapModel model = factory.create();

				MapCtrl ctrl = Component.FindObjectOfType<MapCtrl> ();
				ctrl.setup(model);

				_fsm.post("weakup");
			};
			task.isOver = delegate {
			//	Debug.Log("what!!!!");
				//this.gameObject.SetActive(false);
				return 	!weakup_;
			};
			task.shutdown = delegate {
				Debug.Log("Game fin!");
			};
			return task;
		}

	}
}
