using TMPro;
using UnityEngine;

public class DiaplayPlayerNotes : MonoBehaviour
{
    public PlayerNotes PN;
    public TMP_InputField Input;
    private void OnEnable()
    {
        Input.GetComponent<TMP_InputField>(); Input.text = PN.PlayerWrittenNotes;  
    }

    private void OnDisable()
    {
        if (Input.text != PN.PlayerWrittenNotes)
            TelemetryLogger.Log(this, "Notes Updated", $"|Old Notes: {PN.PlayerWrittenNotes} | New Notes: {Input.text}");

        PN.PlayerWrittenNotes = Input.text;
    }
}
