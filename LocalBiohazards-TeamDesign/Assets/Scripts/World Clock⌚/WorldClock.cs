using System;
using UnityEngine;

[Serializable]
public class AudioClock
{
    public AudioClip hourSFX;
    public AudioClip tickTockSFX;
    public AudioClip endBellSFX;
}
public enum TimeCheck
{
    CheckHour,
    CheckMinute
}


public class WorldClock : MonoBehaviour
{
    EventCore eventCore;
    AudioManager audioManager;
    public AudioClock clockSFX;

    [Header("The in game time and change amounts")]
    [Tooltip("In game time in minutes")]
    [Range(0f, 60f)]
    [SerializeField] int WorldTimeMinutes;

    [Tooltip("In game time in hours")]
    [Range(0f, 23f)]
    [SerializeField] int WorldTimeHours;

    [Tooltip("The change time to world clock - in minutes")]
    [Range(0f, 60f)]
    [SerializeField] int MinuteIncrementAmount;

    [Tooltip("Real Seconds for the TimeIncrementAmount to be add to the world clock - in seconds")]
    [SerializeField] float TimeTillIncrementInSeconds;

    [Tooltip("The hour that dictates a game over. Should be 24 (12am).")]
    [SerializeField] float gameOverHour;

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
            Debug.LogWarning($"{this.gameObject.name} Could not find event core, destroying {this.name}");
            Destroy(this);
        }
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

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
                audioManager.PlayOneShot(clockSFX.hourSFX);

                if (WorldTimeHours == gameOverHour - 1)
                    audioManager.PlayOneShot(clockSFX.tickTockSFX);
                else if (WorldTimeHours == gameOverHour)
                {
                    audioManager.PlayOneShot(clockSFX.endBellSFX);
                    eventCore.resetGameState.Invoke();
                }
                    

                eventCore.TurnOfTheHour.Invoke(WorldTimeHours);
                
            }else if (WorldTimeHours >= 23)
            {
                WorldTimeHours = 0;
            }
            eventCore.TurnOfTheMinute.Invoke(WorldTimeMinutes);
        }
    }
}
