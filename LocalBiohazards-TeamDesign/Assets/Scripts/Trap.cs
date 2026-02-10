using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Trap : MonoBehaviour
{

    public bool IsTrapped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsTrapped = false;
        gameObject.tag = "OpenTrap";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Trapped");
            IsTrapped = true;
            gameObject.tag = "EnemyTrapped";
            //When the Ai system for npc movement is done this is where we'd put the link to stop movement. 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("NotTrapped");
            IsTrapped = false;
            gameObject.tag = "OpenTrap";

            //When the Ai system for npc movement is done this is where we'd put the link to stop movement. 
        }
    }
}
