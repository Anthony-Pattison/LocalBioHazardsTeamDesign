using UnityEngine;
using UnityEngine.Events;

public class CameraChangeTrigger : MonoBehaviour
{
    public UnityEvent ChangeCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ChangeCamera.Invoke();
        }
    }
}
