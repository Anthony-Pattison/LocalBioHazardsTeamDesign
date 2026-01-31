using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    EventCore eventCore;
    CustomAction input;
    NavMeshAgent agent;

    public LayerMask clickableLayers;
    public Transform CameraPos;
    private void Awake()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        agent = GetComponent<NavMeshAgent>();
        input = new CustomAction();
        assignInputs();

    }
    private void Update()
    {
        if(CameraPos != null)
            transform.LookAt(CameraPos.position);
        GetPlayerInput();
    }
    void GetPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            eventCore.dropingTrapEV.Invoke();
        }
    }
    void assignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();

    }

    void ClickToMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
        {
            agent.destination = hit.point;

        }
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }
}
