using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class audio {
    public AudioClip itemChime;
    public AudioClip emptyItem;
}
public class ReserveObject : MonoBehaviour
{
    EventCore eventCore;
    NavMeshAgent agent;
    AudioManager audioManager;
    public GameObject reservedObj = null;
    public audio audioClips;
    bool queuedInteraction;

    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        audioManager = GameObject.Find("AudioManager").GetComponent <AudioManager>();
        eventCore.reserveObjectEV.AddListener(reserveObject);
        eventCore.unreserveObjectEV.AddListener(unreserveObject);
    }

    // Update is called once per frame
    void Update()
    {        
        //if (queuedInteraction)
        //    executeObjectProcessing();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            queuedInteraction = true;
        if (Input.GetMouseButtonDown(1) && reservedObj == null)
            audioManager.PlayOneShot(audioClips.emptyItem);
    }

    void reserveObject(GameObject selectedObj)
    {
        audioManager.PlayOneShot(audioClips.itemChime);
        reservedObj = selectedObj;
    }

    void unreserveObject()
    {
        reservedObj = null;
    }

    void executeObjectProcessing()
    {
        if (agent.velocity.magnitude > 0.2f)
            return;

        queuedInteraction = false;

        if (reservedObj == null)
            return;

        eventCore.processObjectEV.Invoke(reservedObj);
        reservedObj = null;
        
    }
}
