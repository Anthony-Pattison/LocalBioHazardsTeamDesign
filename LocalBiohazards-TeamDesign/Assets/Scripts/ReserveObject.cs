using UnityEngine;
using UnityEngine.AI;

public class ReserveObject : MonoBehaviour
{
    EventCore eventCore;
    NavMeshAgent agent;
    
    public GameObject reservedObj = null;

    bool queuedInteraction;

    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        eventCore.reserveObjectEV.AddListener(reserveObject);
        eventCore.unreserveObjectEV.AddListener(unreserveObject);
    }

    // Update is called once per frame
    void Update()
    {        
        if (queuedInteraction)
            executeObjectProcessing();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            queuedInteraction = true;
    }

    void reserveObject(GameObject selectedObj)
    {
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
