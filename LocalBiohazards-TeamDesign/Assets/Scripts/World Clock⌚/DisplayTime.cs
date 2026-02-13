using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    public GameObject MinuteHand;
    public GameObject HourHand;
    EventCore eventcore;
    float MinuteHandRotation;
    float HourHandRotation;
    bool minuteAdjust = false;
    bool hourAdjust = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventcore = GameObject.Find("EventCore").GetComponent<EventCore>();
        HourHandRotation = 360 / 12;
        MinuteHandRotation = 360 / 60;
        eventcore.TurnOfTheMinute.AddListener(MinuteHandChage);
        eventcore.TurnOfTheHour.AddListener(HourHandChage);
    }

    void MinuteHandChage(float minuteTime)
    {
        if (!minuteAdjust)
        {
            print(minuteTime);

            MinuteHandRotation = MinuteHandRotation * minuteTime;
            minuteAdjust = true;
        }
        MinuteHand.transform.Rotate(0,0,-MinuteHandRotation);
    }
    void HourHandChage(float hourTime)
    {
        if (!hourAdjust)
        {
            HourHand.transform.Rotate(0, 0, -HourHandRotation * (hourTime - 1));
            hourAdjust = true;
        }
        HourHand.transform.Rotate(0, 0, -HourHandRotation);
    }
}
