using UnityEngine;
public enum TimeCheck
{
    CheckHour,
    CheckMinute
}
public class WorldClock : MonoBehaviour
{
    EventCore eventCore;

    [Header("The in game time and change amounts")]
    [Tooltip("In game time in minutes")]
    [Range(0f, 60f)]
    [SerializeField] int WorldTimeMinutes;

    [Tooltip("In game time in hours")]
    [Range(0f, 24f)]
    [SerializeField] int WorldTimeHours;

    [Tooltip("The change time to world clock - in minutes")]
    [Range(0f, 60f)]
    [SerializeField] int MinuteIncrementAmount;

    [Tooltip("Real Seconds for the TimeIncrementAmount to be add to the world clock - in seconds")]
    [SerializeField] float TimeTillIncrementInSeconds;

    [Space(10)]
    [Header("When the time invoke gets called")]
    public TimeCheck timeCheck;

    float TimeTillIncrement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        if(eventCore == null)
        {
            Debug.Log($"{this.gameObject.name} Could not find event core, destroying {this.name}");
            Destroy(this);
        }
        ChangeTime(1, MinuteIncrementAmount, TimeCheck.CheckMinute);
        ChangeTime(0, MinuteIncrementAmount, TimeCheck.CheckHour);
    }

    // Update is called once per frame
    void Update()
    {
        TimeTillIncrement += Time.deltaTime;
        if (TimeTillIncrement >= TimeTillIncrementInSeconds)
        {
            TimeTillIncrement = 0;
            ChangeTime(1, MinuteIncrementAmount, timeCheck);
        }
    }

    void ChangeTime(int HourChange, int MinuteChange, TimeCheck TimeState)
    {
        if (TimeState == TimeCheck.CheckHour)
        {
            WorldTimeHours += HourChange;

            if (WorldTimeHours >= 23)
            {
                WorldTimeHours = 0;
            }
            print("Its been the set amount of hours");
            eventCore.TurnOfTheHour.Invoke(WorldTimeHours);
            return;

        }

        if (TimeState == TimeCheck.CheckMinute)
        {
            WorldTimeMinutes += MinuteChange;

            if (WorldTimeMinutes >= 59)
            {
                WorldTimeMinutes = 0;
                WorldTimeHours++;
                eventCore.TurnOfTheHour.Invoke(WorldTimeHours);
            }else if (WorldTimeHours >= 23)
            {
                WorldTimeHours = 0;
            }
                print("Its been the set amount of minutes");
            eventCore.TurnOfTheMinute.Invoke(WorldTimeMinutes);
        }
    }
}
