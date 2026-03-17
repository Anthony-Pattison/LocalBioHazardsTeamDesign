using NodeCanvas.StateMachines;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class changeNavSpeed : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject weponNeededToKill;
    public Animator animator;
    Transform cameraTransform;
    EventCore eventCore;

    [HideInInspector]
    public bool killed = false;
    
    bool dead = false;
    private void Start()
    {
        cameraTransform = GameObject.Find("CameraHolder").transform;
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        displayWepon(false);
    }

    private void Update()
    {
        Vector3 CamPos = cameraTransform.position;
        if (cameraTransform != null)
        {
            CamPos.y = transform.position.y;
            transform.LookAt(CamPos);
        }

        killCharacter(killed);
    }
    public void displayWepon(bool state)
    {
        if (!dead) 
            weponNeededToKill.SetActive(state);
    }
    public void changeSpeed(float newSpeed)
    {
        agent.speed = newSpeed;
    }

    void killCharacter(bool die)
    {
        if (die == false)
            return;
        animator.SetTrigger("dead");
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<FSMOwner>().enabled = false;
        killed = false;
        displayWepon(false);
        dead = true;
        TelemetryLogger.Log(this, "NPC Killed", $"NPC Name: {name}, Location: {transform.position}");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TelemetryLogger.Log(this, "Failure By NPC", $"NPC Name: {name}, Location: {transform.position}");
            eventCore.resetGameState.Invoke();
        }
    }
}
