using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    EventCore eventCore;
    public List<Items> itemList = new List<Items>();
    public Items Knife;
    public GameObject TrapObject;
    Transform PlayerTransform;

    public int itemCap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        PlayerTransform = GameObject.Find("Player").transform;
        eventCore.dropingTrapEV.AddListener(DropItem);
        eventCore.addToInventoryEV.AddListener(AddToInventory);
        AddToInventory(Knife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Drops one kind of trap on the ground when the player presses<br/>
    /// the Q key <br/>
    /// returns "Faild to find trap." if there are no traps in the inventory
    /// </summary>
    void DropItem()
    {
        // need to modify the list outside of the for loop beacuse c#
        // throws an error
        Items _TrapToUse = null;
        foreach (Items _Item in itemList)
        {
            if (!_Item.IsTrapItem) {
                continue;
            }
            _TrapToUse = _Item;
        }
        // removing from the list
        if (_TrapToUse != null)
        {
            Instantiate(TrapObject, PlayerTransform.position, PlayerTransform.rotation);
            itemList.Remove(_TrapToUse);
            eventCore.updateInventoryDisplayEV.Invoke();
            return;
        }
        Debug.Log("Faild to find a trap item.");
    }

    void AddToInventory(Items item)
    {
        
        itemList.Add(item);
        eventCore.updateInventoryDisplayEV.Invoke();
    }
}
