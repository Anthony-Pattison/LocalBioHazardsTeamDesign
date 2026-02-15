using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{

    EventCore eventCore;

    public GameObject mainCamera;
    public float scrollRate = 1f;

    Transform cameraInitialTransform;
    Vector3 cameraInitialRotation;
    Vector3 layerInitialPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.updateParallaxScrollingEV.AddListener(ResetCameraAndPosition);
        
        if (mainCamera == null)
        {
            mainCamera = GameObject.Find("CameraHolder");
        }
        
        cameraInitialTransform = mainCamera.transform;
        cameraInitialRotation = cameraInitialTransform.eulerAngles;
        layerInitialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraDelta = mainCamera.transform.eulerAngles - cameraInitialRotation;
        print($"{mainCamera.transform.eulerAngles} - {cameraInitialRotation} = {cameraDelta}");

        float layerDeltaX = cameraDelta.y * scrollRate;
        float layerDeltaY = cameraDelta.x * scrollRate;

        layerDeltaY = 0;

        Vector3 newLayerPos = layerInitialPos + new Vector3(0, layerDeltaY, layerDeltaX);
        transform.position = Vector3.Lerp(transform.position, newLayerPos, scrollRate);
    }

    void ResetCameraAndPosition()
    {
        cameraInitialTransform = mainCamera.transform;
        cameraInitialRotation = cameraInitialTransform.eulerAngles;
        transform.position = layerInitialPos;
    }
}
