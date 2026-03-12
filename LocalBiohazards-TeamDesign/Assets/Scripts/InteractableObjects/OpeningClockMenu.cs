using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpeningClockMenu : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject menu;
    public float OpenAmount = 100;
    Vector3 NormalSize;
    Vector3 BigSize;
    public bool Toggle = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NormalSize = transform.localScale;
        BigSize = transform.localScale + Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckMouseOverUIWithThis())
        {
            playerController.enabled = false;

            transform.localScale = BigSize;
            if (Input.GetMouseButtonDown(0))
            {
                MenuOpen();
            }
            return;
        }
        transform.localScale = NormalSize;

    }
    void MenuOpen()
    {
        Toggle = !Toggle;
        OpenAmount = OpenAmount * -1;
        menu.transform.position -= new Vector3(OpenAmount, 0, 0);
        transform.position -= new Vector3(OpenAmount, 0, 0);
        playerController.enabled = Toggle;
    }
    bool CheckMouseOverUIWithThis()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastHit);

        for (int i = 0; i < raycastHit.Count; i++)
        {
            if (raycastHit[i].gameObject == this.gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
