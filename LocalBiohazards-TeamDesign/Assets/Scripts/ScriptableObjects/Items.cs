using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    public string ItemName;
    public Sprite ItemImage;
    public bool IsKeyItem;
    public bool IsTrapItem;
    [TextArea(1, 10)] // text area of a minimum of 1 line and a max of 10
    public string ItemDiscription;

}
