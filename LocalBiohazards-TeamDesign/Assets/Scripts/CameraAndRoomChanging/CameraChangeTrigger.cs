using UnityEngine;
using UnityEngine.Events;

public class CameraChangeTrigger : MonoBehaviour
{
    EventCore eventCore;
    
    public Transform CamEnterPos;
    public Transform CamExitPos;
    public bool passed = false;
    GameObject CamHolder;
    private void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        CamHolder = GameObject.Find("CameraHolder");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            eventCore.updateParallaxScrollingEV.Invoke();
            CamHolder.transform.position = CamEnterPos.transform.position;
            passed = true;

            TelemetryLogger.Log(this, gameObject.name);


        }
    }
}
