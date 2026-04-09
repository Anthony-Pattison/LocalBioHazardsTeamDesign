using JetBrains.Annotations;
using UnityEditor.Rendering;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconDisplay : MonoBehaviour
{
    EventCore eventcore; 
    public Image BackgroundImage;
    public Inventory inventory;
    public GameObject ItemIcon;

    public GameObject Item;

    public Transform PlayerPos;
    Vector3 spawnLocation;

    GameObject InventoryManage;
        
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
        InventoryManage = GameObject.FindGameObjectWithTag("InventoryManager");
    }
    private void Update()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        spawnLocation = new Vector3(PlayerPos.position.x, 0.5f, PlayerPos.position.z);
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
            if (PickedUpItem == ItemIcon.GetComponent<ItemImageInteraction>().Item )
            {
                BackgroundImage.enabled = true;
                ItemIcon.SetActive(true);

                InventoryManage.GetComponent<InventoryManager>().itemSize += 1;
                return;

            }
            else if(InventoryManage.GetComponent<InventoryManager>().itemSize == 3)
            {
                Debug.Log("cant put anymore items!");
         
            }

        }
    }

    public void removeItem()
    {
        Debug.Log("Remove Item");
        InventoryManage.GetComponent<InventoryManager>().itemSize -= 1;
        BackgroundImage.enabled = false;
        ItemIcon.SetActive(false);

        Instantiate(Item, spawnLocation, Quaternion.identity);
        return;
        
    }
}
