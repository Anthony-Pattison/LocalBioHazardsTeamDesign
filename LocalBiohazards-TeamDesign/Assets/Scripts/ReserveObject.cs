using UnityEngine;
using UnityEngine.AI;

public class ReserveObject : MonoBehaviour
{
    EventCore eventCore;
    NavMeshAgent agent;
    
    public GameObject reservedObj = null;

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
        executeObjectProcessing();
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
        if (!agent.isStopped)
            return;

        if (reservedObj == null)
            return;

        eventCore.processObjectEV.Invoke(reservedObj);
    }
}
