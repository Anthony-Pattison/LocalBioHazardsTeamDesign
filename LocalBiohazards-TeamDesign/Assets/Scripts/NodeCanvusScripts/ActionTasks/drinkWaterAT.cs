using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class drinkWaterAT : ActionTask {

		public BBParameter<GameObject> cupOfWaterBBP;
		public BBParameter<bool> timmyIsPoisonedBBP = false;
		public BBParameter<Animator> animatorBBP;
		poisonCup poisonCupScript;
		protected override string OnInit() {
			if (cupOfWaterBBP.value.GetComponent<poisonCup>() == null)
			{
				return "The game object in referance does not have the script youre looking for";
			}
			poisonCupScript = cupOfWaterBBP.value.GetComponent<poisonCup>();
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
           
        }
		protected override void OnUpdate()
		{
			if (Vector3.Distance(agent.transform.position, cupOfWaterBBP.value.transform.position) < 7)
            {
                animatorBBP.value.SetTrigger("drinkWater");

                if (poisonCupScript.isPoisoned)
                {
                    timmyIsPoisonedBBP.value = true;
                }
            }
        }
	}
}