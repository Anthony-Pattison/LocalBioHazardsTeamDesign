using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Object : MonoBehaviour
{
    EventCore eventCore;
    
    //things that will happen when this object is interacted with and processed
    [Header("Actions\n------------------")]
    [Header("General")]
    [Tooltip("Disables the object when interacted with, making it uninteractable and invisible.")]
    public bool disableOnInteraction;
    [Tooltip("Destroys the object when interacted with. Should be used for placeable items (like traps).")]
    public bool destroyOnInteraction;
    public bool addToInventory;
    public Items item;

    [Header("------------------\nNPCs")]
    [Tooltip("Determines whether NPCs should be able to interact with this object when colliding with it. Should see use in traps.")]
    public bool interactableByNpc;

    //activates a flag as an action
    [Header("------------------\nFlag Activation")]
    public string flagName = null; //the name of the flag
    [Tooltip("activates or deactivates a flag based on this value. if flagName is empty, this won't do anything")]
    public bool activateFlag; //activates or deactivates a flag based on this value. if flagName is empty, this won't do anything

    //transports the player as an action
    [Header("Transportation")]
    [Tooltip("keep false if you don't want to teleport the player anywhere when interacting with this object")]
    public bool transportPlayer;
    public Vector3 transportPlayerCoords;

    [Space(15)]
    //conditions that need to be true in order for the object's actions to occur
    [Header("------------------\nConditions")]
    public List<string> inventoryCondition; //an object must be in inventory
    public List<string> flagCondition; //a flag from the flag core must be true

    private void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            print($"player enters: {gameObject.name}");
            eventCore.reserveObjectEV.Invoke(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            print("player exits");
            eventCore.unreserveObjectEV.Invoke();
        }
    }
}
