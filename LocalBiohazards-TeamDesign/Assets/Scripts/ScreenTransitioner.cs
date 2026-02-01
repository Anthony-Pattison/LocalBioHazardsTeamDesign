using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransitioner : MonoBehaviour
{
    EventCore eventCore;

    [Header("References")]
    public RawImage blackImage;
    public Renderer dissolveNoiseGameobject;

    [Header("Values")]
    [Tooltip("Speed of transition. Measured in percentage (100 = 1x = 1 second)")]
    public float transitionSpeed = 100;
    [Tooltip("Delay the amount of time it takes to swap from fading in to fading out.")]
    public float transitionSwapDelay = 0.5f;
    [Tooltip("Wait this amount of seconds after the transition is finished before input is unfrozen.")]
    public float transitionFinishDelay = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.startScreenTransitionEV.AddListener(DetermineTransition);
    }

    void DetermineTransition(string transitionType)
    {
        if (transitionType == "fadeToBlack")
        {
            StartCoroutine(FadeToBlackTransition());
        }
        else if (transitionType == "dissolvingNoise") 
        {
            print("doing this dumbshit");
            StartCoroutine(DissolvingNoiseTransition());
        }
        else
        {
            Debug.LogError($"{transitionType} is not a valid transition.");
        }
    }

    IEnumerator FadeToBlackTransition()
    {
        //set up black image
        Color newColor = blackImage.color;
        newColor.a = 0;
        blackImage.color = newColor;
        
        //fade in
        while (newColor.a < 1)
        {
            newColor.a += (transitionSpeed / 100) * Time.deltaTime;
            blackImage.color = newColor;
            print(newColor.a);
            yield return new WaitForEndOfFrame();
        }

        //finish fade in
        newColor.a = 1;
        blackImage.color = newColor;

        eventCore.transportPlayerEV.Invoke();

        yield return new WaitForSeconds(transitionSwapDelay);

        //fade out
        while (newColor.a > 0)
        {
            newColor.a -= (transitionSpeed / 100) * Time.deltaTime;
            blackImage.color = newColor;
            yield return new WaitForEndOfFrame();
        }

        //finish fade out
        newColor.a = 0;
        blackImage.color = newColor;

        yield return new WaitForSeconds(transitionFinishDelay);
        eventCore.finishTransitionEV.Invoke();
    }

    IEnumerator DissolvingNoiseTransition()
    {
        float materialTransparency = 1.1f;

        //fade in
        while (materialTransparency > -0.1f)
        {
            materialTransparency -= (transitionSpeed / 100) * Time.deltaTime;
            dissolveNoiseGameobject.material.SetFloat("_DissolveStrength", materialTransparency);
            yield return new WaitForEndOfFrame();
        }

        //finish fade in
        materialTransparency = -0.1f;
        dissolveNoiseGameobject.material.SetFloat("_DissolveStrength", materialTransparency);

        eventCore.transportPlayerEV.Invoke();

        yield return new WaitForSeconds(transitionSwapDelay);

        //fade out
        while (materialTransparency < 1.1)
        {
            materialTransparency += (transitionSpeed / 100) * Time.deltaTime;
            dissolveNoiseGameobject.material.SetFloat("_DissolveStrength", materialTransparency);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        //finish fade out
        materialTransparency = 1.1f;
        dissolveNoiseGameobject.material.SetFloat("_DissolveStrength", materialTransparency);

        yield return new WaitForSeconds(transitionFinishDelay);
        eventCore.finishTransitionEV.Invoke();
    }
}
