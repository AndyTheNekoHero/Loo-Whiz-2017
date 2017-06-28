using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urinal : MonoBehaviour {

    private bool Occupied = false;
    public bool Peed = false;

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

    public bool IsPeed()
    {
        return Peed;
    }
    public void CreatedPeeMess()
    {
        Peed = true;
    }
    public void PeeCleaned()
    {
        Peed = false;
    }
}
