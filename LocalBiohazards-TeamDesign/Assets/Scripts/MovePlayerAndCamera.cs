using UnityEngine;
using UnityEngine.AI;

public class MovePlayerAndCamera : MonoBehaviour
{

    public Transform CamEnterPos;
    public Transform PlayerEnterPos;
    public Transform PlayerPosition;
    public bool passed = false;
    GameObject CamHolder;
    private void Start()
    {
        CamHolder = GameObject.Find("CameraHolder");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPosition.position = PlayerEnterPos.position;
            PlayerPosition.gameObject.GetComponent<NavMeshAgent>().ResetPath();
            CamHolder.transform.position = CamEnterPos.transform.position;
            passed = true;
        }
    }
}
