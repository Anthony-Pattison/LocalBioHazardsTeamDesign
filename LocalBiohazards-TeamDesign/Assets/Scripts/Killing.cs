using Unity.VisualScripting;
using UnityEngine;

public class Killing : MonoBehaviour
{

    private GameObject Trapped = null;
    private GameObject Enemy = null;

    public bool isTrapped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrapped == true && Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Killed Enemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyTrapped")
        {
            
            Trapped = other.gameObject;
            isTrapped = Trapped.GetComponent<Trap>().IsTrapped;

        }

        if(other.gameObject.tag == "Enemy")
        {
            Enemy = other.gameObject;
        }
    }


}
