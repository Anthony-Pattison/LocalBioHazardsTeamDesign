using UnityEngine;

[CreateAssetMenu(fileName = "value", menuName = "Scriptable Objects/value")]
public class value : ScriptableObject
{

    public float valueNum;
    public bool resetNumOnEnable;
    private void OnEnable()
    {
        if (resetNumOnEnable)
            resetValue();
    }
    public void resetValue()
    {
        valueNum = 0;

    }
}
