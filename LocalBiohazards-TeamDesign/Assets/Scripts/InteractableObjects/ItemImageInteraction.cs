using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
[RequireComponent(typeof(Animator))]
public class ItemImageInteraction : MonoBehaviour
{
    Vector3 NormalSize;
    Vector3 BigSize;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NormalSize = transform.localScale;
        BigSize = transform.localScale + Vector3.one;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        MoseOver(mousePos);
    }
    void MoseOver(Vector3 mousepos)
    {
        if (CheckMouseOverUIWithThis())
        {
            animator.SetBool("Shake", true);
            transform.localScale = BigSize;
            return;
        }
        animator.SetBool("Shake", false);
        transform.localScale = NormalSize;
    }
    /// <summary>
    /// Returns true of the  mouse is over the game objects this is on
    /// </summary>
    /// <returns></returns>
    bool CheckMouseOverUIWithThis()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastHit);

        for (int i = 0; i < raycastHit.Count; i++)
        {
            print(raycastHit[i].gameObject.name);
            if (raycastHit[i].gameObject == this.gameObject)
            {
                return true;
            }
        }
        return false;
    }
    bool IsmouseOver()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
