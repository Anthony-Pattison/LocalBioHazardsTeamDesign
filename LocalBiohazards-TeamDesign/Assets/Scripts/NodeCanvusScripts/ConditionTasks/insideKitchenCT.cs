using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class insideKitchenCT : ConditionTask {
		victimFSMClass victimFSMClass;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
            victimFSMClass = agent.GetComponent<victimFSMClass>();
			return null;
		}

		protected override bool OnCheck() {
			if (victimFSMClass.currentLocation == currentLocation.kitchen)
			{
				return true;
			}
			return false;
		}
	}
}