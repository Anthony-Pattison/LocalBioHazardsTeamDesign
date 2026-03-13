using UnityEngine;
using UnityEngine.AI;

public class changeNavSpeed : MonoBehaviour
{
    public NavMeshAgent agent;
    
    public void changeSpeed(float newSpeed)
    {
        agent.speed = newSpeed;
    }
}
