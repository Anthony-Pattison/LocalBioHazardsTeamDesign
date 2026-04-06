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
    public Items chainsaw;
    public Items rope;
    public Items acidBottle;

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
                determineKillWeapon(stevensClass);
            }
        }
        if (canKillTimmy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                timmysClass.killed = true;
                determineKillWeapon(timmysClass);
            }
        }
    }
    void checkIfKill()
    {
        float distanceToTimmy = Vector3.Distance(transform.position, timmyTransform.position);
        float distanceToSteven = Vector3.Distance(transform.position, stevensTransform.position);

        if (distanceToSteven < tiggerDistance && (currentInventory.itemList.Contains(bat) || currentInventory.itemList.Contains(chainsaw) || currentInventory.itemList.Contains(acidBottle)))
        {
            canKillSteven = true;
            determineKillWeaponPrompt(stevensClass);
            stevensClass.displayWepon(true);
        }
        else
        {
            canKillSteven = false;
            stevensClass.displayWepon(false);
        }
        if (distanceToTimmy < tiggerDistance && (currentInventory.itemList.Contains(rope) || currentInventory.itemList.Contains(chainsaw)))
        {
            canKillTimmy = true;
            determineKillWeaponPrompt(timmysClass);
            timmysClass.displayWepon(true);
        }
        else
        {
            canKillTimmy = false;
            timmysClass.displayWepon(false);
        }
    }

    void determineKillWeaponPrompt(changeNavSpeed npc)
    {
        if (npc.gameObject.name == "Steven")
        {
            if (currentInventory.itemList.Contains(bat))
                npc.killWeaponImage.sprite = bat.ItemImage;
            else if (currentInventory.itemList.Contains(chainsaw))
                npc.killWeaponImage.sprite = chainsaw.ItemImage;
            else if (currentInventory.itemList.Contains(acidBottle))
                npc.killWeaponImage.sprite = acidBottle.ItemImage;
        }
        else if (npc.gameObject.name == "Timmy")
        {
            if (currentInventory.itemList.Contains(rope))
                npc.killWeaponImage.sprite = rope.ItemImage;
            else if (currentInventory.itemList.Contains(chainsaw))
                npc.killWeaponImage.sprite = chainsaw.ItemImage;
        }
    }

    void determineKillWeapon(changeNavSpeed npc)
    {
        if (npc.gameObject.name == "Steven")
        {
            if (currentInventory.itemList.Contains(bat))
                npc.killCharacter(true, bat);
            else if (currentInventory.itemList.Contains(chainsaw))
                npc.killCharacter(true, chainsaw);
            else if (currentInventory.itemList.Contains(acidBottle))
                npc.killCharacter(true, acidBottle);
        }
        else if (npc.gameObject.name == "Timmy")
        {
            if (currentInventory.itemList.Contains(rope))
                npc.killCharacter(true, rope);
            else if (currentInventory.itemList.Contains(chainsaw))
                npc.killCharacter(true, chainsaw);
        }

        
    }
}
