using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform CameraPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (CameraPos == null)
        {
            CameraPos = GameObject.Find("CameraHolder").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CamPos = CameraPos.position;
        if (CameraPos != null)
        {
            CamPos.y = transform.position.y;
            transform.LookAt(CamPos);
            //print($" Player rotation {transform.eulerAngles}, wanted rotation {CamPos}");
        }
    }
}
