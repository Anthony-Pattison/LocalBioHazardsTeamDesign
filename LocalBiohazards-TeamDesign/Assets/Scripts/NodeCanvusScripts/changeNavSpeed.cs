using UnityEngine;
using UnityEngine.AI;

public class changeNavSpeed : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform cameraTransform;
    EventCore eventCore;

    private void Start()
    {
        cameraTransform = GameObject.Find("CameraHolder").transform;
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
    }

    private void Update()
    {
        Vector3 CamPos = cameraTransform.position;
        if (cameraTransform != null)
        {
            CamPos.y = transform.position.y;
            transform.LookAt(CamPos);
        }
    }
    public void changeSpeed(float newSpeed)
    {
        agent.speed = newSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            eventCore.resetGameState.Invoke();
        }
    }
}
