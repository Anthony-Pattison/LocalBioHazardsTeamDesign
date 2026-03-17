using System.Linq;
using UnityEngine;

public class killingVictims : MonoBehaviour
{
    public Transform timmyTransform;
    public Transform stevensTransform;
    public float tiggerDistance;
    changeNavSpeed timmysClass;
    changeNavSpeed stevensClass;
    Inventory currentInventory;
    public Items bat;
    public Items rope;

    bool canKillTimmy;
    bool canKillSteven;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timmysClass = timmyTransform.GetComponent<changeNavSpeed>();
        stevensClass = stevensTransform.GetComponent<changeNavSpeed>();
        currentInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfKill();
        mouseInput();
    }

    void mouseInput()
    {
        if (canKillSteven)
        {
            if (Input.GetMouseButtonDown(1))
            {
                stevensClass.killed = true;
            }
        }
        if (canKillTimmy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                timmysClass.killed = true;
            }
        }
    }
    void checkIfKill()
    {
        float distanceToTimmy = Vector3.Distance(transform.position, timmyTransform.position);
        float distanceToSteven = Vector3.Distance(transform.position, stevensTransform.position);

        if (distanceToSteven < tiggerDistance && currentInventory.itemList.Contains(bat))
        {
            canKillSteven = true;
            stevensClass.displayWepon(true);
        }
        else
        {
            canKillSteven = false;
            stevensClass.displayWepon(false);
        }
        if (distanceToTimmy < tiggerDistance && currentInventory.itemList.Contains(rope))
        {
            canKillTimmy = true;
            timmysClass.displayWepon(true);
        }
        else
        {
            canKillTimmy = false;
            timmysClass.displayWepon(false);
        }
    }
}
