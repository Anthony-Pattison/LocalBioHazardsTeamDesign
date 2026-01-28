using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("For looking at the Player")]
    public Transform PlayerTransform;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerTransform.position);
    }
}
