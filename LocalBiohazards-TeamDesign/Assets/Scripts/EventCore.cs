using UnityEngine;
using UnityEngine.Events;

public class EventCore : MonoBehaviour
{
    [HideInInspector]
    //event for processing an object player interacts with
    public UnityEvent<GameObject> processObjectEV;

    [HideInInspector]
    //event that reserves an object for object processing. happens when the player enters the collision area of an object
    public UnityEvent<GameObject> reserveObjectEV;

    [HideInInspector]
    //event that unreserves an object for object processing. happens when a player exits the collision area of an object
    public UnityEvent unreserveObjectEV;

    [HideInInspector]
    //an outcome to processing an object: event for adding an item to an object
    public UnityEvent<Items> addToInventoryEV;

    [HideInInspector]
    //follows the addToInventory EV; event for updating the inventory display since the item list has changed
    public UnityEvent updateInventoryDisplayEV;

    [HideInInspector]
    //event for doing full screen transitions, such as a fade to black. typically freezes input and ai
    public UnityEvent<string> startScreenTransitionEV;

    [HideInInspector]
    public UnityEvent transportPlayerEV;

    [HideInInspector]
    //event for finishing a transition, which unfreezes input and ai
    public UnityEvent finishTransitionEV;

    [HideInInspector]
    //event for the player droping a trap on the ground
    public UnityEvent dropingTrapEV;

    //for testing purposes in order to invoke the event through pressing a button
    public void InvokeProcessObjectEV(GameObject gameObj)
    {
        processObjectEV.Invoke(gameObj);
    }
}
