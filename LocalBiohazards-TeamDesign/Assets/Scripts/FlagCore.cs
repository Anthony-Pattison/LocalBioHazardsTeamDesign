using System.Collections.Generic;
using UnityEngine;

public class FlagCore : MonoBehaviour
{

    //a dictionary that holds all of the bool flags. there might be more flags of different types later on if needed
    public Dictionary<string, bool> boolFlags = new Dictionary<string, bool>()
    {
        {"test1", true},
        {"test2", false}
    };
    
}
