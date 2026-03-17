using UnityEngine;
using UnityEngine.AI;

public class changeNavSpeed : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform cameraTransform;
    private void Start()
    {
        cameraTransform = GameObject.Find("CameraHolder").transform;
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
}
