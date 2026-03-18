using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion.Serialization.FullSerializer;
using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;

namespace NodeCanvas.Tasks.Actions
{

    public class changeRoomAT : ActionTask
    {

        public locationClass locations;
        public BBParameter<NavMeshAgent> navAgentBBP;
        public currentLocation[] loactionsToCycleThrough;
        public currentLocation goToLocation;
        public BBParameter<Transform> locationToMoveToBBP;
        public BBParameter<float> stoppingDistanceBBP = 2;

        int roomNumber;
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            currentLocation _temp = (currentLocation)1;
            foreach (var location in locations.aiLocations)
            {
                if (location.location == loactionsToCycleThrough[roomNumber])
                {
                    _temp = location.location;
                    locationToMoveToBBP.value = location.locationTransform;
                    roomNumber++;
                    if (roomNumber > loactionsToCycleThrough.Length -1)
                    {
                        roomNumber = 0;
                    }
                    break;
                }
            }

            navAgentBBP.value.SetDestination(locationToMoveToBBP.value.position);
            agent.GetComponent<victimFSMClass>().currentLocation = _temp;
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {

            if (Vector3.Distance(agent.transform.position, locationToMoveToBBP.value.position) < stoppingDistanceBBP.value)
            {
                EndAction();
            }
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            currentLocation nextLocation = (currentLocation)UnityEngine.Random.RandomRange(0, 7);
            if (nextLocation == goToLocation)
            {
                goToLocation = (currentLocation)3;
            }
        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
    }
}