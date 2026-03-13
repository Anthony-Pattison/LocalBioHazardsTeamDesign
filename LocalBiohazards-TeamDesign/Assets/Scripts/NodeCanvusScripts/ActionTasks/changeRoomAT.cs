using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;
using ParadoxNotion.Serialization.FullSerializer;

namespace NodeCanvas.Tasks.Actions {

	public class changeRoomAT : ActionTask {

		public locationClass locations;
		public BBParameter<NavMeshAgent> navAgentBBP;
		public currentLocation goToLocation;
		public BBParameter<Transform> locationToMoveToBBP;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			for (int i = 0; i < locations.aiLocations.Length; i++) {

				if (locations.aiLocations[i].location == goToLocation)
				{
                    locationToMoveToBBP.value = locations.aiLocations[i].locationTransform;

                    navAgentBBP.value.SetDestination(locationToMoveToBBP.value.position);
					break;
				}
			}
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            Debug.Log(Vector3.Distance(agent.transform.position, locationToMoveToBBP.value.position));

            if (Vector3.Distance(agent.transform.position, locationToMoveToBBP.value.position) < 2)
			{
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