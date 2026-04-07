using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class seenMeter : MonoBehaviour
{
    EventCore eventCore;
    [Range(0f, 1f)]
    public float fillAmount = 0f;
    public Image knifeFill;
    public value seenValue;
    public float secondsToCoolDown = 1;
    public float decreaseAmount = 0.01f;
    public float coolDownWait = .5f;
    float timer;

    bool coroutineRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        seenValue.resetValue();
    }

    // Update is called once per frame
    void Update()
    {
        seenValue.valueNum = Mathf.Clamp(seenValue.valueNum, 0f, 1.0f);
        knifeFill.fillAmount = seenValue.valueNum;

        if (knifeFill.fillAmount >= 1)
        {
            eventCore.resetGameState.Invoke();
            //TelemetryLogger.Log(this, "Failure By NPC", $"NPC Name: {gameObject.name}, Location: {gameObject.transform.position}");
            seenValue.resetValue();
        }

        if (seenValue.valueNum > 0)
        {
            timer += Time.deltaTime;
        }

        if (timer >= secondsToCoolDown && !coroutineRunning)
            StartCoroutine(lowerValue());
    }

    IEnumerator lowerValue()
    {
        coroutineRunning = true;
        while (seenValue.valueNum > 0)
        {
            seenValue.valueNum -= decreaseAmount;
            yield return new WaitForSeconds(coolDownWait);
        }
        timer = 0;
        coroutineRunning = false;
    }
}
