using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("For looking at the Player")]
    public Transform PlayerTransform;

    [Space(10)]
    [Header("Variables assigned at run time")]
    // For setting the position of the camera
    public Transform TimmysRoomCam;
    public Transform StevensRoomCam;
    public Transform ParentsRoomCam;
    public Transform DinningRoomCam;
    public Transform KitchenCam;
    public Transform HallCam;

    public Transform PreviousCamPos;

    public bool passed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimmysRoomCam = GameObject.Find("TImmy'sCamPos").transform;
        KitchenCam = GameObject.Find("Kitchen'sCamPos").transform;

        PreviousCamPos = KitchenCam;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerTransform.position);    
    }

    public void MoveToTimmysRoom()
    {
        if (!passed)
        {
            PreviousCamPos.position = transform.position;
            transform.position = TimmysRoomCam.position;
            passed = true;
            return;
        }
        transform.position = PreviousCamPos.position;
        passed = false;
    }
}
