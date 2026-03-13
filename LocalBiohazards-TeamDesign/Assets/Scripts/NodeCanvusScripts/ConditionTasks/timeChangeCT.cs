using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {
	public class timeChangeCT : ConditionTask {
		public EventCore eventcore;
        bool finishAction = false;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
			eventcore.TurnOfTheMinute.AddListener(onMinuteTurn);
			return null;
		}
		void onMinuteTurn(float number)
		{
			finishAction = true;
		}
		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			finishAction = false;
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			return finishAction;
		}
	}
}