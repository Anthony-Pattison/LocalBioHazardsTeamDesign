using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryDisplay : MonoBehaviour
{
    EventCore eventCore;
    Inventory inventory;
    //displays items in inventory through text
    //obviously temporary, just gotta show it works rn
    public TextMeshProUGUI inventoryItemsText;
    public GameObject ItemImage;

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
        ItemImage.GetComponent<Image>().sprite = null;
        inventoryItemsText.text = "";
        foreach (Items item in inventory.itemList)
        {
            inventoryItemsText.text += $"{item.ItemName},\n";
            if (!item.IsTrapItem)
            {
                continue;
            }
            ItemImage.GetComponent<Image>().sprite = item.ItemImage;
        }
    }

}
