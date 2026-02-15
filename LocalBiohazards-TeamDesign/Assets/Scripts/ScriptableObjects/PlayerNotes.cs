using UnityEngine;

[CreateAssetMenu(fileName = "PlayerNotes", menuName = "Scriptable Objects/PlayerNotes")]
public class PlayerNotes : ScriptableObject
{
    [TextArea(1, 10)] // text area of a minimum of 1 line and a max of 10
    public string PlayerWrittenNotes;
}
