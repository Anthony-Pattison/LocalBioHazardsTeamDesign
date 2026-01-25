using TMPro;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    EventCore eventCore;
    Inventory inventory;
    //displays items in inventory through text
    //obviously temporary, just gotta show it works rn
    public TextMeshProUGUI inventoryItemsText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        eventCore.updateInventoryDisplayEV.AddListener(UpdateInventoryDisplay);

        UpdateInventoryDisplay();
    }
    void UpdateInventoryDisplay()
    {
        inventoryItemsText.text = "";

        foreach (string item in inventory.itemList)
        {
            inventoryItemsText.text += $"{item},\n";
        }
    }

}
