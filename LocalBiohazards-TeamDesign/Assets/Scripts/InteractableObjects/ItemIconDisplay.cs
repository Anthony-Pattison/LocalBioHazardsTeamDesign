using UnityEngine;
using UnityEngine.UI;

public class ItemIconDisplay : MonoBehaviour
{
    EventCore eventcore; 
    Image BackgroundImage;
    public Inventory inventory;
    public GameObject ItemIcon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BackgroundImage = GetComponent<Image>();
        ItemIcon = transform.GetChild(0).gameObject;
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        BackgroundImage.enabled = false;
        ItemIcon.SetActive(false);
        eventcore.addToInventoryEV.AddListener(SetItemActive);
    }
    /// <summary>
    /// Sets the item display that this has children active and <div>
    /// Set the image component to from fasle</div>
    /// PickedUpItem does not get used
    /// </summary>
    /// <param name="PickedUpItem"></param>
    void SetItemActive(Items PickedUpItem)
    {
        foreach (Items inventory in inventory.itemList)
        {
            if(inventory == ItemIcon.GetComponent<ItemImageInteraction>().Item)
            {
                BackgroundImage.enabled = true;
                ItemIcon.SetActive(true);
            }
        }
    }
  
}
