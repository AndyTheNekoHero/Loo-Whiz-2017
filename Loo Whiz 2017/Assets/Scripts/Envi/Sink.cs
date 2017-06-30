using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour {

    private bool Occupied = false;
    private bool WaterPuddle = false;

    public bool InUse()
    {
        return Occupied;
    }
    public void UnOccupy()
    {
        Occupied = false;
    }
    public void Occupy()
    {
        Occupied = true;
    }

    public bool IsWaterPuddle()
    {
        return WaterPuddle;
    }
    public void CreatedWaterPuddle()
    {
        WaterPuddle = true;
    }
    public void WaterPuddleCleaned()
    {
        WaterPuddle = false;
    }
}
