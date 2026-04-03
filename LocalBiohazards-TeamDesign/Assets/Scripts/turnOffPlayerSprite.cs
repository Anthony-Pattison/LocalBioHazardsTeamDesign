using UnityEngine;

public class turnOffPlayerSprite : MonoBehaviour
{
    SpriteRenderer player;
    PlayerController playerController;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void turnPlayerSpriteOn()
    {
        playerController.enabled = true;
        player.enabled = true;
    }
    public void turnPlayerSpriteOff()
    {
        playerController.enabled = false;
        player.enabled = false;
    }
}
