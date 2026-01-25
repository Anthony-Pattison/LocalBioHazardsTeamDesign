using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Object : MonoBehaviour
{
    //things that will happen when this object is interacted with and processed
    [Header("Actions\n------------------")]
    [Header("General")]
    public bool disableOnInteraction;
    public bool addToInventory;

    //activates a flag as an action
    [Header("Flag Activation")]
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
    
}
