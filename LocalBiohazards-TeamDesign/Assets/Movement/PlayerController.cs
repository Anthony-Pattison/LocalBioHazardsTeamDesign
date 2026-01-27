using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    CustomAction input;
    NavMeshAgent agent;

    public LayerMask clickableLayers;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        input = new CustomAction();
        assignInputs();

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
