using NodeCanvas.StateMachines;
using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class audioVictim
{
    public AudioClip deathSound;
}

public class changeNavSpeed : MonoBehaviour
{
    [Header("For the view meter")]
    public value seenMeter;
    [Tooltip("How much/fast the seen meter fills up")]
    public float seenValue = 0.1f;
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
        if(playerDetected)
            seenMeter.valueNum += seenValue * Time.deltaTime;

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
        killed = false;
        displayWepon(false);
        dead = true;
        TelemetryLogger.Log(this, "NPC Killed", $"NPC Name: {name}, Location: {transform.position}");
    }
    private void OnTriggerEnter(Collider other)
    {
        RaycastHit hitInfo;
        Physics.Linecast(transform.position, other.gameObject.transform.position, out hitInfo);

        if (other.gameObject.CompareTag("Player"))
        {
            print("found player");
            if (hitInfo.collider.gameObject.CompareTag("UndetectableCollision") || hitInfo.collider.gameObject.CompareTag("Player"))
            {
                if (enableGameover)
                {
                    print($"something is blocking player but is being ignored, leading to game over: {hitInfo.collider.gameObject}");
                    TelemetryLogger.Log(this, "Failure By NPC", $"NPC Name: {name}, Location: {transform.position}");
                    playerDetected = true;

                    //eventCore.resetGameState.Invoke();
                }
                else
                {
                    print($"something is blocking player but is being ignored, no game over though: {hitInfo.collider.gameObject}");
                }

            }
            else
            {
                print($"something is blocking player: {hitInfo.collider.gameObject}");
                playerDetected = false;
            }

        }
        else playerDetected = false;

        if (other.gameObject.CompareTag("NPC"))
        {
            print("found npc");
            //the sprite, which holds the capsule collider, is a child
            //the component is in the parent
            changeNavSpeed npc = other.GetComponentInParent<changeNavSpeed>();

            if (npc.dead)
                foundCorpseBehavior = true;

        }
    }

    public bool GetFoundCorpseBehavior()
    {
        return foundCorpseBehavior;
    }
}
