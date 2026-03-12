using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;
using ParadoxNotion.Serialization.FullSerializer;

namespace NodeCanvas.Tasks.Actions {

	public class changeRoomAT : ActionTask {

		public locationClass locations;
		public BBParameter<NavMeshAgent> navAgentBBP;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			navAgentBBP.value.SetDestination(locations.aiLocations[0].locationTransform.position);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            if (Vector3.Distance(agent.transform.position, locations.aiLocations[0].locationTransform.position) < 1)
			{
				Debug.Log("made it to the position");
				EndAction();
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}