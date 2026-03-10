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
    [System.Serializable]
    public struct EnterEventData
    {
        public string Zone;
        public Vector3 PlayerPos;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            eventCore.updateParallaxScrollingEV.Invoke();
            CamHolder.transform.position = CamEnterPos.transform.position;
            passed = true;

            var data = new EnterEventData()
            {
                Zone = name,
                PlayerPos = other.gameObject.transform.position

            };
            TelemetryLogger.Log(this, "Entered", data);


        }
    }

}
