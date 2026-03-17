using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class resetGameState : MonoBehaviour
{
    public KeyCode resetKey;
    public Image resetImage;
    EventCore eventCore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.resetGameState.AddListener(resetGame);
        StartCoroutine(fadeInAndOut(1, true));
    }

    private void Update()
    {
        if (Input.GetKeyDown(resetKey))
        {
            StartCoroutine(fadeInAndOut(0, false));
        }
    }
    void resetGame()
    {
        StartCoroutine(fadeInAndOut(0, false));
    }

    IEnumerator fadeInAndOut(float alpha, bool fadeIn)
    {
        Color color = resetImage.GetComponent<Image>().color;
        if (fadeIn)
        {
            color.a = 1;
            resetImage.GetComponent<Image>().color = color;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime;
                color.a = alpha;
                resetImage.GetComponent<Image>().color = color;
                yield return null;
            }
        }
        else
        {

            while (alpha < 1)
            {
                alpha += Time.deltaTime;
                color.a = alpha;
                resetImage.GetComponent<Image>().color = color;
                yield return null;
            }
            SceneManager.LoadScene(0);
        }
    }
}
