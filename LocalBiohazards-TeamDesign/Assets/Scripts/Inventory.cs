using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    EventCore eventCore;
    public List<string> itemList = new List<string>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();

        eventCore.addToInventoryEV.AddListener(AddToInventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddToInventory(string item)
    {
        
        itemList.Add(item);
        eventCore.updateInventoryDisplayEV.Invoke();
    }
}
