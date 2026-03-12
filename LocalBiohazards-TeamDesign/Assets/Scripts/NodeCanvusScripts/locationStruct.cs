using UnityEngine;
[System.Serializable]
public struct aiLocations
{
    public currentLocation location;
    public Transform locationTransform;
}

public enum currentLocation{
    stevensRoom,
    timmysRoom,
    kitchen,
    bathRoom,
    parentsRoom,
    livingRoom,
    DinningRoom,
    outside
}
