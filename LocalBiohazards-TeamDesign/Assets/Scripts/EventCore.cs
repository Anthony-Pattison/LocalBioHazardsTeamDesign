using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    [HideInInspector]
    //event for processing an object player interacts with
    public UnityEvent<GameObject> processObjectEV;

    [HideInInspector]
    //an outcome to processing an object: event for adding an item to an object
    public UnityEvent<string> addToInventoryEV;

    [HideInInspector]
    //follows the addToInventory EV; event for updating the inventory display since the item list has changed
    public UnityEvent updateInventoryDisplayEV;

    //for testing purposes in order to invoke the event through pressing a button
    public void InvokeProcessObjectEV(GameObject gameObj)
    {
        processObjectEV.Invoke(gameObj);
    }
}
