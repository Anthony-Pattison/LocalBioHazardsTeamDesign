using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMenu : MonoBehaviour
{

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            HideControls();
        }
    }

    public void ShowControls()
    {
        gameObject.SetActive(true);
    }

    public void HideControls()
    {
        gameObject.SetActive(false);
    }
}
