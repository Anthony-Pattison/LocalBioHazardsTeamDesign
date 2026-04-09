using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public int itemSize;
    public GameObject message;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void hideMessage()
    {
        message.SetActive(false);
    }

    public void displayMessage()
    {
        message.SetActive(true);
        Invoke("hideMessage", 3f);
    }

}
