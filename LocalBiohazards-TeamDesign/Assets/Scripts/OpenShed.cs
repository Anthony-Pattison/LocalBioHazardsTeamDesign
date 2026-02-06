using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class OpenShed : MonoBehaviour
{
    public GameObject Ropeitem;
    public GameObject Shed;
    public Transform RightDoorPivot;
    public Transform LeftDoorPivot;
    Vector3 MousePosition;
    Vector3 LeftClosed;
    Vector3 RightClosed;
    public LayerMask clickableLayers;
    public bool Open;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RightClosed = RightDoorPivot.transform.eulerAngles;
        LeftClosed = LeftDoorPivot.transform.eulerAngles;
    }

    private void OnTriggerStay(Collider other)
    {
        ClickToOpenDoor();
    }
    private void OnTriggerExit(Collider other)
    {
        openDoors();
        Open = false;
    }
    void ClickToOpenDoor()
    {
        MousePosition = Input.mousePosition;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers) && !Open)
        {
            if (Input.GetMouseButtonDown(1) && hit.collider.gameObject == Shed)
            {
                openDoors();
                Open = true;
            }
        }
    }
    void openDoors()
    {

        if (Open) {
            Ropeitem.SetActive(false);
            RightDoorPivot.transform.eulerAngles = RightClosed;
            LeftDoorPivot.transform.eulerAngles = LeftClosed;
            return;
        }
        Ropeitem.SetActive(true);
        RightDoorPivot.transform.eulerAngles = new Vector3 (0f, -250f, 0f)  * Mathf.Deg2Rad;
        LeftDoorPivot.transform.eulerAngles = new Vector3(0f, -250f, 0f)  * Mathf.Deg2Rad;
    }
}
