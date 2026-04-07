using UnityEngine;

public class ChangeKillerSprite : MonoBehaviour
{
    EventCore eventCore;

    changeNavSpeed stevenClass;
    Animator playerAnimator;
    public float pickUpDistance = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();

        if (stevenClass == null) 
            stevenClass = GetComponent<changeNavSpeed>();

        if (playerAnimator == null) 
            playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        print(Vector3.Distance(transform.position, playerAnimator.transform.position));
        if (Input.GetMouseButtonDown(1) && stevenClass.dead && Vector3.Distance(transform.position, playerAnimator.transform.position) < pickUpDistance)
        {
            ChangeSprite();
        }   
    }

    void ChangeSprite()
    {
        playerAnimator.SetBool("hasStevenFace", true);
        eventCore.changeKillerSpriteEV.Invoke();
    }
}
