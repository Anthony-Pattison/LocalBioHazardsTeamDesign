using UnityEngine;

public class turnOffPlayerSprite : MonoBehaviour
{
    SpriteRenderer player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    public void turnPlayerSpriteOn()
    {
        player.enabled = true;
    }
    public void turnPlayerSpriteOff()
    {
        player.enabled = false;
    }
}
