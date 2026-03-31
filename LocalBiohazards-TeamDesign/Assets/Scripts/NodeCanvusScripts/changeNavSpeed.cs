using JetBrains.Annotations;
using NodeCanvas.StateMachines;
using System;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
[Serializable]
public class audioVictim
{
    public AudioClip deathSound;
}
[Serializable]
public struct killedNPC
{
    public string npcKilled;
    public Vector3 position;

}
public class changeNavSpeed : MonoBehaviour
{
    public SpriteRenderer victimSprite;
    [Header("For the view meter")]
    public value seenMeter;
    [Tooltip("How much/fast the seen meter fills up")]
    public float seenValue = 0.1f;
    [Tooltip("The rate of how quickly the seen meter fills up based on distance. Higher values mean quicker speed.")]
    public float seenValueMultiplierRt = 3f;
    [Tooltip("A calculation that increases how fast the meter fills up based on how close the player is")]
    float seenValueMultiplier;
    public Collider areaOfVision;
    [Space(10.0f)]
    public audioVictim audioSound;
    
    public NavMeshAgent agent;
    public GameObject weponNeededToKill;
    public Animator animator;
    Transform cameraTransform;
    EventCore eventCore;
    AudioManager audioManager;
    
    [HideInInspector]
    public bool killed = false; 
    public bool enableGameover = true;
    
    public bool dead = false;
    public bool foundCorpseBehavior = false;
    public bool playerDetected = false;

    bool playerSound = false;

    
    private void Start()
    {
        if (areaOfVision == null)
        {
            areaOfVision = transform.Find("AreaOfVision").GetComponent<Collider>();
        }
        
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cameraTransform = GameObject.Find("CameraHolder").transform;
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        displayWepon(false);
    }

    private void Update()
    {
        Vector3 CamPos = cameraTransform.position;
        if (cameraTransform != null)
        {
            CamPos.y = transform.position.y;
            transform.LookAt(CamPos);
        }
        if (playerDetected && enableGameover)
        {
            eventCore.victimName = gameObject.name;
            seenMeter.valueNum += seenValue * seenValueMultiplier * Time.deltaTime;
        }
        //seenMeter.valueNum += seenValue * Time.deltaTime;

        killCharacter(killed);
    }
    public void displayWepon(bool state)
    {
        if (!dead) 
            weponNeededToKill.SetActive(state);
    }
    public void changeSpeed(float newSpeed)
    {
        agent.speed = newSpeed;
    }

    public void flipSprite()
    {
        Vector2 sceenpointOfCharacter = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 screenpointOfDestination = Camera.main.WorldToScreenPoint(agent.destination);

        if (screenpointOfDestination.x > sceenpointOfCharacter.x)
            victimSprite.flipX = true;
        if (screenpointOfDestination.x < sceenpointOfCharacter.x)
            victimSprite.flipX = false;

    }
    void killCharacter(bool die)
    {
        //respectfully why are there three dying variables bro
        if (die == false)
            return;
        if (!playerSound)
            audioManager.PlayOneShot(audioSound.deathSound);
        playerSound = true;
        animator.SetTrigger("dead");
        transform.Find("ConeCollision").gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<FSMOwner>().enabled = false;
        playerDetected = false;
        enableGameover = false;
        killed = false;
        displayWepon(false);
        dead = true;
        var killedNpcData = new killedNPC()
        {
            npcKilled = this.gameObject.name,
            position = transform.position
        };

        TelemetryLogger.Log(this, "NPC Killed", killedNpcData);
    }
  
    private void OnTriggerEnter(Collider other)
    {
        areaOfVision.enabled = false;
        GameObject collidedObj = other.gameObject;

        RaycastHit hitInfo;
        Physics.Linecast(transform.position, collidedObj.transform.position, out hitInfo);

        if (collidedObj.CompareTag("Player"))
        {
            print("found player");
            if (hitInfo.collider.gameObject.CompareTag("UndetectableCollision") || hitInfo.collider.gameObject.CompareTag("Player"))
            {
                playerDetected = true;
                seenValueMultiplier = seenValueMultiplierRt / Vector3.Distance(transform.position, other.gameObject.transform.position);
                print($"something is blocking player but is being ignored, leading to detection: {hitInfo.collider.gameObject}");
                print($"distance: {Vector3.Distance(transform.position, other.gameObject.transform.position)}");
                print($"decrease value: {seenValue * seenValueMultiplier * Time.deltaTime}");
                //print($"decrease value: {seenValue * Time.deltaTime}");

            }
            else
            {
                print($"something is blocking player: {hitInfo.collider.gameObject}");
                playerDetected = false;
            }

        }

        if (collidedObj.CompareTag("NPC") && (hitInfo.collider.gameObject.CompareTag("NPC") || hitInfo.collider.gameObject.CompareTag("UndetectableCollision")))
        {
            print("found npc");
            //the sprite, which holds the capsule collider, is a child
            //the component is in the parent
            changeNavSpeed npc = other.GetComponentInParent<changeNavSpeed>();

            if (npc.dead)
                foundCorpseBehavior = true;

        }

        areaOfVision.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("player has left vision");
            playerDetected = false;
        }
    }

    public bool GetFoundCorpseBehavior()
    {
        return foundCorpseBehavior;
    }
}
