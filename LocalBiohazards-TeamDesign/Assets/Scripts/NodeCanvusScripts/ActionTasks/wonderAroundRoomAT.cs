using NodeCanvas.Framework;
using ParadoxNotion.Serialization.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Composites;
using static UnityEditor.FilePathAttribute;

namespace NodeCanvas.Tasks.Actions {

	public class wonderAroundRoomAT : ActionTask {
		public BBParameter<NavMeshAgent> navAgent;
		public float randomDistance;
		Vector3 wonderSpot;
		Vector3 Destination;
		public BBParameter<Transform> wonderSpotTransformBBP;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

			wonderSpot = wonderSpotTransformBBP.value.position;
			setNewDestination();
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			float distanceToDestination = Vector3.Distance(agent.transform.position, Destination);
			Debug.DrawLine(agent.transform.position, Destination, Color.red);
            Debug.Log(distanceToDestination);
			if ( distanceToDestination < 2.0f)
			{
				setNewDestination();
            }
		}

		void setNewDestination()
		{
			Vector3 Temp = randomDistance * Random.insideUnitSphere + wonderSpotTransformBBP.value.position;
			NavMeshHit hit = new NavMeshHit();
			if (!NavMesh.SamplePosition(Temp, out hit, randomDistance, NavMesh.AllAreas))
			{
				Debug.LogError("didnt find a location");
				setNewDestination();
			}
			else
			{
                Destination = hit.position;
				navAgent.value.SetDestination(Destination);
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