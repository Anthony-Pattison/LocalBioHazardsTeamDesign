using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("For looking at the Player")]
    public Transform PlayerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerTransform.position);    
    }
}
