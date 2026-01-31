using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    EventCore eventCore;
    public List<Items> itemList = new List<Items>();
    public Items Knife;
    public GameObject TrapObject;
    Transform PlayerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.dropingTrapEV.AddListener(DropItem);
        eventCore.addToInventoryEV.AddListener(AddToInventory);
        AddToInventory(Knife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropItem()
    {
        foreach (Items _Item in itemList)
        {
            if (!_Item.IsTrapItem) {
                Debug.Log("Faild to find a trap item");
                continue;
            }
            Instantiate(TrapObject, PlayerTransform);
            itemList.Remove(_Item);
            eventCore.updateInventoryDisplayEV.Invoke();
        }
    }
    void AddToInventory(Items item)
    {
        
        itemList.Add(item);
        eventCore.updateInventoryDisplayEV.Invoke();
    }
}
