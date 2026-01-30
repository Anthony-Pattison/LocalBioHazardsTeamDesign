using TMPro;
using UnityEngine;

public class DisplayItems : MonoBehaviour
{
    public Items Knife;
    public TextMeshProUGUI ItemTextName;
    public TextMeshProUGUI ItemTextDescription;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemTextName = gameObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        ItemTextDescription = gameObject.transform.Find("ItemDiscription").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        ItemTextName.text = Knife.ItemName;
        ItemTextDescription.text = Knife.ItemDiscription;
    }
}
