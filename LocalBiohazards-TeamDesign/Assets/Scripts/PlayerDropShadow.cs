using UnityEngine;

public class PlayerDropShadow : MonoBehaviour
{
    public Vector3 Offset;
    Transform PlayerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPos = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = PlayerPos.position + Offset;
    }
}
