using UnityEngine;
using UnityEngine.Events;

public class CameraChangeTrigger : MonoBehaviour
{
    public Transform CamEnterPos;
    public Transform CamExitPos;
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
            if (!passed)
            {
                CamHolder.transform.position = CamEnterPos.transform.position;
                passed = true;
                return;

            }

        }
    }
}
