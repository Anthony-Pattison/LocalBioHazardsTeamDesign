using UnityEngine;
using UnityEngine.UI;

public class seenMeter : MonoBehaviour
{
    EventCore eventCore;
    [Range(0f, 1f)]
    public float fillAmount = 0f;
    public Image knifeFill;
    public value seenValue;
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
            seenValue.resetValue();
        }
    }
}
