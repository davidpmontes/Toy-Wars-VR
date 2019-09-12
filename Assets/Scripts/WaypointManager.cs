using UnityEngine;

public enum WAYPOINT_LOCATIONS
{
    SNES, LincolnLogs, TrashCan, Hallway, HallwayDoor
}

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
}
