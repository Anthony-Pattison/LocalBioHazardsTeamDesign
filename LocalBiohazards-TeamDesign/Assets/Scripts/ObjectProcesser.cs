using System.Collections.Generic;
using UnityEngine;

public class ObjectProcesser : MonoBehaviour
{
    EventCore eventCore;
    Inventory inventory;
    FlagCore flagCore;

    Object obj;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        flagCore = GameObject.Find("FlagCore").GetComponent<FlagCore>();

        eventCore.processObjectEV.AddListener(ProcessObject);
        eventCore.transportPlayerEV.AddListener(TeleportPlayer);
    }

    void ProcessObject(GameObject interactedObject)
    {
        print($"processing object: {interactedObject.name}");
        obj = interactedObject.GetComponent<Object>();

        bool passedCondition = ConditionCheck(obj);
        if (!passedCondition)
        {
            return;
        }

        //process the object's actions if all conditions are true
        if (obj.disableOnInteraction)
        {
            obj.gameObject.SetActive(false);
        }

        if (obj.addToInventory)
        {
            eventCore.addToInventoryEV.Invoke(interactedObject.name);
        }

        if (obj.transportPlayer)
        {
           //does the transition first
           //might add functionality for it to be optional later on and for multiple types
            eventCore.startScreenTransitionEV.Invoke("fadeToBlack");
        }
    }

    void TeleportPlayer()
    {
        //just moves the camera as the player rn
        //obviously this will change once we get the player and we'll just transport it
        GameObject player = GameObject.Find("Main Camera");
        player.transform.position = obj.transportPlayerCoords;
    }

    //checks whether all the conditions are fulfilled in the obj
    bool ConditionCheck(Object obj)
    {
        bool inventoryConditionPassed;
        bool flagConditionPassed;
        
        //inventory check
        if (obj.inventoryCondition.Count > 0)
        {
            inventoryConditionPassed = CheckingConditionInventory(obj.inventoryCondition);
        }
        else
        {
            inventoryConditionPassed = true;
        }
        
        //flag check
        if (obj.flagCondition.Count > 0)
        {
            flagConditionPassed = CheckingConditionFlags(obj.flagCondition);
        }
        else
        {
            flagConditionPassed = true;
        }

        //if either inventory or flag check fail, it will return false. otherwise return true
        //it's basically a "and" statement since both conditions need to be true for the object to process the actions
        //i might add functionality for an "or" statement later but i don't see that point rn
        if (!inventoryConditionPassed || !flagConditionPassed)
        {
            return false;
        }
        return true;
    }

    //for checking whether the inventory has certain items
    bool CheckingConditionInventory(List<string> list)
    {
        //create a new list that holds whether the conditions for each item in list is true
        List<bool> passedConditions = new List<bool>();
        for (int i = 0; i < list.Count; i++)
            passedConditions.Add(false);

        //check the inventory if it has an item specified by the conditions
        for (int i = 0; i < list.Count; i++)
        {
            string condition = list[i];

            foreach (string item in inventory.itemList)
            {
                //if the inventory has the item, update the passedConditions list to reflect this
                if (item.Equals(condition))
                {
                    passedConditions[i] = true;
                    break;
                }
            }
        }

        //now check if the conditions are fulfilled
        foreach (bool condition in passedConditions)
        {
            if (!condition)
                return false;
        }

        return true;
    }

    //for checking whether certain flags are true
    bool CheckingConditionFlags(List<string> list)
    {
        //create a new list that holds whether the conditions for each item in list is true
        List<bool> passedConditions = new List<bool>();
        for (int i = 0; i < list.Count; i++)
            passedConditions.Add(false);

        //check the flags if they are true
        for (int i = 0; i < list.Count; i++)
        {
            string condition = list[i];
            
            //if the flag doesn't exist in the flag core, output an error message and default the condition check to false
            if (!flagCore.boolFlags.ContainsKey(condition))
            {
                Debug.LogError($"The flag ({condition}) does not exist in the flagCore. FIX IT NOWWWWWW!!!11!1!");
                return false;
            }

            //if the flags are true, update the passedConditions list to reflect this
            bool flagActivated = flagCore.boolFlags[condition];
            if (flagActivated)
            {
                passedConditions[i] = true;
            }

        }

        //now check if the conditions are fulfilled
        foreach (bool condition in passedConditions)
        {
            if (!condition)
                return false;
        }

        return true;
    }
}
