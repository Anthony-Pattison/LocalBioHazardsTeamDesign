using UnityEngine;

public class poisonCup : MonoBehaviour
{
    public bool isPoisoned = false;

    public GameObject player;
    public Items cyanide;
    Inventory currentInventory;
    public float activationDistance = 3;
    public SpriteRenderer waterSr;
    public Sprite poisonedSprite;

    private void Start()
    {
        waterSr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        currentInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < activationDistance && Input.GetMouseButton(1) && !isPoisoned && currentInventory.itemList.Contains(cyanide))
        {
            PoisonCup();
        }
    }

    void PoisonCup()
    {
        isPoisoned = true;
        waterSr.sprite = poisonedSprite;
    }
}
